using System;
using System.Collections.Generic;
using System.Linq;
using MageBot.Util.IO;
using Util.Util.Text.Log;
using MageBot.Util.Enums.Internal;
using MageBot.DataFiles.Data.Pathfinding.Positions;
using MageBot.DataFiles.Data.Pathfinding;
using MageBot.Protocol.Messages.Game.Context.Fight;
using MageBot.Protocol.Messages.Game.Context;
using MageBot.Protocol.Messages.Game.Actions.Fight;
using MageBot.Protocol.Messages.Game.Context.Roleplay.Fight;
using MageBot.Protocol.Enums;
using MageBot.Core.Monsters;
using MageBot.Core.Engine.Network;
using System.Threading.Tasks;

namespace MageBot.Core.Fight
{
    public class BFight
    {
        #region Fields 
        private Account.Account m_Account;
        private FightParser m_AI;
        private FightData m_Data;
        public int flag;

        #region Private Fields
        private Object clock = new Object();
        private int m_error = 0;
        private List<MonsterGroup> Banned = new List<MonsterGroup>();
        #endregion
        #endregion

        #region Constructors
        public BFight(Account.Account account, FightParser AI, FightData data)
        {
            m_Account = account;
            m_AI = AI;
            m_Data = data;
            flag = 1;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Execute the AI plan.
        /// </summary>
        public void ExecutePlan()
        {
            if (flag != 1)
                return;
            m_Account.SetStatus(Status.Fighting);
            Dictionary<BSpell, BFighter> plan = m_AI.GetPlan();
            foreach (KeyValuePair<BSpell, BFighter> pair in plan)
            {
                if (pair.Value == null)
                    return;
                int c = m_Data.CanUseSpell(pair.Key, pair.Value); /* Get if we can use the spell with/without move, or if we can't launch the spell */
                switch (c)
                {
                    case -1:              /* We can't use the spell, continue to the next spell */
                        continue;
                    case 0:
                        LaunchSpell(pair.Key.SpellId, pair.Value.CellId);  /* We can use the spell without move */
                        return;
                    default:
                        MoveToCell(c);                                    /* We can use the spell with move */
                        LaunchSpell(pair.Key.SpellId, pair.Value.CellId);
                        return;
                }
            }
            PerformMove(); /* No spell are launchable, move if we can and end the turn */
        }

        /// <summary>
        /// Search for a fight on the map.
        /// </summary>
        public async Task<bool> SearchFight()
        {
            int minNumber = m_Account.Config.MinMonstersNumber;
            int maxNumber = m_Account.Config.MaxMonstersNumber;
            int minLevel = m_Account.Config.MinMonstersLevel;
            int maxLevel = m_Account.Config.MaxMonstersLevel;
            MonsterGroup monsters = m_Account.MapData.Monsters.FirstOrDefault(monst => monst.monstersCount >= minNumber &&
                                                                                       monst.monstersCount <= maxNumber &&
                                                                                       monst.monstersLevel >= minLevel &&
                                                                                       monst.monstersLevel <= maxLevel &&
                                                                                       m_Account.AllowedGroup(monst.NameList()) &&
                                                                                       !Banned.Contains(monst));
            if (monsters != null)
            {
                if (m_Account.Map.MoveToCell(monsters.m_cellId).Result)
                {
                    m_Account.SetStatus(Status.None);
                    m_Account.Log(new ActionTextInformation(string.Format("Fight started, {0} monsters of level {1} ({2})", monsters.monstersCount, monsters.monstersLevel, monsters.monstersName(true))), 1);
                    await m_Account.PutTaskDelay(2000);
                    m_Account.Fight.LaunchFight((int)monsters.m_contextualId);
                    await m_Account.PutTaskDelay(2000);
                    if (m_Account.State != Status.Fighting)
                        await SearchFight();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Launch a fight by sending a GameRolePlayAttackMonsterRequestMessage.
        /// </summary>
        public void LaunchFight(int id)
        {
            GameRolePlayAttackMonsterRequestMessage msg = new GameRolePlayAttackMonsterRequestMessage(id);
            m_Account.SocketManager.Send(msg);
            m_Account.Log(new ActionTextInformation("Launch Fight !"), 1);
        }

        /// <summary>
        /// Increase the error count
        /// </summary>
        public void Error()
        {
            m_error++;
            if (m_error > 1)
            {
                BanMonsters(m_Data.FollowingGroup);
                m_error = 0;
            }
        }

        /// <summary>
        /// Place the character according to the AI positioning.
        /// </summary>
        public void PlaceCharacter(List<int> PlacementCells)
        {
            m_error = 0;
            m_Account.Log(new BotTextInformation("Placement du personnage."), 5);
            try
            {
                PlacementEnum position = m_AI.GetPositioning();
                int distance = -1;
                int cell = -1;
                Dictionary<int, int> cells = new Dictionary<int, int>();
                foreach (int tempCell in PlacementCells)
                {
                    int tempDistance = 0;
                    MapPoint cellPoint = new MapPoint(Convert.ToInt32(tempCell));
                    foreach (BFighter fighter in m_Data.Fighters)
                    {
                        MapPoint fighterPoint = new MapPoint(fighter.CellId);
                        tempDistance += cellPoint.DistanceToCell(fighterPoint);
                    }

                    switch (position)
                    {
                        case PlacementEnum.Eloigné:
                        case PlacementEnum.Far:
                            if (distance < tempDistance || distance == -1)
                            {
                                distance = tempDistance;
                                cell = Convert.ToInt32(tempCell);
                            }
                            break;
                        case PlacementEnum.Near:
                        case PlacementEnum.Rapproché:
                            if (distance > tempDistance || distance == -1 || tempDistance == 1)
                            {
                                distance = tempDistance;
                                cell = Convert.ToInt32(tempCell);
                            }
                            break;
                    }
                }
                if (cell != -1)
                {
                    GameFightPlacementPositionRequestMessage msg = new GameFightPlacementPositionRequestMessage((ushort)cell);
                    m_Account.SocketManager.Send(msg);
                }
            }
            catch (Exception ex)
            {
                m_Account.Log(new ErrorTextInformation(ex.Message), 0);
            }
        }

        /// <summary>
        /// Lock the fight by sending the GameFightOptionToggleMessage.
        /// </summary>
        public void LockFight()
        {
            GameFightOptionToggleMessage msg = new GameFightOptionToggleMessage((byte)FightOptionsEnum.FIGHT_OPTION_SET_CLOSED);
            m_Account.SocketManager.Send(msg);
            m_Account.Log(new ActionTextInformation("Combat closed"), 4);
        }

        public void LockFightForParty()
        {
            GameFightOptionToggleMessage msg = new GameFightOptionToggleMessage((byte)FightOptionsEnum.FIGHT_OPTION_SET_TO_PARTY_ONLY);
            m_Account.SocketManager.Send(msg);
            m_Account.Log(new ActionTextInformation("Combat closed to party only"), 4);
        }

        public void LockFightForSpectators()
        {
            GameFightOptionToggleMessage msg = new GameFightOptionToggleMessage((byte)FightOptionsEnum.FIGHT_OPTION_SET_SECRET);
            m_Account.SocketManager.Send(msg);
            m_Account.Log(new ActionTextInformation("Combat spectator closed"), 4);
        }

        public void AskForHelpInFight()
        {
            GameFightOptionToggleMessage msg = new GameFightOptionToggleMessage((byte)FightOptionsEnum.FIGHT_OPTION_ASK_FOR_HELP);
            m_Account.SocketManager.Send(msg);
            m_Account.Log(new ActionTextInformation("Asking for help"), 4);
        }

        #endregion

        #region Private Methods
        private SpellCategory GetCategory(int effectId)
        {
            switch (effectId)
            {
                case 96:
                    // water
                    return SpellCategory.DamagesWater;
                case 97:
                    //earth
                    return SpellCategory.DamagesEarth;
                case 98:
                    //air
                    return SpellCategory.DamagesAir;
                case 99:
                    //fire
                    return SpellCategory.DamagesFire;
                case 100:
                    return SpellCategory.DamagesNeutral;
                //neutral
                case 623:
                    return SpellCategory.Invocation;
                case 81:
                case 143:
                case 108:
                    return SpellCategory.Healing;
                case 120:
                    return SpellCategory.Buff;
                case 4:
                    return SpellCategory.Teleport;
                default:
                    return SpellCategory.None;
            }
        }

        /// <summary>
        /// Kick the specified player from the fight.
        /// </summary>
        private void KickPlayer(ulong id)
        {
            GameContextKickMessage msg = new GameContextKickMessage(id);
            m_Account.SocketManager.Send(msg);
        }

        /// <summary>
        /// Launch a spell on the specified cellId.
        /// </summary>
        private void LaunchSpell(int spellId, int cellId)
        {
            flag = 1;
            using (BigEndianWriter writer = new BigEndianWriter())
            {
                GameActionFightCastRequestMessage msg = new GameActionFightCastRequestMessage((ushort)spellId, (short)cellId);
                msg.Serialize(writer);
                writer.Content = m_Account.HumanCheck.Hash_function(writer.Content);
                MessagePackaging pack = new MessagePackaging(writer);
                pack.Pack(msg.MessageID);
                m_Account.SocketManager.Send(pack.Writer.Content);
                m_Account.Log(new ActionTextInformation("Lancement d'un sort en " + cellId), 5);
                m_Account.Log(new DebugTextInformation("[SND] 1005 (GameActionFightCastRequestMessage)"), 0);
            }
        }

        /// <summary>
        /// Move to the specified cell (Fight).
        /// </summary>
        private bool MoveToCell(int cellId)
        {
            if (cellId != m_Data.Fighter.CellId)
            {
                if (!(m_Data.IsCellWalkable(cellId)))
                {
                    int num = -1;
                    int num2 = 5000;
                    MapPoint point = new MapPoint(m_Data.Fighter.CellId);
                    MapPoint point2 = new MapPoint(cellId);
                    int direction = 1;
                    while (true)
                    {
                        MapPoint nearestCellInDirection = point2.GetNearestCellInDirection(direction, 1);
                        if (m_Data.IsCellWalkable(nearestCellInDirection.CellId))
                        {
                            int num4 = point.DistanceToCell(nearestCellInDirection);
                            if (num4 < num2)
                            {
                                num2 = num4;
                                num = nearestCellInDirection.CellId;
                            }
                        }
                        direction = (direction + 2);
                        if (direction > 7)
                        {
                            if (num == -1)
                                return false;
                            cellId = num;
                            break;
                        }
                    }
                }
                SimplePathfinder pathfinder = new SimplePathfinder(m_Account.MapData);
                pathfinder.SetFight(m_Data.Fighters, m_Data.Fighter.MovementPoints);
                MovementPath path = pathfinder.FindPath(m_Data.Fighter.CellId, cellId);
                if (path != null)
                {
                    List<UInt32> serverMovement = MapMovementAdapter.GetServerMovement(path);
                    using (BigEndianWriter writer = new BigEndianWriter())
                    {
                        GameMapMovementRequestMessage msg = new GameMapMovementRequestMessage(serverMovement.ToList().Select(ui => (short)ui).ToList(), m_Account.MapData.Id);
                        msg.Serialize(writer);
                        writer.Content = m_Account.HumanCheck.Hash_function(writer.Content);
                        MessagePackaging pack = new MessagePackaging(writer);
                        pack.Pack(msg.MessageID);
                        flag = 0;
                        m_Account.SocketManager.Send(pack.Writer.Content);
                        m_Account.Log(new DebugTextInformation("[SND] 950 (GameMapMovementRequestMessage)"), 0);
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Send the GameFightTurnFinishMessage packet in order to end the turn.
        /// </summary>
        public void EndTurn()
        {
            GameFightTurnFinishMessage msg = new GameFightTurnFinishMessage();
            m_Account.SocketManager.Send(msg);
        }

        /// <summary>
        /// Perform the correct move according to the tactic and end the turn.
        /// </summary>
        private void PerformMove()
        {
            BFighter NearMonster = m_Data.NearestMonster();

            if (NearMonster == null)
            {
                EndTurn();
                return;
            }
            TacticEnum tactic = m_AI.GetStrategy();
            // EndMove
            if (m_Data.Fighter.MovementPoints > 0 && tactic != TacticEnum.Immobile)
            {
                int distance = m_Data.DistanceFrom(NearMonster);
                if (tactic == TacticEnum.Fuyard)
                {
                    MoveToCell(m_Data.FurthestCellFrom(NearMonster));
                }
                else if (tactic == TacticEnum.Barbare)
                {
                    if (!m_Data.IsHandToHand())
                        MoveToCell(m_Data.NearestCellFrom(NearMonster));
                    else
                        EndTurn();
                }
                flag = -1;

                m_Account.Log(new BotTextInformation("EndMove"), 5);
            }
            else
                EndTurn();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Ban a group of monsters (we won't try to launch a fight against them).
        /// </summary>
        private void BanMonsters(MonsterGroup m)
        {
            Banned.Add(m);
        }
        #endregion
    }
}
