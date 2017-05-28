using System;
using System.Linq;
using Util.Util.Text.Log;
using MageBot.Util.IO;
using MageBot.Protocol.Messages;
using MageBot.Util.Enums.Internal;
using MageBot.DataFiles.Data.Pathfinding.Positions;
using MageBot.DataFiles.Data.Pathfinding;
using MageBot.Protocol.Messages.Game.Actions.Fight;
using MageBot.Protocol.Messages.Game.Context.Fight;
using MageBot.Protocol.Messages.Game.Character.Stats;
using MageBot.Protocol.Messages.Game.Actions;
using MageBot.Protocol.Messages.Game.Actions.Sequence;
using MageBot.Protocol.Messages.Game.Inventory.Preset;
using MageBot.Protocol.Messages.Game.Context;
using MageBot.Protocol.Types.Game.Context.Fight;
using MageBot.Protocol.Messages.Game.Context.Fight.Character;
using MageBot.Core.Fight;
using MageBot.DataFiles.Data.D2o;
using System.Collections.Generic;
using MageBot.DataFiles.Data.I18n;

namespace MageBot.Core.Engine.Handlers.Fight
{
    class FightHandler
    {
        #region Public methods
        [MessageHandler(typeof(GameActionFightDeathMessage))]
        public static void GameActionFightDeathMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameActionFightDeathMessage msg = (GameActionFightDeathMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.SetFighterDeath((long)msg.TargetId);
        }

        [MessageHandler(typeof(GameActionFightDispellableEffectMessage))]
        public static void GameActionFightDispellableEffectMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameActionFightDispellableEffectMessage msg = (GameActionFightDispellableEffectMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.SetEffect(msg.Effect, msg.ActionId);
        }

        [MessageHandler(typeof(GameActionFightPointsVariationMessage))]
        public static void GameActionFightPointsVariationMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameActionFightPointsVariationMessage msg = (GameActionFightPointsVariationMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.SetPointsVariation((long)msg.TargetId, msg.ActionId, msg.Delta);
        }

        [MessageHandler(typeof(GameActionFightLifePointsLostMessage))]
        public static void GameActionFightLifePointsLostMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameActionFightLifePointsLostMessage msg = (GameActionFightLifePointsLostMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.UpdateFighterLifePoints((long)msg.TargetId, -((int)msg.Loss));

        }

        [MessageHandler(typeof(GameActionFightSlideMessage))]
        public static void GameActionFightSlideMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameActionFightSlideMessage msg = (GameActionFightSlideMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.UpdateFighterCell((long)msg.TargetId, msg.EndCellId);
        }

        [MessageHandler(typeof(GameActionFightSpellCastMessage))]
        public static void GameActionFightSpellCastMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameActionFightSpellCastMessage msg = (GameActionFightSpellCastMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.SetSpellCasted((long)msg.SourceId, msg.SpellId, msg.DestinationCellId);
        }

        [MessageHandler(typeof(GameActionFightSummonMessage))]
        public static void GameActionFightSummonMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameActionFightSummonMessage msg = (GameActionFightSummonMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            msg.Summons.ForEach(summon =>
            account.FightData.AddSummon(
                (long)msg.SourceId, new BFighter(
                    summon.ContextualId, summon.Disposition.CellId,
                    summon.Stats.ActionPoints, summon.Stats, summon.Alive,
                    (int)summon.Stats.LifePoints, (int)summon.Stats.MaxLifePoints,
                    summon.Stats.MovementPoints, summon.TeamId, 0))
            );
        }

        [MessageHandler(typeof(GameActionFightTeleportOnSameMapMessage))]
        public static void GameActionFightTeleportOnSameMapMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameActionFightTeleportOnSameMapMessage msg = (GameActionFightTeleportOnSameMapMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.UpdateFighterCell((long)msg.TargetId, msg.CellId);
        }

        [MessageHandler(typeof(GameEntitiesDispositionMessage))]
        public static void GameEntitiesDispositionMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameEntitiesDispositionMessage msg = (GameEntitiesDispositionMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (account.Fight != null)
            {
                msg.Dispositions.ToList().ForEach(d =>
                {
                    account.FightData.UpdateFighterCell((long)d.ObjectId, d.CellId);
                });
            }
            account.SetStatus(Status.Fighting);
        }

        [MessageHandler(typeof(GameFightEndMessage))]
        public static void GameFightEndMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightEndMessage msg = (GameFightEndMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            TreatObtainedLoot(account, msg);
            if (account.MyGroup != null)
            {
                //Thread t = new Thread(() =>
                //{
                account.FightData.FightStop();
                if (account.IsMaster && account.MyGroup.Path != null)
                    account.MyGroup.Path.PerformFlag();
                //});
                //t.Start();
            }
            else
            {
                /*Thread t = new Thread(*/
                account.Path.PerformFlag()/*)*/;
                //t.Start();
            }
        }

        [MessageHandler(typeof(GameFightHumanReadyStateMessage))]
        public static void GameFightHumanReadyStateMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightHumanReadyStateMessage msg = (GameFightHumanReadyStateMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (msg.CharacterId == account.CharacterBaseInformations.ObjectID)
                account.FightData.WaitForReady = !msg.IsReady;
        }

        [MessageHandler(typeof(GameFightJoinMessage))]
        public static void GameFightJoinMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightJoinMessage msg = (GameFightJoinMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (account.Fight != null)
            {
                account.FightData.Reset(msg.IsFightStarted, msg.CanSayReady);
            }
        }

        [MessageHandler(typeof(GameFightLeaveMessage))]
        public static void GameFightLeaveMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightLeaveMessage msg = (GameFightLeaveMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }

            if (msg.CharId == account.CharacterBaseInformations.ObjectID)
            {
                //Thread t = new Thread(() =>
                account.FightData.FightStop();
                //);
                //t.Start();
            }
        }

        [MessageHandler(typeof(GameFightOptionStateUpdateMessage))]
        public static void GameFightOptionStateUpdateMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightOptionStateUpdateMessage msg = (GameFightOptionStateUpdateMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.SetOption(msg.State, msg.Option);
        }

        [MessageHandler(typeof(GameFightShowFighterMessage))]
        public static void GameFightShowFighterMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightShowFighterMessage msg = (GameFightShowFighterMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.AddFighter(msg.Informations);

        }

        [MessageHandler(typeof(GameFightShowFighterRandomStaticPoseMessage))]
        public static void GameFightShowFighterRandomStaticPoseMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightShowFighterRandomStaticPoseMessage msg = (GameFightShowFighterRandomStaticPoseMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.AddFighter(msg.Informations);
        }

        [MessageHandler(typeof(GameFightStartMessage))]
        public static void GameFightStartMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightStartMessage msg = (GameFightStartMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.FightStart();
        }

        [MessageHandler(typeof(GameFightSynchronizeMessage))]
        public static void GameFightSynchronizeMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightSynchronizeMessage msg = (GameFightSynchronizeMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.ClearFighters();
            foreach (GameFightFighterInformations i in msg.Fighters)
                account.FightData.AddFighter(i);
        }

        [MessageHandler(typeof(GameFightTurnEndMessage))]
        public static void GameFightTurnEndMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightTurnEndMessage msg = (GameFightTurnEndMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.TurnEnded((ulong)msg.ObjectId);
        }

        [MessageHandler(typeof(GameFightTurnStartMessage))]
        public static void GameFightTurnStartMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightTurnStartMessage msg = (GameFightTurnStartMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            //Thread t = new Thread(() =>
            account.Wait((int)msg.WaitTime);
            //);
            //t.Start();
        }

        [MessageHandler(typeof(GameMapMovementMessage))]
        public static void GameMapMovementMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameMapMovementMessage msg = (GameMapMovementMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            MovementPath clientMovement = MapMovementAdapter.GetClientMovement(msg.KeyMovements.Select(k => (int)k).ToList());
            if (account.State == Status.Fighting)
            {
                account.FightData.UpdateFighterCell((long)msg.ActorId, clientMovement.CellEnd.CellId);
            }
            account.UpdateMap();
        }

        [MessageHandler(typeof(GameFightNewRoundMessage))]
        public static void GameFightNewRoundMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightNewRoundMessage msg = (GameFightNewRoundMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.UpdateTurn((int)msg.RoundNumber);
        }

        [MessageHandler(typeof(GameFightTurnStartPlayingMessage))]
        public static void GameFightTurnStartPlayingMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightTurnStartPlayingMessage msg = (GameFightTurnStartPlayingMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.TurnStarted();
            if (account.Fight != null && !account.FightData.IsDead)
            {
                account.Fight.flag = 1;
                //Thread t = new Thread(() =>
                account.Fight.ExecutePlan();
                //);
                //t.Start();
            }
            else
                account.Log(new ErrorTextInformation("No AI, the bot can not fight !"), 0);
        }

        [MessageHandler(typeof(GameFightPlacementPossiblePositionsMessage))]
        public static void GameFightPlacementPossiblePositionsMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightPlacementPossiblePositionsMessage msg = (GameFightPlacementPossiblePositionsMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.SetStatus(Status.Fighting);
            account.FightData.UpdateTurn(0);
            if (account.Fight != null)
            {
                account.Fight.PlaceCharacter(msg.PositionsForChallengers.Select(item => (int)item).ToList());
            }
            PerformStartWithItemSet(account);
            //Thread t = new Thread(() =>
            //{
            if (account.MyGroup != null)
                account.Wait(2000);
            PerformLockFight(account);
            GameFightReadyMessage nmsg = new GameFightReadyMessage(true);
            account.SocketManager.Send(nmsg);
            account.Log(new BotTextInformation("Send Ready!"), 5);
            //});
            //t.Start();
        }

        private static void PerformStartWithItemSet(Account.Account account)
        {
            if (account.Config.StartFightWithItemSet)
            {
                byte id = account.Config.PresetStartUpId;
                InventoryPresetUseMessage msg2 = new InventoryPresetUseMessage((byte)(id - 1));
                account.SocketManager.Send(msg2);
                account.Log(new ActionTextInformation("Fast equipment number " + Convert.ToString(id)), 5);
            }
        }

        [MessageHandler(typeof(GameFightTurnReadyRequestMessage))]
        public static void GameFightTurnReadyRequestMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightTurnReadyRequestMessage msg = (GameFightTurnReadyRequestMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            GameFightTurnReadyMessage msg2 = new GameFightTurnReadyMessage(true);
            account.SocketManager.Send(msg2);
        }

        [MessageHandler(typeof(SequenceEndMessage))]
        public static void SequenceEndMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            SequenceEndMessage msg = (SequenceEndMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (!account.FightData.IsDead && account.FightData.Fighter.Id == msg.AuthorId && !account.Config.IsMITM && account.Fight != null && account.FightData.IsFighterTurn)
            {
                GameActionAcknowledgementMessage msg2 = new GameActionAcknowledgementMessage(true, (byte)msg.ActionId);
                account.SocketManager.Send(msg2);
                switch (account.Fight.flag)
                {
                    case -1:
                        account.Fight.EndTurn();
                        break;
                    case 1:
                        /*Thread t = new Thread(*/account.Fight.ExecutePlan()/*)*/;
                        //t.Start();
                        break;
                }
            }
        }

        [MessageHandler(typeof(LifePointsRegenBeginMessage))]
        public static void LifePointsRegenBeginMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            LifePointsRegenBeginMessage msg = (LifePointsRegenBeginMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
        }

        [MessageHandler(typeof(LifePointsRegenEndMessage))]
        public static void LifePointsRegenEndMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            LifePointsRegenEndMessage msg = (LifePointsRegenEndMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            int percent = ((int)msg.LifePoints / (int)msg.MaxLifePoints) * 100;
            account.Log(new BotTextInformation("Regeneration ends. + " + msg.LifePointsGained + " lifepoints"), 2);
            account.ModifBar(2, (int)msg.MaxLifePoints, (int)msg.LifePoints, "Life");
            account.SetStatus(Status.None);
        }

        [MessageHandler(typeof(GameFightSpectatorJoinMessage))]
        public static void GameFightSpectatorJoinMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameFightSpectatorJoinMessage msg = (GameFightSpectatorJoinMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }

            if (account.Config.LockingSpectators)
            {
                account.Fight.LockFightForSpectators();
            }
        }

        [MessageHandler(typeof(GameActionFightLifePointsGainMessage))]
        public static void GameActionFightLifePointsGainMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameActionFightLifePointsGainMessage msg = (GameActionFightLifePointsGainMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (account.State == Status.Fighting)
            {
                if (msg.ActionId == 108) // HP Récupérés (delta = combien on a récupérés)
                {
                    account.FightData.UpdateFighterLifePoints((long)msg.TargetId, (int)msg.Delta);
                }
            }
        }

        [MessageHandler(typeof(GameActionFightTackledMessage))]
        public static void GameActionFightTackledMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameActionFightTackledMessage msg = (GameActionFightTackledMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (account.Fight != null && account.Fight.flag == -1)
                account.Fight.EndTurn();
        }
        #endregion

        #region Private methods
        private static void PerformLockFight(Account.Account account)
        {
            if (account.MyGroup != null)
            {
                if (account.IsMaster && account.Config.LockingFights && account.Fight != null && !account.LockPerformed)
                {
                    account.Fight.LockFight();
                    account.LockPerformed = true;
                }
            }
            else
            {
                if (account.Config.LockingFights && account.Fight != null && !account.LockPerformed)
                {
                    account.FightData.PerformAutoTimeoutFight(2000);
                    account.Fight.LockFight();
                    account.LockPerformed = true;
                }
            }
        }

        private static void TreatObtainedLoot(Account.Account account, GameFightEndMessage msg)
        {
            List<ushort> itemsWithQuantity = new List<ushort>();
            Dictionary<ushort, int> itemsByQuantity = new Dictionary<ushort, int>();
            msg.Results.ForEach(res => itemsWithQuantity.AddRange(res.Rewards.Objects));

            for (int i = 0; i < itemsWithQuantity.Count; i += 2)
            {
                if (i + 1 < itemsWithQuantity.Count)
                {
                    if (itemsByQuantity.ContainsKey(itemsWithQuantity[i]))
                        itemsByQuantity[itemsWithQuantity[i]] += itemsWithQuantity[i + 1];
                    else
                        itemsByQuantity.Add(itemsWithQuantity[i], itemsWithQuantity[i + 1]);
                }
            }
            ulong kamas = 0;
            msg.Results.ForEach(res => kamas += res.Rewards.Kamas);
            List<string> itemsNamesWithQuantity = new List<string>();
            itemsByQuantity.Keys.ToList().ForEach(item => itemsNamesWithQuantity.Add(I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Items, item).Fields["nameId"]) +
                                                " x " + itemsByQuantity[item]));
            account.Log(new BotTextInformation("Total obtained items"), 0);
            foreach (string s in itemsNamesWithQuantity)
            {
                account.Log(new BotTextInformation(s), 0);
            }
            account.Log(new BotTextInformation("total obtained Kamas : " + kamas + " kamas."), 0);
        }
        #endregion
    }
}

