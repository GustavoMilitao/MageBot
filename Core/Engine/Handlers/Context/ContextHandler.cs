using BlueSheep.Common.Data;
using BlueSheep.Common.Data.D2o;
using BlueSheep.Util.IO;
using BlueSheep.Data.Pathfinding;
using BlueSheep.Data.Pathfinding.Positions;
using BlueSheep.Util.Text.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using BotForgeAPI.Protocol.Messages;
using BotForgeAPI.Network.Messages;
using Core.Engine.Types;
using BotForge.Core.Game.Gather;
using BotForge.Core.Game.Map;
using BotForgeAPI.Game.Map;
using BotForge.Core.Game;
using BotForge.Core.Game.Path;
using BotForgeAPI.Protocol.Types;
using BotForge.Core.Game.Inventory;

namespace Core.Engine.Handlers.Context
{
    class ContextHandler
    {
        #region Public methods
        [MessageHandler(typeof(MapComplementaryInformationsDataInHouseMessage))]
        public static void MapComplementaryInformationsDataInHouseMessageTreatment(Message message, Account account)
        {
            MapComplementaryInformationsDataMessageTreatment(message, account);
        }

        [MessageHandler(typeof(MapComplementaryInformationsDataMessage))]
        public static void MapComplementaryInformationsDataMessageTreatment(Message message, Account account)
        {
            MapComplementaryInformationsDataMessage msg = (MapComplementaryInformationsDataMessage)message;
            //account.Game..Config.HeroicConfig.AnalysePacket(message, packetDatas);
            (account.Game.Gather.Data as GatherData).Clear();
            (account.Game.Map.Data as MapData).Clear();
            (account.Game.Map.Data as MapData).ParseLocation(msg.MapId, msg.SubAreaId);
            (account.Game.Map.Data as MapData).ParseStatedElements(msg.StatedElements.ToArray());
            (account.Game.Map.Data as MapData).ParseActors(msg.Actors.ToArray());
            (account.Game.Map.Data as MapData).ParseInteractiveElements(msg.InteractiveElements.ToArray());
            //account.Settings.a.Enabled = true;
            (account.Game.Map.Data as MapData).DoAction();
            //account.ActualizeMap();
            // TODO Militão: Populate the new interface
        }

        [MessageHandler(typeof(MapComplementaryInformationsWithCoordsMessage))]
        public static void MapComplementaryInformationsWithCoordsMessageTreatment(Message message, Account account)
        {
            MapComplementaryInformationsDataMessageTreatment(message, account);
        }

        [MessageHandler(typeof(CurrentMapMessage))]
        public static void CurrentMapMessageTreatment(Message message, Account account)
        {
            CurrentMapMessage currentMapMessage = (CurrentMapMessage)message;

            account.Logger.Log("[Map] = " + currentMapMessage.MapId, BotForgeAPI.Logger.LogEnum.Debug);
            account.Game.Character.SetStatus(Status.None);
            //if (account.Game.Map.Data != null)
            //{
            //    (account.Game as Game) = currentMapMessage.MapId;
            //    //if (account.MapID == account.MapData.LastMapId && account.Fight != null)
            //    //{
            //    //    account.FightData.winLoseDic["Gagné"]++;
            //    //    account.ActualizeFightStats(account.FightData.winLoseDic, account.FightData.xpWon);
            //    //}
            //}
            if (account.IsFullSocket)
            {
                MapInformationsRequestMessage mapInformationsRequestMessage
                = new MapInformationsRequestMessage(currentMapMessage.MapId);
                account.Network.Connection.Send(mapInformationsRequestMessage);
            }
        }

        [MessageHandler(typeof(GameContextCreateMessage))]
        public static void GameContextCreateMessageTreatment(Message message, Account account)
        {
        }

        [MessageHandler(typeof(QuestListMessage))]
        public static void QuestListMessageTreatment(Message message, Account account)
        {
            if (account.IsFullSocket)
            {
                MapInformationsRequestMessage mapInformationsRequestMessage
                    = new MapInformationsRequestMessage(account.Game.Map.Data.Id);

                account.Network.Connection.Send(mapInformationsRequestMessage);
            }

        }

        [MessageHandler(typeof(TextInformationMessage))]
        public static void TextInformationMessageTreatment(Message message, Account account)
        {
            TextInformationMessage msg = (TextInformationMessage)message;
            switch (msg.MsgId)
            {
                //case 89:
                //    account.Log(new DofAlertTextInformation("Bienvenue sur DOFUS, dans le Monde des Douze !" + System.Environment.NewLine + "Il est interdit de transmettre votre identifiant ou votre mot de passe."), 1);
                //    break;
                //case 153:
                //    account.Log(new DofInfoCanal("Votre adresse ip actuelle est " + msg.parameters[0]), 0);
                //    break;
                //case 171:
                //    account.Log(new ErrorTextInformation(string.Format("Impossible de lancer ce sort, vous avez une portée de {0} à {1}, et vous visez à {2} !", msg.parameters[0], msg.parameters[1], msg.parameters[2])), 4);
                //    break;
                case 36:
                    if (account.LockingSpectators)
                        account.LockFightForSpectators();
                    break;
                case 34:
                    //account.Log(new ErrorTextInformation(string.Format("Vous avez perdu {0} points d'énergie", msg.parameters[0])), 0);
                    //account.Log(new ErrorTextInformation("Combat perdu"), 0);
                    if (account.Game.Fight != null)
                    {
                        //account.Game..FightData.winLoseDic["Perdu"]++;
                        //account.ActualizeFightStats(account.FightData.winLoseDic, account.FightData.xpWon);
                        // TODO Militão: Populate the new interface
                    }
                    break;
            }
            //default:


            DataClass data = GameData.GetDataObject(D2oFileEnum.InfoMessages, msg.MsgType * 10000 + msg.MsgId);
            string text = I18N.GetText((int)data.Fields["textId"]);
            for (int i = 0; i < msg.Parameters.Count(); i++)
            {
                var parameter = msg.Parameters[i];
                text = text.Replace("%" + (i + 1), parameter);
            }
            account.Logger.Log(text, BotForgeAPI.Logger.LogEnum.TextInformationMessage);
        }

        [MessageHandler(typeof(ChatServerMessage))]
        public static void ChatServerMessageTreatment(Message message, Account account)
        {
            ChatServerMessage msg = (ChatServerMessage)message;

            switch (msg.Channel)
            {
                case 0:
                    account.Logger.Log(msg.SenderName + ": " + msg.Content, BotForgeAPI.Logger.LogEnum.ChannelGlobal);
                    break;
                case 1:
                    //account.Log(new DofAlertTextInformation("Bienvenue sur DOFUS, dans le Monde des Douze !" + System.Environment.NewLine + "Il est interdit de transmettre votre identifiant ou votre mot de passe."));
                    break;
                case 2:
                    account.Logger.Log(msg.SenderName + ": " + msg.Content, BotForgeAPI.Logger.LogEnum.ChannelGuild);
                    break;
                case 3:
                    account.Logger.Log(msg.SenderName + ": " + msg.Content, BotForgeAPI.Logger.LogEnum.ChannelAlliance);
                    break;
                case 5:
                    account.Logger.Log(msg.SenderName + ": " + msg.Content, BotForgeAPI.Logger.LogEnum.ChannelGlobal);
                    break;
                case 6:
                    account.Logger.Log(msg.SenderName + ": " + msg.Content, BotForgeAPI.Logger.LogEnum.ChannelGlobal);
                    break;
                case 9:
                    account.Logger.Log("de " + msg.SenderName + " : " + msg.Content, BotForgeAPI.Logger.LogEnum.PseudoChannelPrivate);
                    break;
            }
        }

        //[MessageHandler(typeof(GameMapMovementConfirmMessage))]
        //public static void GameMapMovementConfirmMessageTreatment(Message message,  Account account)
        //{
        //    GameMapMovementConfirmMessage msg = (GameMapMovementConfirmMessage)message;

        //    using (BigEndianReader reader = new BigEndianReader(packetDatas))
        //    {
        //        msg.Deserialize(reader);
        //    }
        //    BlueSheep.Core.Fight.Entity Character = null;
        //    foreach (BlueSheep.Core.Fight.Entity e in account.Map.Entities)
        //    {
        //        if (e.Id == account.CharacterBaseInformations.id)
        //            Character = e;
        //    }
        //    int mapChangeData = ((BlueSheep.Data.D2p.Map)account.Map.Data).Cells[Character.CellId].MapChangeData;
        //    if (mapChangeData != 0)
        //    {
        //        int neighbourId = 0;
        //        if (neighbourId == -2)
        //        {
        //            if ((mapChangeData & 64) > 0)
        //                neighbourId = ((BlueSheep.Data.D2p.Map)account.Map.Data).TopNeighbourId;
        //            if ((mapChangeData & 16) > 0)
        //                neighbourId = ((BlueSheep.Data.D2p.Map)account.Map.Data).LeftNeighbourId;
        //            if ((mapChangeData & 4) > 0)
        //                neighbourId = ((BlueSheep.Data.D2p.Map)account.Map.Data).BottomNeighbourId;
        //            if ((mapChangeData & 1) > 0)
        //                neighbourId = ((BlueSheep.Data.D2p.Map)account.Map.Data).RightNeighbourId;
        //        }
        //        if (neighbourId >= 0)
        //            account.Map.LaunchChangeMap(neighbourId);
        //    }
        //    account.SetStatus(Status.None);

        //}

        [MessageHandler(typeof(GameMapMovementMessage))]
        public static void GameMapMovementMessageTreatment(Message message, Account account)
        {
            GameMapMovementMessage msg = (GameMapMovementMessage)message;

            List<int> keys = new List<int>();
            foreach (int s in msg.KeyMovements)
            {
                keys.Add(s);
            }

            MovementPath clientMovement = MapMovementAdapter.GetClientMovement(keys);
            (account.Game.Map.Data as MapData).UpdateEntityCell(msg.ActorId, clientMovement.CellEnd.CellId);
        }

        [MessageHandler(typeof(GameMapNoMovementMessage))]
        public static void GameMapNoMovementMessageTreatment(Message message, Account account)
        {
            GameMapNoMovementMessage msg = (GameMapNoMovementMessage)message;

            //if (account.Fight != null && account.FightData.IsFollowingGroup)
            //{
            //    account.Fight.LaunchFight(account.FightData.followingGroup.m_contextualId);
            //    return;
            //}
            //if (account.Path != null)
            //{
            //    account.SetStatus(Status.None);
            //    account.Log(new DebugTextInformation("[Path] NoMovement : Continue the path..."), 0);
            //    account.Path.PerformActionsStack();
            //}
            //account.Map.Moving = false;
            //account.Map.ConfirmMove();
            if (account.Path != null)
                (account.Path as Path).Start();

            //else if (account.Map.Moving)
            //{
            //    account.SetStatus(Status.None);
            //    if (account.Path != null)
            //        account.Path.ParsePath();
            //}

        }

        [MessageHandler(typeof(PopupWarningMessage))]
        public static void PopupWarningMessageTreatment(Message message, Account account)
        {
            PopupWarningMessage msg = (PopupWarningMessage)message;

            account.Logger.Log("[FROM " + msg.Author + " ] : " + msg.Content, BotForgeAPI.Logger.LogEnum.TextInformationError);
            account.Logger.Log("You has been locked for " + msg.LockDuration + ". Stopping BlueSheep actions while blocked...", BotForgeAPI.Logger.LogEnum.Info);
            account.Logger.Log("Y a un popup sur l'écran, surement un modo :s", BotForgeAPI.Logger.LogEnum.TextInformationMessage);
            //account.Wait(msg.LockDuration, msg.LockDuration);
            account.Wait(msg.LockDuration, msg.LockDuration);
            //account.SocketManager.Disconnect("Alerte au modo ! Alerte au modo !");
        }

        [MessageHandler(typeof(SystemMessageDisplayMessage))]
        public static void SystemMessageDisplayMessageTreatment(Message message, Account account)
        {
            SystemMessageDisplayMessage msg = (SystemMessageDisplayMessage)message;

            msg.Parameters.ToList().ForEach(item => account.Logger.Log(item, BotForgeAPI.Logger.LogEnum.Info));
            // account.SocketManager.Disconnect("Alerte au modo ! Alerte au modo !");
        }


        [MessageHandler(typeof(PartyInvitationMessage))]
        public static void PartyInvitationMessageTreatment(Message message, Account account)
        {
            PartyInvitationMessage msg = (PartyInvitationMessage)message;

            BotForge.Core.Account.Group g = (account.Group as BotForge.Core.Account.Group);
            if (account.Group != null && msg.FromName == account.Group.Leader.Game.Character.Name)
            {

                PartyAcceptInvitationMessage msg2 = new PartyAcceptInvitationMessage(msg.PartyId);
                account.Network.Connection.Send(msg2);
                account.Logger.Log("J'ai rejoint le groupe :3", BotForgeAPI.Logger.LogEnum.Bot);

            }
            else
            {
                PartyRefuseInvitationMessage msg2 = new PartyRefuseInvitationMessage(msg.PartyId);
                account.Network.Connection.Send(msg2);

            }
        }

        [MessageHandler(typeof(PartyMemberInFightMessage))]
        public static void PartyMemberInFightMessageTreatment(Message message, Account account)
        {
            PartyMemberInFightMessage msg = (PartyMemberInFightMessage)message;

            if (msg.FightMap.MapId == account.Game.Map.Data.Id && msg.MemberName == account.Group.Leader.Game.Character.Name)
            {
                //account.Wait(500, 1500);
                account.Wait(1500, 1500);
                using (BigEndianWriter writer = new BigEndianWriter())
                {
                    GameFightJoinRequestMessage msg2 = new GameFightJoinRequestMessage(msg.MemberId, msg.FightId);
                    account.Network.Connection.Send(msg2);
                }
            }

        }

        [MessageHandler(typeof(InteractiveElementUpdatedMessage))]
        public static void InteractiveElementUpdatedMessageTreatment(Message message, Account account)
        {
            InteractiveElementUpdatedMessage msg = (InteractiveElementUpdatedMessage)message;

            //if (account.Game.BidHouse != null)
            //{
            //    InteractiveElement e = msg.InteractiveElement;
            //    account.Game.BidHouse.Data..ElementIdd = e.ElementId;
            //    List<InteractiveElementSkill> EnabledSkills = e.EnabledSkills;
            //    account.House.SkillInstanceID = EnabledSkills[1].SkillInstanceUid;
            //    account.House.UseHouse();
            //}
            (account.Game.Map.Data as MapData).UpdateInteractiveElement(msg.InteractiveElement);

        }

        [MessageHandler(typeof(StatedElementUpdatedMessage))]
        public static void StatedElementUpdatedMessageTreatment(Message message, Account account)
        {
            StatedElementUpdatedMessage msg = (StatedElementUpdatedMessage)message;

            //(account.Game.Map.Data as MapData).UpdateStatedElement(msg.StatedElement);
        }

        [MessageHandler(typeof(PurchasableDialogMessage))]
        public static void PurchasableDialogMessageTreatment(Message message, Account account)
        {
            PurchasableDialogMessage msg = (PurchasableDialogMessage)message;

            //if (account.House != null)
            //{
            //    account.House.priceHouse = msg.Price;
            //    if (account.House.priceHouse < account.Config.MaxPriceHouse)
            //    {
            //        account.House.Buy();
            //    }
            //    else
            //    {
            //        account.Log(new ErrorTextInformation("Prix trop élevé..."), 2);
            //    }
            //}
        }

        [MessageHandler(typeof(HousePropertiesMessage))]
        public static void HousePropertiesMessageTreatment(Message message, Account account)
        {
            //HousePropertiesMessage msg = (HousePropertiesMessage)message;

            //using (BigEndianReader reader = new BigEndianReader(packetDatas))
            //{
            //    msg.Deserialize(reader);
            //}
            //if (!String.IsNullOrEmpty(account.Config.HouseSearcherLogPath))
            //{
            //    StreamWriter SourceFile = new StreamWriter(account.Config.HouseSearcherLogPath, true);
            //    SourceFile.WriteLine("Abandoned house in : " + "[" + account.MapData.X + ";" + account.MapData.Y + "]");
            //    SourceFile.Close();
            //}
            //TODO Militão: Treat this
            //account.Log(new BotTextInformation("Maison abandonnée en : " + "[" + account.MapData.X + ";" + account.MapData.Y + "]"), 1);
        }

        [MessageHandler(typeof(GameContextRemoveElementMessage))]
        public static void GameContextRemoveElementMessageTreatment(Message message, Account account)
        {
            GameContextRemoveElementMessage msg = (GameContextRemoveElementMessage)message;

            account.Game.Map.Data.InteractiveElements.Remove(
                account.Game.Map.Data.InteractiveElements.FirstOrDefault(ielem => ielem.Id == msg.ObjectId));
        }

        [MessageHandler(typeof(GameRolePlayShowActorMessage))]
        public static void GameRolePlayShowActorMessageTreatment(Message message, Account account)
        {
            GameRolePlayShowActorMessage msg = (GameRolePlayShowActorMessage)message;
            //account.Config.HeroicConfig.AnalysePacket(msg, packetDatas);

            (account.Game.Map.Data as MapData).ParseActors(new List<GameRolePlayActorInformations>() { msg.Informations }.ToArray());

            if (account.Game.Tchat != null && msg.Informations is GameRolePlayCharacterInformations)
            {
                GameRolePlayCharacterInformations infos = (GameRolePlayCharacterInformations)msg.Informations;
                account.Game.Tchat.SendMP(infos.Name, account.Game.Tchat.Data.Content);
            }
            //if  && msg.Informations is GameRolePlayCharacterInformations)
            //{
            //    GameRolePlayCharacterInformations infos = (GameRolePlayCharacterInformations)msg.Informations;
            //    account.Config.Flood.SaveNameInMemory(infos);
            //}
        }

        [MessageHandler(typeof(ExchangeStartedWithStorageMessage))]
        public static void ExchangeStartedWithStorageMessageTreatment(Message message, Account account)
        {
            ExchangeStartedWithStorageMessage msg = (ExchangeStartedWithStorageMessage)message;

            if (account.Path != null && account.Game.Inventory != null)
            {
                List<int> items = (account.Game.Inventory.Data as BotForge.Core.Game.Inventory.InventoryData).GetItemsToTransfer();
                account.Game.Inventory.TransferItems(items);
                //account.Game.Inventory.GetItems(account.Game.Inventory.items.ItemsToGetFromBank.Select(item => item.UID).ToList());
            }
            // Get and put items from/to bank
        }

        [MessageHandler(typeof(DisplayNumericalValuePaddockMessage))]
        public static void DisplayNumericalValueMessageTreatment(Message message, Account account)
        {
            DisplayNumericalValuePaddockMessage msg = (DisplayNumericalValuePaddockMessage)message;

        }

        [MessageHandler(typeof(ObtainedItemMessage))]
        public static void ObtainedItemMessageTreatment(Message message, Account account)
        {
            ObtainedItemMessage msg = (ObtainedItemMessage)message;

            //if ((account.Game.Gather.Data as GatherData)..Current_El == null)
            //    return;
            //account.Logger.Log("Ressource récoltée : " + account.Game.Gather.resourceName + " +" + msg.BaseQuantity, BotForgeAPI.Logger.LogEnum.TextInformationMessage);
            //TODO Militão 2.0: GatherData.GatheredElement Action
            //if ((account.Game.Gather.Data as GatherData).Stats.ContainsKey(account.Gather.resourceName))
            //    account.Gather.Stats[account.Gather.resourceName] += (int)msg.BaseQuantity;
            //else
            //    account.Gather.Stats.Add(account.Gather.resourceName, (int)msg.BaseQuantity);
            //account.Gather.Current_Job.ActualizeStats(account.Gather.Stats);
        }

        [MessageHandler(typeof(InteractiveUseErrorMessage))]
        public static void InteractiveUseErrorMessageTreatment(Message message, Account account)
        {
            InteractiveUseErrorMessage msg = (InteractiveUseErrorMessage)message;

            //if (account.Gather.Error())
            //    return;
            (account.Game.Gather.Data as GatherData).Ban(msg.ElemId);
            //account.Log(new ErrorTextInformation("Erreur lors de l'utilisation de l'element numero " + msg.elemId + ". Pg lelz. Poursuite du trajet."), 0);
            if (account.Path != null)
                account.Path.Perform();
        }

        [MessageHandler(typeof(InteractiveUseEndedMessage))]
        public static void InteractiveUseEndedMessageTreatment(Message message, Account account)
        {
            InteractiveUseEndedMessage msg = (InteractiveUseEndedMessage)message;

            //if (account.Game.Gather.Id == -1)
            //    return;
            account.Game.Character.SetStatus(Status.None);
            //account.Gather.Id = -1;
            if (account.Path != null)
                account.Path.Perform();
        }

        [MessageHandler(typeof(ExchangeStartedWithPodsMessage))]
        public static void ExchangeStartedWithPodsMessageTreatment(Message message, Account account)
        {
            ExchangeStartedWithPodsMessage msg = (ExchangeStartedWithPodsMessage)message;

            //if ((!account.Game.Inventory.Data as InventoryData).ListeningToExchange)
            //    return;
            //TODO Militão 2.0: Verify listen to exchange
            List<int> items = (account.Game.Inventory.Data as InventoryData).GetItemsToTransfer();
            account.Game.Inventory.TransferItems(items);
            account.Wait(3000, 3000);
            account.Game.Inventory.ExchangeReady();
        }

        [MessageHandler(typeof(ExchangeRequestedTradeMessage))]
        public static void ExchangeRequestedTradeMessageTreatment(Message message, Account account)
        {
            ExchangeRequestedTradeMessage msg = (ExchangeRequestedTradeMessage)message;

            //if (account.Game.Inventory.ListeningToExchange)
            //    account.Inventory.AcceptExchange();
            //TODO Militão 2.0: Verify listen to exchange
        }

        [MessageHandler(typeof(ExchangeIsReadyMessage))]
        public static void ExchangeIsReadyMessageTreatment(Message message, Account account)
        {
            ExchangeIsReadyMessage msg = (ExchangeIsReadyMessage)message;

            //if (msg.Ready && account.Game.Inventory.ListeningToExchange)
            //    account.Inventory.ExchangeReady();
            //TODO Militão 2.0: Verify listen to exchange
        }


        #endregion
    }
}
