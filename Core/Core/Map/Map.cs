using BlueSheep.Util.IO;
using BlueSheep.Protocol.Messages.Game.Context;
using BlueSheep.Protocol.Messages.Game.Context.Roleplay;
using BlueSheep.Protocol.Messages.Game.Interactive;
using BlueSheep.Protocol.Messages.Game.Interactive.Zaap;
using BlueSheep.Core.Map.Elements;
using BlueSheep.Data.Pathfinding;
using BlueSheep.Data.Pathfinding.Positions;
using BlueSheep.Util.Enums.Internal;
using BlueSheep.Util.Text.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueSheep.Engine.Network;

namespace BlueSheep.Core.Map
{
    public class Map
    {
        #region Fields
        private Account.Account Account { get; set; }
        #endregion

        #region Constructeurs
        public Map(Account.Account account)
        {
            Account = account;
        }
        #endregion

        #region Public methods
        public bool ChangeMap(string direction)
        {
            int neighbourId = -1;
            //int num2 = -1;
            switch (direction)
            {
                case "haut":
                case "up":
                    neighbourId = Account.MapData.Data.TopNeighbourId;
                    //num2 = 64;
                    break;
                case "bas":
                case "bottom":
                    neighbourId = Account.MapData.Data.BottomNeighbourId;
                    //num2 = 4;
                    break;
                case "droite":
                case "right":
                    neighbourId = Account.MapData.Data.RightNeighbourId;
                    //num2 = 1;
                    break;
                case "gauche":
                case "left":
                    neighbourId = Account.MapData.Data.LeftNeighbourId;
                    //num2 = 16;
                    break;
                default:
                    return false;
            }

            //if ((num2 != -1) && (neighbourId >= 0))
            //{
            //    int cellId = m_Account.MapData.Character.Disposition.CellId;
            //    if ((m_Account.MapData.Data.Cells[cellId].MapChangeData & num2) > 0)
            //    {
            ChangeMap(neighbourId);
            return true;
            //}
            //List<int> list = new List<int>();
            //int num4 = (m_Account.MapData.Data.Cells.Count - 1);
            //int i = 0;
            //while (i <= num4)
            //{
            //    if (((m_Account.MapData.Data.Cells[i].MapChangeData & num2) > 0) && m_Account.MapData.NothingOnCell(i))
            //        list.Add(i);
            //    i += 1;
            //}
            //while (list.Count > 0)
            //{
            //    int randomCellId = list[RandomCell(0, list.Count)];
            //    m_MapId = neighbourId;
            //    if (MoveToCell(randomCellId).Result)
            //    {
            //        return true;
            //    }
            //    list.Remove(randomCellId);
            //}
            //}
            //return false;
        }

        public async Task<bool> MoveToCell(int cellId)
        {
            if (Account.State != Status.Fighting)
            {
                Account.SetStatus(Status.Moving);
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
                    await Account.PutTaskDelay(timetowait);
                    Move(serverMovement);
                }
            }
            return true;
        }

        public bool MoveToDoor(int cellId)
        {
            return MoveToCellWithDistance(cellId, 1, true).Result;
        }

        public bool MoveToElement(int id, int maxDistance)
        {
            Elements.StatedElement element = Account.MapData.StatedElements.Find(s => s.Id == id);
            if (element != null)
                return MoveToCellWithDistance((int)element.CellId, maxDistance, false).Result;
            else
                return false;
        }

        public bool MoveToSecureElement(int id)
        {
            Elements.StatedElement element = Account.MapData.StatedElements.Find(s => s.Id == id);
            if (element != null)
                return MoveToCellWithDistance((int)element.CellId, 1, true).Result;
            else
            {
                return false;
            }
        }

        public void ChangeMap(int mapId)
        {
            if (Account.Config.Path != null)
                Account.Config.Path.ClearStack();
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
                writer.Content = Account.HumanCheck.hash_function(writer.Content);
                MessagePackaging pack = new MessagePackaging(writer);
                pack.Pack(msg.MessageID);
                Account.SocketManager.Send(pack.Writer.Content);
            }
            Account.Log(new DebugTextInformation("[SND] 5001 (InteractiveUseRequestMessage)"), 0);
        }

        public void UseElement(int id)
        {
            InteractiveElement e = Account.MapData.InteractiveElements.Keys.ToList().Find(i => i.Id == id);
            UseElement(id, e.EnabledSkills.FirstOrDefault().SkillInstanceUid);
        }

        private void ChangingMapToSameMapAndSamePosition()
        {
            Account.SocketManager.Send(new GameMapMovementConfirmMessage());
            Account.SetStatus(Status.None);
        }

        public async void useZaapiTo(int mapid)
        {

            InteractiveElement e = Account.MapData.InteractiveElements.Keys.ToList().Find(i => i.TypeId == 106);
            if (e != null)
            {
                MoveToSecureElement((int)e.Id);
                UseElement((int)e.Id, e.EnabledSkills[0].SkillInstanceUid);
                await Account.PutTaskDelay(100);
                TeleportRequestMessage msg = new TeleportRequestMessage(1, mapid);
                Account.SocketManager.Send(msg);
            }
        }

        public async void UseZaapTo(int mapid)
        {
            InteractiveElement e = Account.MapData.InteractiveElements.Keys.ToList().Find(i => i.TypeId == 16);
            if (e != null)
            {
                MoveToSecureElement((int)e.Id);
                UseElement((int)e.Id, e.EnabledSkills[0].SkillInstanceUid);
                await Account.PutTaskDelay(100);
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
                writer.Content = Account.HumanCheck.hash_function(writer.Content);
                msg.Pack(writer);
                Account.SocketManager.Send(writer.Content);
                Account.SetStatus(Status.Moving);
                Account.Log(new DebugTextInformation("[SND] 950 (GameMapMovementRequestMessage)"), 0);
            }
        }

        private async Task<bool> MoveToCellWithDistance(int cellId, int maxDistance, bool bool1)
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
            await Account.PutTaskDelay(timetowait);
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
