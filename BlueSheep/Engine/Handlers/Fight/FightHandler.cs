using System;
using System.Linq;
using BlueSheep.Interface;
using BlueSheep.Interface.Text;
using BlueSheep.Common.IO;
using BlueSheep.Common;
using BlueSheep.Common.Types;
using BlueSheep.Engine.Enums;
using BlueSheep.Data.Pathfinding.Positions;
using BlueSheep.Data.Pathfinding;
using BlueSheep.Common.Protocol.Messages.Game.Actions.Fight;
using BlueSheep.Common.Protocol.Messages.Game.Context.Fight;
using BlueSheep.Common.Protocol.Messages.Game.Character.Stats;
using BlueSheep.Common.Protocol.Messages.Game.Actions;
using BlueSheep.Common.Protocol.Messages.Game.Actions.Sequence;
using BlueSheep.Common.Protocol.Messages.Game.Inventory.Preset;
using BlueSheep.Common.Protocol.Messages.Game.Context;
using BlueSheep.Common.Protocol.Types.Game.Context.Fight;
using BlueSheep.Common.Protocol.Messages.Game.Context.Fight.Character;

namespace BlueSheep.Engine.Handlers.Fight
{
    class FightHandler
    {
        #region Public methods
        [MessageHandler(typeof(GameActionFightDeathMessage))]
        public static void GameActionFightDeathMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameActionFightDeathMessage msg = (GameActionFightDeathMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.SetFighterDeath((long)msg.TargetId);
        }

        [MessageHandler(typeof(GameActionFightDispellableEffectMessage))]
        public static void GameActionFightDispellableEffectMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameActionFightDispellableEffectMessage msg = (GameActionFightDispellableEffectMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.SetEffect(msg.Effect, msg.ActionId);
        }

        [MessageHandler(typeof(GameActionFightPointsVariationMessage))]
        public static void GameActionFightPointsVariationMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameActionFightPointsVariationMessage msg = (GameActionFightPointsVariationMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.SetPointsVariation((long)msg.TargetId, msg.ActionId, msg.Delta);
        }

        [MessageHandler(typeof(GameActionFightLifePointsLostMessage))]
        public static void GameActionFightLifePointsLostMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameActionFightLifePointsLostMessage msg = (GameActionFightLifePointsLostMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.UpdateFighterLifePoints((long)msg.TargetId, -((int)msg.Loss));

        }

        [MessageHandler(typeof(GameActionFightSlideMessage))]
        public static void GameActionFightSlideMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameActionFightSlideMessage msg = (GameActionFightSlideMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.UpdateFighterCell((long)msg.TargetId, msg.EndCellId);
        }

        [MessageHandler(typeof(GameActionFightSpellCastMessage))]
        public static void GameActionFightSpellCastMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameActionFightSpellCastMessage msg = (GameActionFightSpellCastMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.SetSpellCasted((long)msg.SourceId, msg.SpellId, msg.DestinationCellId);
        }

        [MessageHandler(typeof(GameActionFightSummonMessage))]
        public static void GameActionFightSummonMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
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
                    summon.Stats.MovementPoints, (uint)summon.TeamId, 0))
            );
        }

        [MessageHandler(typeof(GameActionFightTeleportOnSameMapMessage))]
        public static void GameActionFightTeleportOnSameMapMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameActionFightTeleportOnSameMapMessage msg = (GameActionFightTeleportOnSameMapMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.UpdateFighterCell((long)msg.TargetId, msg.CellId);
        }

        [MessageHandler(typeof(GameEntitiesDispositionMessage))]
        public static void GameEntitiesDispositionMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
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
        public static void GameFightEndMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameFightEndMessage msg = (GameFightEndMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.FightStop();
            account.SetStatus(Status.None);
            //account.Fight.infinite = true; // Swap it with checkbox
        }

        [MessageHandler(typeof(GameFightHumanReadyStateMessage))]
        public static void GameFightHumanReadyStateMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
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
        public static void GameFightJoinMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameFightJoinMessage msg = (GameFightJoinMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (account.Fight != null)
            {
                account.FightData.Reset(msg.IsFightStarted, msg.CanSayReady);
                if (account.IsLockingFight.Checked && account.Fight != null)
                {
                    account.FightData.PerformAutoTimeoutFight(2000);
                    account.Fight.LockFight();
                }

            }
        }

        [MessageHandler(typeof(GameFightLeaveMessage))]
        public static void GameFightLeaveMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameFightLeaveMessage msg = (GameFightLeaveMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            /* TODO : HANDLE IT */

            if (msg.CharId == account.CharacterBaseInformations.ObjectID)
            {
                account.FightData.FightStop();
            }
            //else
            //{
            //    BFighter fighter = account.Fight.GetFighter(msg.charId);
            //    if (fighter != null)
            //        account.Fight.Fighters.Remove(fighter);
            //}
        }

        [MessageHandler(typeof(GameFightOptionStateUpdateMessage))]
        public static void GameFightOptionStateUpdateMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameFightOptionStateUpdateMessage msg = (GameFightOptionStateUpdateMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.SetOption(msg.State, msg.Option);
        }

        [MessageHandler(typeof(GameFightShowFighterMessage))]
        public static void GameFightShowFighterMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameFightShowFighterMessage msg = (GameFightShowFighterMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.AddFighter(msg.Informations);

        }

        [MessageHandler(typeof(GameFightShowFighterRandomStaticPoseMessage))]
        public static void GameFightShowFighterRandomStaticPoseMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameFightShowFighterRandomStaticPoseMessage msg = (GameFightShowFighterRandomStaticPoseMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.AddFighter(msg.Informations);
        }

        [MessageHandler(typeof(GameFightStartMessage))]
        public static void GameFightStartMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameFightStartMessage msg = (GameFightStartMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.FightStart();
        }

        [MessageHandler(typeof(GameFightSynchronizeMessage))]
        public static void GameFightSynchronizeMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
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
        public static void GameFightTurnEndMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameFightTurnEndMessage msg = (GameFightTurnEndMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.TurnEnded((ulong)msg.ObjectId);
        }

        [MessageHandler(typeof(GameFightTurnStartMessage))]
        public static void GameFightTurnStartMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameFightTurnStartMessage msg = (GameFightTurnStartMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }

        }

        [MessageHandler(typeof(GameMapMovementMessage))]
        public static void GameMapMovementMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameMapMovementMessage msg = (GameMapMovementMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            MovementPath clientMovement = MapMovementAdapter.GetClientMovement(msg.KeyMovements.Select(k => (int)k).ToList());
            if (account.state == Enums.Status.Fighting)
            {
                account.FightData.UpdateFighterCell((long)msg.ActorId, clientMovement.CellEnd.CellId);
            }
            account.ActualizeMap();
        }

        [MessageHandler(typeof(GameFightNewRoundMessage))]
        public static void GameFightNewRoundMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameFightNewRoundMessage msg = (GameFightNewRoundMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.UpdateTurn((int)msg.RoundNumber);
        }

        [MessageHandler(typeof(GameFightTurnStartPlayingMessage))]
        public static void GameFightTurnStartPlayingMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameFightTurnStartPlayingMessage msg = (GameFightTurnStartPlayingMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.FightData.TurnStarted();
            if (account.Fight != null)
            {
                account.Fight.flag = 1;
                account.Fight.ExecutePlan();
            }
            else
                account.Log(new ErrorTextInformation("Aucune IA, le bot ne peut pas combattre !"), 0);
        }

        [MessageHandler(typeof(GameFightPlacementPossiblePositionsMessage))]
        public static void GameFightPlacementPossiblePositionsMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
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
            if (account.WithItemSetBox.Checked == true)
            {
                byte id = (byte)account.PresetStartUpD.Value;
                InventoryPresetUseMessage msg2 = new InventoryPresetUseMessage((byte)(id - 1));
                account.SocketManager.Send(msg2);
                account.Log(new ActionTextInformation("Equipement rapide numero " + Convert.ToString(id)), 5);
            }
            GameFightReadyMessage nmsg = new GameFightReadyMessage(true);
            account.SocketManager.Send(nmsg);
            account.Log(new BotTextInformation("Send Ready !"), 5);
        }

        [MessageHandler(typeof(GameFightTurnReadyRequestMessage))]
        public static void GameFightTurnReadyRequestMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
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
        public static void SequenceEndMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            SequenceEndMessage msg = (SequenceEndMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (!account.FightData.IsDead && account.FightData.Fighter.Id == msg.AuthorId && !account.IsMITM && account.Fight != null && account.FightData.IsFighterTurn)
            {
                GameActionAcknowledgementMessage msg2 = new GameActionAcknowledgementMessage(true, (byte)msg.ActionId);
                account.SocketManager.Send(msg2);
                switch (account.Fight.flag)
                {
                    case -1:
                        account.Fight.EndTurn();
                        break;
                    case 1:
                        account.Fight.ExecutePlan();
                        break;
                }
            }
        }

        [MessageHandler(typeof(LifePointsRegenBeginMessage))]
        public static void LifePointsRegenBeginMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            LifePointsRegenBeginMessage msg = (LifePointsRegenBeginMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
        }

        [MessageHandler(typeof(LifePointsRegenEndMessage))]
        public static void LifePointsRegenEndMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            LifePointsRegenEndMessage msg = (LifePointsRegenEndMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            int percent = ((int)msg.LifePoints / (int)msg.MaxLifePoints) * 100;
            account.Log(new BotTextInformation("Fin de la régénération. + " + msg.LifePointsGained + " points de vie"), 2);
            account.ModifBar(2, (int)msg.MaxLifePoints, (int)msg.LifePoints, "Vitalité");
        }

        [MessageHandler(typeof(GameFightSpectatorJoinMessage))]
        public static void GameFightSpectatorJoinMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameFightSpectatorJoinMessage msg = (GameFightSpectatorJoinMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            /* TODO : Handle it */
        }

        [MessageHandler(typeof(GameActionFightLifePointsGainMessage))]
        public static void GameActionFightLifePointsGainMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            GameActionFightLifePointsGainMessage msg = (GameActionFightLifePointsGainMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (account.state == Status.Fighting)
            {
                if (msg.ActionId == 108) // HP Récupérés (delta = combien on a récupérés)
                {
                    account.FightData.UpdateFighterLifePoints((long)msg.TargetId, (int)msg.Delta);
                }
            }
        }

        [MessageHandler(typeof(GameActionFightTackledMessage))]
        public static void GameActionFightTackledMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
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
    }
}

