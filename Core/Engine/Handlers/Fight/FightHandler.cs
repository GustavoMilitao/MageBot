using System;
using System.Linq;
using BlueSheep.Util.Text.Log;
using BlueSheep.Util.IO;
using BlueSheep.Data.Pathfinding.Positions;
using BlueSheep.Data.Pathfinding;
using BotForgeAPI.Protocol.Messages;
using BotForgeAPI.Network.Messages;
using BotForge.Core.Game.Fight;
using BotForge.Core.Game.Fight.Fighters;
using Core.Engine.Types;
using BotForgeAPI.Game.Map;
using BotForgeAPI.Protocol.Types;

namespace BlueSheep.Engine.Handlers.Fight
{
    class FightHandler
    {
        #region Public methods
        [MessageHandler(typeof(GameActionFightDeathMessage))]
        public static void GameActionFightDeathMessageTreatment(Message message,  Account account)
        {
            GameActionFightDeathMessage msg = (GameActionFightDeathMessage)message;
            (account.Game.Fight.Data as FightData).SetFighterDeath((long)msg.TargetId);
        }

        [MessageHandler(typeof(GameActionFightDispellableEffectMessage))]
        public static void GameActionFightDispellableEffectMessageTreatment(Message message,  Account account)
        {
            GameActionFightDispellableEffectMessage msg = (GameActionFightDispellableEffectMessage)message;
            (account.Game.Fight.Data as FightData).SetEffect(msg.Effect, msg.ActionId);
        }

        [MessageHandler(typeof(GameActionFightPointsVariationMessage))]
        public static void GameActionFightPointsVariationMessageTreatment(Message message,  Account account)
        {
            GameActionFightPointsVariationMessage msg = (GameActionFightPointsVariationMessage)message;
            (account.Game.Fight.Data as FightData).SetPointsVariation((long)msg.TargetId, msg.ActionId, msg.Delta);
        }

        [MessageHandler(typeof(GameActionFightLifePointsLostMessage))]
        public static void GameActionFightLifePointsLostMessageTreatment(Message message,  Account account)
        {
            GameActionFightLifePointsLostMessage msg = (GameActionFightLifePointsLostMessage)message;
            (account.Game.Fight.Data as FightData).UpdateFighterLifePoints((long)msg.TargetId, -((int)msg.Loss));

        }

        [MessageHandler(typeof(GameActionFightSlideMessage))]
        public static void GameActionFightSlideMessageTreatment(Message message,  Account account)
        {
            GameActionFightSlideMessage msg = (GameActionFightSlideMessage)message;
            (account.Game.Fight.Data as FightData).UpdateFighterCell((long)msg.TargetId, msg.EndCellId);
        }

        [MessageHandler(typeof(GameActionFightSpellCastMessage))]
        public static void GameActionFightSpellCastMessageTreatment(Message message,  Account account)
        {
            GameActionFightSpellCastMessage msg = (GameActionFightSpellCastMessage)message;
            (account.Game.Fight.Data as FightData).SetSpellCasted((long)msg.SourceId, msg.SpellId, msg.DestinationCellId, msg.SpellLevel);
        }

        [MessageHandler(typeof(GameActionFightSummonMessage))]
        public static void GameActionFightSummonMessageTreatment(Message message,  Account account)
        {
            GameActionFightSummonMessage msg = (GameActionFightSummonMessage)message;
            msg.Summons.ToList().ForEach(summon =>
            (account.Game.Fight.Data as FightData).Summons.Add(
                    new Fighter(summon, account.Game.Fight.Data,
                                account.Game.Map.Data)));
        }

        [MessageHandler(typeof(GameActionFightTeleportOnSameMapMessage))]
        public static void GameActionFightTeleportOnSameMapMessageTreatment(Message message,  Account account)
        {
            GameActionFightTeleportOnSameMapMessage msg = (GameActionFightTeleportOnSameMapMessage)message;
            (account.Game.Fight.Data as FightData).UpdateFighterCell((long)msg.TargetId, msg.CellId);
        }

        [MessageHandler(typeof(GameEntitiesDispositionMessage))]
        public static void GameEntitiesDispositionMessageTreatment(Message message,  Account account)
        {
            GameEntitiesDispositionMessage msg = (GameEntitiesDispositionMessage)message;
            if (account.Game.Fight != null)
            {
                msg.Dispositions.ToList().ForEach(d =>
                {
                    (account.Game.Fight.Data as FightData).UpdateFighterCell((long)d.ObjectId, d.CellId);
                });
            }
            account.Game.Character.SetStatus(Status.Fighting);
        }

        [MessageHandler(typeof(GameFightEndMessage))]
        public async static void GameFightEndMessageTreatment(Message message,  Account account)
        {
            GameFightEndMessage msg = (GameFightEndMessage)message;
            (account.Game.Fight.Data as FightData).FightStop(msg);
            account.Game.Character.SetStatus(Status.None);
        }

        [MessageHandler(typeof(GameFightHumanReadyStateMessage))]
        public static void GameFightHumanReadyStateMessageTreatment(Message message,  Account account)
        {
            GameFightHumanReadyStateMessage msg = (GameFightHumanReadyStateMessage)message;
            if (msg.CharacterId == account.Game.Character.Id)
                (account.Game.Fight.Data as FightData).WaitForReady = !msg.IsReady;
        }

        [MessageHandler(typeof(GameFightJoinMessage))]
        public static void GameFightJoinMessageTreatment(Message message,  Account account)
        {
            GameFightJoinMessage msg = (GameFightJoinMessage)message;
            if (account.Game.Fight != null)
            {
                (account.Game.Fight.Data as FightData).Reset(msg.IsFightStarted, msg.CanSayReady);
                if (account.Settings.LockingFight && account.Game.Fight != null && !account.LockPerformed)
                {
                    //(account.Game.Fight.Data as FightData).PerformAutoTimeoutFight(2000);
                    account.Wait(2000, 2000);
                    account.Game.Fight.LockFight();
                    account.LockPerformed = true;
                }

            }
        }

        [MessageHandler(typeof(GameFightLeaveMessage))]
        public static void GameFightLeaveMessageTreatment(Message message,  Account account)
        {
            GameFightLeaveMessage msg = (GameFightLeaveMessage)message;

            //if (msg.CharId == account.Game.Character.Id)
            //{
            //    (account.Game.Fight as BotForge.Core.Game.Fight.Fight);
            //}
            // TODO Militão 2.0: Fight stop here
        }

        [MessageHandler(typeof(GameFightOptionStateUpdateMessage))]
        public static void GameFightOptionStateUpdateMessageTreatment(Message message,  Account account)
        {
            GameFightOptionStateUpdateMessage msg = (GameFightOptionStateUpdateMessage)message;
            (account.Game.Fight.Data as FightData).SetOption(msg.State, msg.Option);
        }

        [MessageHandler(typeof(GameFightShowFighterMessage))]
        public static void GameFightShowFighterMessageTreatment(Message message,  Account account)
        {
            GameFightShowFighterMessage msg = (GameFightShowFighterMessage)message;
            (account.Game.Fight.Data as FightData).AddFighter(msg.Informations);

        }

        [MessageHandler(typeof(GameFightShowFighterRandomStaticPoseMessage))]
        public static void GameFightShowFighterRandomStaticPoseMessageTreatment(Message message,  Account account)
        {
            GameFightShowFighterRandomStaticPoseMessage msg = (GameFightShowFighterRandomStaticPoseMessage)message;
            (account.Game.Fight.Data as FightData).AddFighter(msg.Informations);
        }

        [MessageHandler(typeof(GameFightStartMessage))]
        public static void GameFightStartMessageTreatment(Message message,  Account account)
        {
            GameFightStartMessage msg = (GameFightStartMessage)message;
            (account.Game.Fight.Data as FightData).FightStart();
        }

        [MessageHandler(typeof(GameFightSynchronizeMessage))]
        public static void GameFightSynchronizeMessageTreatment(Message message,  Account account)
        {
            GameFightSynchronizeMessage msg = (GameFightSynchronizeMessage)message;
            (account.Game.Fight.Data as FightData).ClearFighters();
            foreach (GameFightFighterInformations i in msg.Fighters)
                (account.Game.Fight.Data as FightData).AddFighter(i);
        }

        [MessageHandler(typeof(GameFightTurnEndMessage))]
        public static void GameFightTurnEndMessageTreatment(Message message,  Account account)
        {
            GameFightTurnEndMessage msg = (GameFightTurnEndMessage)message;
            if (msg.ObjectId == account.Game.Character.Id)
                (account.Game.Fight.Data as FightData).IsFighterTurn = false;
        }

        [MessageHandler(typeof(GameFightTurnStartMessage))]
        public static void GameFightTurnStartMessageTreatment(Message message,  Account account)
        {
            GameFightTurnStartMessage msg = (GameFightTurnStartMessage)message;
            //TODO Militão 2.0: Verify this

        }

        [MessageHandler(typeof(GameMapMovementMessage))]
        public static void GameMapMovementMessageTreatment(Message message,  Account account)
        {
            GameMapMovementMessage msg = (GameMapMovementMessage)message;
            MovementPath clientMovement = MapMovementAdapter.GetClientMovement(msg.KeyMovements.Select(k => (int)k).ToList());
            if (account.Game.Character.Status == Status.Fighting)
            {
                (account.Game.Fight.Data as FightData).UpdateFighterCell((long)msg.ActorId, clientMovement.CellEnd.CellId);
            }
            //account.ActualizeMap();
            // TODO Militão: Populate the new interface
        }

        [MessageHandler(typeof(GameFightNewRoundMessage))]
        public static void GameFightNewRoundMessageTreatment(Message message,  Account account)
        {
            GameFightNewRoundMessage msg = (GameFightNewRoundMessage)message;
            (account.Game.Fight.Data as FightData).UpdateTurn((int)msg.RoundNumber);
        }

        [MessageHandler(typeof(GameFightTurnStartPlayingMessage))]
        public static void GameFightTurnStartPlayingMessageTreatment(Message message,  Account account)
        {
            GameFightTurnStartPlayingMessage msg = (GameFightTurnStartPlayingMessage)message;
            (account.Game.Fight.Data as FightData).IsFighterTurn = true;
            if (account.Game.Fight != null)
            {
                account.Game.Fight.ExecutePlan();
            }
            else
                account.Logger.Log("AI is necessary to run fight", BotForgeAPI.Logger.LogEnum.TextInformationError);
        }

        [MessageHandler(typeof(GameFightPlacementPossiblePositionsMessage))]
        public static void GameFightPlacementPossiblePositionsMessageTreatment(Message message,  Account account)
        {
            GameFightPlacementPossiblePositionsMessage msg = (GameFightPlacementPossiblePositionsMessage)message;
            account.Game.Character.SetStatus(Status.Fighting);
            (account.Game.Fight.Data as FightData).UpdateTurn(0);
            if (account.Game.Fight != null)
            {
                account.Game.Fight.PlaceCharacter((ushort)msg.PositionsForChallengers.Select(item => (int)item).FirstOrDefault());
            }
            if (account.StartFightWithItemSet)
            {
                byte id = account.PresetStartUpId;
                InventoryPresetUseMessage msg2 = new InventoryPresetUseMessage((sbyte)(id - 1));
                account.Network.Send(msg2);
                account.Logger.Log("Fast equipment number " + Convert.ToString(id), BotForgeAPI.Logger.LogEnum.TextInformationMessage);
            }
            GameFightReadyMessage nmsg = new GameFightReadyMessage(true);
            account.Network.Send(nmsg);
            account.Logger.Log("Send Ready !", BotForgeAPI.Logger.LogEnum.Bot);
        }

        [MessageHandler(typeof(GameFightTurnReadyRequestMessage))]
        public static void GameFightTurnReadyRequestMessageTreatment(Message message,  Account account)
        {
            GameFightTurnReadyRequestMessage msg = (GameFightTurnReadyRequestMessage)message;
            GameFightTurnReadyMessage msg2 = new GameFightTurnReadyMessage(true);
            account.Network.Send(msg2);
        }

        [MessageHandler(typeof(SequenceEndMessage))]
        public static void SequenceEndMessageTreatment(Message message,  Account account)
        {
            SequenceEndMessage msg = (SequenceEndMessage)message;
            
            if ((account.Game.Fight.Data as FightData).Fighter.IsAlive && (account.Game.Fight.Data as FightData).Fighter.Id == msg.AuthorId && !account.IsFullSocket && account.Game.Fight != null && (account.Game.Fight.Data as FightData).IsFighterTurn)
            {
                GameActionAcknowledgementMessage msg2 = new GameActionAcknowledgementMessage(true, (sbyte)msg.ActionId);
                account.Network.Send(msg2);
                //switch (account.Game.Fight.flag)
                //{
                //    case -1:
                //        account.Fight.EndTurn();
                //        break;
                //    case 1:
                //        account.Fight.ExecutePlan();
                //        break;
                //}
                //TODO Militão 2.0: Get Flag.
            }
        }

        [MessageHandler(typeof(LifePointsRegenBeginMessage))]
        public static void LifePointsRegenBeginMessageTreatment(Message message,  Account account)
        {
            LifePointsRegenBeginMessage msg = (LifePointsRegenBeginMessage)message;
        }

        [MessageHandler(typeof(LifePointsRegenEndMessage))]
        public static void LifePointsRegenEndMessageTreatment(Message message,  Account account)
        {
            LifePointsRegenEndMessage msg = (LifePointsRegenEndMessage)message;
            int percent = ((int)msg.LifePoints / (int)msg.MaxLifePoints) * 100;
            account.Logger.Log("End Regenerating. + " + msg.LifePointsGained + " life points", BotForgeAPI.Logger.LogEnum.Bot);
            account.ModifBar(2, (int)msg.MaxLifePoints, (int)msg.LifePoints, "Vitalité");
        }

        [MessageHandler(typeof(GameFightSpectatorJoinMessage))]
        public static void GameFightSpectatorJoinMessageTreatment(Message message,  Account account)
        {
            GameFightSpectatorJoinMessage msg = (GameFightSpectatorJoinMessage)message;

            if (account.LockingSpectators)
            {
                account.Game.Fight.ToggleOption(BotForgeAPI.Protocol.Enums.FightOptionsEnum.FIGHT_OPTION_SET_SECRET);
            }
        }

        [MessageHandler(typeof(GameActionFightLifePointsGainMessage))]
        public static void GameActionFightLifePointsGainMessageTreatment(Message message,  Account account)
        {
            GameActionFightLifePointsGainMessage msg = (GameActionFightLifePointsGainMessage)message;
            if (account.Game.Character.Status == Status.Fighting)
            {
                if (msg.ActionId == 108) // HP Récupérés (delta = combien on a récupérés)
                {
                    (account.Game.Fight.Data as FightData).UpdateFighterLifePoints((long)msg.TargetId, (int)msg.Delta);
                }
            }
        }

        [MessageHandler(typeof(GameActionFightTackledMessage))]
        public static void GameActionFightTackledMessageTreatment(Message message,  Account account)
        {
            GameActionFightTackledMessage msg = (GameActionFightTackledMessage)message;
            if (account.Game.Fight != null )//&& account.Fight.flag == -1)
                account.Game.Fight.EndTurn();
            //TODO Militão 2.0: Get Flag
        }
        #endregion
    }
}

