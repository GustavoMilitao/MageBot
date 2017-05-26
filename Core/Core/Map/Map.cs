using MageBot.Util.IO;
using MageBot.Protocol.Messages.Game.Context;
using MageBot.Protocol.Messages.Game.Context.Roleplay;
using MageBot.Protocol.Messages.Game.Interactive;
using MageBot.Protocol.Messages.Game.Interactive.Zaap;
using MageBot.Core.Map.Elements;
using MageBot.DataFiles.Data.Pathfinding;
using MageBot.DataFiles.Data.Pathfinding.Positions;
using MageBot.Util.Enums.Internal;
using Util.Util.Text.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MageBot.Core.Engine.Network;
using System.Threading;

namespace MageBot.Core.Map
{
    public class Map
    {
        #region Fields
        private Account.Account Account { get; set; }
        private List<string> possibleDir { get; set; } = new List<string>() { "up", "bottom", "right", "left" };
        #endregion

        #region Constructeurs
        public Map(Account.Account account)
        {
            Account = account;
        }
        #endregion

        #region Public methods
        public bool ChangeMap(string direction = "")
        {
            int neighbourId = -1;
            int num2 = -1;
            int rand = new Random().Next(0, 3);
            string direct = String.IsNullOrEmpty(direction) ? possibleDir[rand] : direction;
            neighbourId = TreatNeighbourId(ref num2, direct);

            if ((num2 != -1) && (neighbourId >= 0))
            {
                int cellId = Account.MapData.Character.Disposition.CellId;
                if ((Account.MapData.Data.Cells[cellId].MapChangeData & num2) > 0)
                {
                    ChangeMap(neighbourId);
                    return true;
                }
                List<int> list = GetGoodPositionsToChangeMap(neighbourId, num2, cellId);
                while (list.Count > 0)
                {
                    int randomCellId = list[RandomCell(0, list.Count)];
                    if (MoveToCell(randomCellId))
                    {
                        ChangeMap(neighbourId);
                        return true;
                    }
                    list.Remove(randomCellId);
                }
            }
            return false;
        }

        private List<int> GetGoodPositionsToChangeMap(int neighbourId, int num2, int cellId)
        {
            List<int> list = new List<int>();
            int num4 = (Account.MapData.Data.Cells.Count - 1);
            int i = 0;
            while (i <= num4)
            {
                if (((Account.MapData.Data.Cells[i].MapChangeData & num2) > 0) && Account.MapData.NothingOnCell(i))
                    list.Add(i);
                i += 1;
            }

            return list;
        }

        private int TreatNeighbourId(ref int num2, string direct)
        {
            int neighbourId;
            switch (direct)
            {
                case "haut":
                case "up":
                    neighbourId = Account.MapData.Data.TopNeighbourId;
                    num2 = 64;
                    break;
                case "bas":
                case "bottom":
                    neighbourId = Account.MapData.Data.BottomNeighbourId;
                    num2 = 4;
                    break;
                case "droite":
                case "right":
                    neighbourId = Account.MapData.Data.RightNeighbourId;
                    num2 = 1;
                    break;
                case "gauche":
                case "left":
                    neighbourId = Account.MapData.Data.LeftNeighbourId;
                    num2 = 16;
                    break;
                default:
                    neighbourId = -1;
                    break;
            }

            return neighbourId;
        }

        public bool MoveToCell(int cellId)
        {
            if (Account.State != Status.Fighting)
            {
                MovementPath path = (new Pathfinder(Account.MapData)).FindPath(Account.MapData.Character.Disposition.CellId, cellId);
                if (path != null)
                {
                    List<UInt32> serverMovement = MapMovementAdapter.GetServerMovement(path);
                    int timetowait;
                    if (serverMovement.Count() < 3)
                        timetowait = serverMovement.Count() * 500;
                    else
                    {
                        timetowait = serverMovement.Count() * 300;
                    }
                    Move(serverMovement);
                    //await Account.PutTaskDelay(timetowait);
                    Account.Wait(timetowait);
                    ConfirmMove();
                }
            }
            return true;
        }


        public bool MoveToDoor(int cellId)
        {
            return MoveToCellWithDistance(cellId, 1, true);
        }

        public bool MoveToElement(int id, int maxDistance)
        {
            Elements.StatedElement element = Account.MapData.StatedElements[id];
            if (element != null)
                return MoveToCellWithDistance((int)element.CellId, maxDistance, false);
            else
                return false;
        }

        public bool MoveToSecureElement(int id)
        {
            Elements.StatedElement element = Account.MapData.StatedElements[id];
            if (element != null)
                return MoveToCellWithDistance((int)element.CellId, 1, true);
            else
            {
                return false;
            }
        }

        public void ChangeMap(int mapId)
        {
            if (Account.Path != null)
                Account.Path.ClearStack();
            ChangeMapMessage msg = new ChangeMapMessage(mapId);
            Account.SetStatus(Status.Busy);
            Account.SocketManager.Send(msg);
        }

        public void UseElement(int id, int skillId)
        {
            using (BigEndianWriter writer = new BigEndianWriter())
            {
                InteractiveUseRequestMessage msg = new InteractiveUseRequestMessage((uint)id, (uint)skillId);
                msg.Serialize(writer);
                writer.Content = Account.HumanCheck.Hash_function(writer.Content);
                MessagePackaging mp = new MessagePackaging(writer);
                mp.Pack(msg.MessageID);
                Account.SocketManager.Send(mp.Writer.Content);
            }
            Account.Log(new DebugTextInformation("[SND] 5001 (InteractiveUseRequestMessage)"), 0);
        }

        public void UseElement(int id)
        {
            InteractiveElement e = Account.MapData.InteractiveElements[(uint)id];
            UseElement(id, e.EnabledSkills.FirstOrDefault().SkillInstanceUid);
        }

        private void ConfirmMove()
        {
            Account.SocketManager.Send(new GameMapMovementConfirmMessage());
            Account.SetStatus(Status.None);
        }

        public void useZaapiTo(int mapid)
        {

            InteractiveElement e = Account.MapData.InteractiveElements.Values.ToList().Find(i => i.TypeId == 106);
            if (e != null)
            {
                MoveToSecureElement((int)e.Id);
                UseElement((int)e.Id, e.EnabledSkills[0].SkillInstanceUid);
                Account.Wait(100);
                TeleportRequestMessage msg = new TeleportRequestMessage(1, mapid);
                Account.SocketManager.Send(msg);
            }
        }

        public void UseZaapTo(int mapid)
        {
            InteractiveElement e = Account.MapData.InteractiveElements.Values.ToList().Find(i => i.TypeId == 16);
            if (e != null)
            {
                MoveToSecureElement((int)e.Id);
                UseElement((int)e.Id, e.EnabledSkills[0].SkillInstanceUid);
                Account.Wait(100);
                TeleportRequestMessage msg = new TeleportRequestMessage(0, mapid);
                Account.SocketManager.Send(msg);
            }

        }
        #endregion

        #region Private methods      

        private void Move(List<uint> serverMovement)
        {
            using (BigEndianWriter writer = new BigEndianWriter())
            {
                GameMapMovementRequestMessage msg = new GameMapMovementRequestMessage(serverMovement.Select(ui => (short)ui).ToList(), Account.MapData.Id);
                msg.Serialize(writer);
                writer.Content = Account.HumanCheck.Hash_function(writer.Content);
                MessagePackaging mp = new MessagePackaging(writer);
                mp.Pack(msg.ProtocolId);
                Account.SocketManager.Send(mp.Writer.Content);
                Account.SetStatus(Status.Moving);
                Account.Log(new DebugTextInformation("[SND] 950 (GameMapMovementRequestMessage)"), 0);
            }
        }

        private bool MoveToCellWithDistance(int cellId, int maxDistance, bool bool1)
        {
            MovementPath path = null;
            int savDistance = -1;
            MapPoint characterPoint = new MapPoint(Account.MapData.Character.Disposition.CellId);
            MapPoint targetPoint = new MapPoint(cellId);
            foreach (MapPoint point in Account.MapData.GetListPointAtGoodDistance(characterPoint, targetPoint, maxDistance))
            {
                Pathfinder pathFinding = null;
                if ((targetPoint.DistanceToCell(point) > maxDistance) || ((targetPoint.X != point.X) && (targetPoint.Y != point.Y)))
                    continue;
                int distance = characterPoint.DistanceTo(point);
                if ((savDistance != -1) && (distance >= savDistance))
                    continue;
                if (bool1)
                {
                    if (Account.MapData.Data.IsWalkable(point.CellId))
                        goto Label_00A8;
                    continue;
                }
                if (!(Account.MapData.NothingOnCell(point.CellId)))
                    continue;
                Label_00A8:
                pathFinding = new Pathfinder(Account.MapData);
                MovementPath path2 = pathFinding.FindPath(Account.MapData.Character.Disposition.CellId, point.CellId);
                if (path2 != null)
                {
                    path = path2;
                    savDistance = distance;
                }
            }
            if (path == null)
                return false;
            List<UInt32> serverMovement = MapMovementAdapter.GetServerMovement(path);
            int timetowait;
            if (serverMovement.Count() < 3)
                timetowait = serverMovement.Count() * 514;
            else
            {
                timetowait = serverMovement.Count() * 320;
            }
            Account.Wait(timetowait);
            Move(serverMovement);
            return true;
        }

        private int RandomCell(int min, int max)
        {
            Random random = new Random();
            if (min > max)
            {
                int num = max;
                max = min;
                min = num;
            }
            return random.Next(min, max);
        }

        #endregion
    }
}
