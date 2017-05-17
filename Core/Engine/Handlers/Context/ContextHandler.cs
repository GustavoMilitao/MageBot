using MageBot.DataFiles.Data.D2o;
using MageBot.Util.IO;
using MageBot.Protocol.Messages.Game.Basic;
using MageBot.Protocol.Messages.Game.Chat;
using MageBot.Protocol.Messages.Game.Context;
using MageBot.Protocol.Messages.Game.Context.Display;
using MageBot.Protocol.Messages.Game.Context.Fight;
using MageBot.Protocol.Messages.Game.Context.Roleplay;
using MageBot.Protocol.Messages.Game.Context.Roleplay.Houses;
using MageBot.Protocol.Messages.Game.Context.Roleplay.Party;
using MageBot.Protocol.Messages.Game.Context.Roleplay.Purchasable;
using MageBot.Protocol.Messages.Game.Context.Roleplay.Quest;
using MageBot.Protocol.Messages.Game.Interactive;
using MageBot.Protocol.Messages.Game.Inventory.Exchanges;
using MageBot.Protocol.Messages.Game.Inventory.Items;
using MageBot.Protocol.Messages.Game.Moderation;
using MageBot.Protocol.Messages.Server.Basic;
using MageBot.Protocol.Types.Game.Context.Roleplay;
using MageBot.Protocol.Types.Game.Interactive;
using MageBot.DataFiles.Data.Pathfinding;
using MageBot.DataFiles.Data.Pathfinding.Positions;
using MageBot.Util.Enums.Internal;
using MageBot.Protocol.Messages;
using Util.Util.Text.Log;
using System.Collections.Generic;
using System.Linq;

namespace MageBot.Core.Engine.Handlers.Context
{
    class ContextHandler
    {
        #region Public methods
        [MessageHandler(typeof(MapComplementaryInformationsDataInHouseMessage))]
        public static void MapComplementaryInformationsDataInHouseMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            MapComplementaryInformationsDataMessageTreatment(message, packetDatas, account);
        }

        [MessageHandler(typeof(MapComplementaryInformationsDataMessage))]
        public static void MapComplementaryInformationsDataMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            MapComplementaryInformationsDataMessage msg = (MapComplementaryInformationsDataMessage)message;
            account.Config.HeroicConfig.AnalysePacket(message, packetDatas);
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.Gather.ClearError();
            account.MapData.Clear();
            account.MapData.ParseLocation(msg.MapId, msg.SubAreaId);
            account.MapData.ParseStatedElements(msg.StatedElements.ToArray());
            account.MapData.ParseActors(msg.Actors.ToArray());
            account.MapData.ParseInteractiveElements(msg.InteractiveElements.ToArray());
            account.Config.Enabled = true;
            account.MapData.DoAction();
            //account.ActualizeMap();
            // TODO Militão: Populate the new interface
        }

        [MessageHandler(typeof(MapComplementaryInformationsWithCoordsMessage))]
        public static void MapComplementaryInformationsWithCoordsMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            MapComplementaryInformationsDataMessageTreatment(message, packetDatas, account);
        }

        [MessageHandler(typeof(CurrentMapMessage))]
        public static void CurrentMapMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            CurrentMapMessage currentMapMessage = (CurrentMapMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                currentMapMessage.Deserialize(reader);
            }

            account.Log(new DebugTextInformation("[Map] = " + currentMapMessage.MapId), 0);
            account.SetStatus(Status.None);
            if (account.MapData.Data != null)
            {
                account.MapData.Data.Id = currentMapMessage.MapId;
                //if (account.MapID == account.MapData.LastMapId && account.Fight != null)
                //{
                //    account.FightData.winLoseDic["Gagné"]++;
                //    account.ActualizeFightStats(account.FightData.winLoseDic, account.FightData.xpWon);
                //}
            }
            if (!account.Config.IsMITM)
            {
                MapInformationsRequestMessage mapInformationsRequestMessage
                = new MapInformationsRequestMessage(currentMapMessage.MapId);
                account.SocketManager.Send(mapInformationsRequestMessage);
            }
        }

        [MessageHandler(typeof(GameContextCreateMessage))]
        public static void GameContextCreateMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
        }

        [MessageHandler(typeof(QuestListMessage))]
        public static void QuestListMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            if (!account.Config.IsMITM)
            {
                MapInformationsRequestMessage mapInformationsRequestMessage
                    = new MapInformationsRequestMessage(account.MapData.Id);

                account.SocketManager.Send(mapInformationsRequestMessage);
            }

        }

        [MessageHandler(typeof(TextInformationMessage))]
        public static void TextInformationMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            TextInformationMessage msg = (TextInformationMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
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
                    if (account.Config.LockingSpectators)
                        account.Fight.LockFightForSpectators();
                    break;
                case 34:
                    //account.Log(new ErrorTextInformation(string.Format("Vous avez perdu {0} points d'énergie", msg.parameters[0])), 0);
                    //account.Log(new ErrorTextInformation("Combat perdu"), 0);
                    if (account.Fight != null)
                    {
                        account.FightData.WinLoseDic["Perdu"]++;
                        //account.ActualizeFightStats(account.FightData.winLoseDic, account.FightData.xpWon);
                        // TODO Militão: Populate the new interface
                    }
                    break;
            }
            //default:


            DataClass data = GameData.GetDataObject(D2oFileEnum.InfoMessages, msg.MsgType * 10000 + msg.MsgId);
            string text = MageBot.DataFiles.Data.I18n.I18N.GetText((int)data.Fields["textId"]);
            for (int i = 0; i < msg.Parameters.Count; i++)
            {
                var parameter = msg.Parameters[i];
                text = text.Replace("%" + (i + 1), parameter);
            }
            account.Log(new DofAlertTextInformation(text), 0);
        }

        [MessageHandler(typeof(ChatServerMessage))]
        public static void ChatServerMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            ChatServerMessage msg = (ChatServerMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            switch (msg.Channel)
            {
                case 0:
                    account.Log(new GeneralTextInformation(msg.SenderName + ": " + msg.Content), 1);
                    break;
                case 1:
                    //account.Log(new DofAlertTextInformation("Bienvenue sur DOFUS, dans le Monde des Douze !" + System.Environment.NewLine + "Il est interdit de transmettre votre identifiant ou votre mot de passe."));
                    break;
                case 2:
                    account.Log(new GuildTextInformation(msg.SenderName + ": " + msg.Content), 1);
                    break;
                case 3:
                    account.Log(new AllianceTextInformation(msg.SenderName + ": " + msg.Content), 1);
                    break;
                case 5:
                    account.Log(new CommerceTextInformation(msg.SenderName + ": " + msg.Content), 1);
                    break;
                case 6:
                    account.Log(new RecrutementTextInformation(msg.SenderName + ": " + msg.Content), 1);
                    break;
                case 9:
                    account.Log(new PrivateTextInformation("de " + msg.SenderName + " : " + msg.Content), 1);
                    break;
            }
        }

        //[MessageHandler(typeof(GameMapMovementConfirmMessage))]
        //public static void GameMapMovementConfirmMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        //{
        //    GameMapMovementConfirmMessage msg = (GameMapMovementConfirmMessage)message;

        //    using (BigEndianReader reader = new BigEndianReader(packetDatas))
        //    {
        //        msg.Deserialize(reader);
        //    }
        //    MageBot.Core.Fight.Entity Character = null;
        //    foreach (MageBot.Core.Fight.Entity e in account.Map.Entities)
        //    {
        //        if (e.Id == account.CharacterBaseInformations.id)
        //            Character = e;
        //    }
        //    int mapChangeData = ((MageBot.DataFiles.Data.D2p.Map)account.Map.Data).Cells[Character.CellId].MapChangeData;
        //    if (mapChangeData != 0)
        //    {
        //        int neighbourId = 0;
        //        if (neighbourId == -2)
        //        {
        //            if ((mapChangeData & 64) > 0)
        //                neighbourId = ((MageBot.DataFiles.Data.D2p.Map)account.Map.Data).TopNeighbourId;
        //            if ((mapChangeData & 16) > 0)
        //                neighbourId = ((MageBot.DataFiles.Data.D2p.Map)account.Map.Data).LeftNeighbourId;
        //            if ((mapChangeData & 4) > 0)
        //                neighbourId = ((MageBot.DataFiles.Data.D2p.Map)account.Map.Data).BottomNeighbourId;
        //            if ((mapChangeData & 1) > 0)
        //                neighbourId = ((MageBot.DataFiles.Data.D2p.Map)account.Map.Data).RightNeighbourId;
        //        }
        //        if (neighbourId >= 0)
        //            account.Map.LaunchChangeMap(neighbourId);
        //    }
        //    account.SetStatus(Status.None);

        //}

        [MessageHandler(typeof(GameMapMovementMessage))]
        public static void GameMapMovementMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameMapMovementMessage msg = (GameMapMovementMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            List<int> keys = new List<int>();
            foreach (int s in msg.KeyMovements)
            {
                keys.Add(s);
            }

            MovementPath clientMovement = MapMovementAdapter.GetClientMovement(keys);
            account.MapData.UpdateEntityCell(msg.ActorId, clientMovement.CellEnd.CellId);
        }

        [MessageHandler(typeof(GameMapNoMovementMessage))]
        public static void GameMapNoMovementMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameMapNoMovementMessage msg = (GameMapNoMovementMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
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
            if (account.Config.Path != null)
                account.Config.Path.PerformActionsStack();

            //else if (account.Map.Moving)
            //{
            //    account.SetStatus(Status.None);
            //    if (account.Path != null)
            //        account.Path.ParsePath();
            //}

        }

        [MessageHandler(typeof(PopupWarningMessage))]
        public async static void PopupWarningMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            PopupWarningMessage msg = (PopupWarningMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.Log(new ErrorTextInformation("[FROM " + msg.Author + " ] : " + msg.Content), 0);
            account.Log(new BotTextInformation("You has been locked for " + msg.LockDuration + ". Stopping MageBot.actions while blocked..."), 0);
            account.Log(new ErrorTextInformation("Y a un popup sur l'écran, surement un modo :s"), 0);
            //account.Wait(msg.LockDuration, msg.LockDuration);
            await account.PutTaskDelay(msg.LockDuration);
            //account.SocketManager.Disconnect("Alerte au modo ! Alerte au modo !");
        }

        [MessageHandler(typeof(SystemMessageDisplayMessage))]
        public static void SystemMessageDisplayMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            SystemMessageDisplayMessage msg = (SystemMessageDisplayMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            msg.Parameters.ForEach(item => account.Log(new DofAlertTextInformation(item), 0));
            // account.SocketManager.Disconnect("Alerte au modo ! Alerte au modo !");
        }


        [MessageHandler(typeof(PartyInvitationMessage))]
        public static void PartyInvitationMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            PartyInvitationMessage msg = (PartyInvitationMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (account.MyGroup != null && msg.FromName == account.MyGroup.GetMaster().CharacterBaseInformations.Name)
            {

                PartyAcceptInvitationMessage msg2 = new PartyAcceptInvitationMessage(msg.PartyId);
                account.SocketManager.Send(msg2);
                account.Log(new BotTextInformation("J'ai rejoint le groupe :3"), 3);

            }
            else
            {
                PartyRefuseInvitationMessage msg2 = new PartyRefuseInvitationMessage(msg.PartyId);
                account.SocketManager.Send(msg2);

            }
        }

        [MessageHandler(typeof(PartyMemberInFightMessage))]
        public async static void PartyMemberInFightMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            PartyMemberInFightMessage msg = (PartyMemberInFightMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (msg.FightMap.MapId == account.MapData.Id && msg.MemberName == account.MyGroup.GetMaster().CharacterBaseInformations.Name)
            {
                //account.Wait(500, 1500);
                await account.PutTaskDelay(1500);
                using (BigEndianWriter writer = new BigEndianWriter())
                {
                    GameFightJoinRequestMessage msg2 = new GameFightJoinRequestMessage(msg.MemberId, msg.FightId);
                    account.SocketManager.Send(msg2);
                }
            }

        }

        [MessageHandler(typeof(InteractiveElementUpdatedMessage))]
        public static void InteractiveElementUpdatedMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            InteractiveElementUpdatedMessage msg = (InteractiveElementUpdatedMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (account.House != null)
            {
                InteractiveElement e = msg.InteractiveElement;
                account.House.ElementIdd = e.ElementId;
                List<InteractiveElementSkill> EnabledSkills = e.EnabledSkills;
                account.House.SkillInstanceID = EnabledSkills[1].SkillInstanceUid;
                account.House.UseHouse();
            }
            account.MapData.UpdateInteractiveElement(msg.InteractiveElement);

        }

        [MessageHandler(typeof(StatedElementUpdatedMessage))]
        public static void StatedElementUpdatedMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            StatedElementUpdatedMessage msg = (StatedElementUpdatedMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.MapData.UpdateStatedElement(msg.StatedElement);
        }

        [MessageHandler(typeof(PurchasableDialogMessage))]
        public static void PurchasableDialogMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            PurchasableDialogMessage msg = (PurchasableDialogMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (account.House != null)
            {
                account.House.priceHouse = msg.Price;
                if (account.House.priceHouse < account.Config.MaxPriceHouse)
                {
                    account.House.Buy();
                }
                else
                {
                    account.Log(new ErrorTextInformation("Prix trop élevé..."), 2);
                }
            }
        }

        [MessageHandler(typeof(HousePropertiesMessage))]
        public static void HousePropertiesMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
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
        public static void GameContextRemoveElementMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameContextRemoveElementMessage msg = (GameContextRemoveElementMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.MapData.Remove(msg.ObjectId);
        }

        [MessageHandler(typeof(GameRolePlayShowActorMessage))]
        public static void GameRolePlayShowActorMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            GameRolePlayShowActorMessage msg = (GameRolePlayShowActorMessage)message;
            account.Config.HeroicConfig.AnalysePacket(msg, packetDatas);
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }

            account.MapData.ParseActors(new List<GameRolePlayActorInformations>() { msg.Informations }.ToArray());

            if (account.Config.Flood.FloodStarted && account.Config.Flood.InPrivateChannel && msg.Informations is GameRolePlayCharacterInformations)
            {
                GameRolePlayCharacterInformations infos = (GameRolePlayCharacterInformations)msg.Informations;
                account.Config.Flood.SendPrivateTo(infos);
            }
            if (account.Config.Flood.SaveInMemory && msg.Informations is GameRolePlayCharacterInformations)
            {
                GameRolePlayCharacterInformations infos = (GameRolePlayCharacterInformations)msg.Informations;
                account.Config.Flood.SaveNameInDisk(infos);
            }
        }

        [MessageHandler(typeof(ExchangeStartedWithStorageMessage))]
        public static void ExchangeStartedWithStorageMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            ExchangeStartedWithStorageMessage msg = (ExchangeStartedWithStorageMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (account.Config.Path != null && account.Inventory != null)
            {
                List<int> items = account.Inventory.GetItemsToTransfer();
                account.Inventory.TransferItems(items);
                account.Inventory.GetItems(account.Inventory.ItemsToGetFromBank.Select(item => item.UID).ToList());
            }
            // Get and put items from/to bank
        }

        [MessageHandler(typeof(DisplayNumericalValuePaddockMessage))]
        public static void DisplayNumericalValueMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            DisplayNumericalValuePaddockMessage msg = (DisplayNumericalValuePaddockMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
        }

        [MessageHandler(typeof(ObtainedItemMessage))]
        public static void ObtainedItemMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            ObtainedItemMessage msg = (ObtainedItemMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (account.Gather.Current_El == null)
                return;
            account.Log(new ActionTextInformation("Ressource récoltée : " + account.Gather.resourceName + " +" + msg.BaseQuantity), 3);
            if (account.Gather.Stats.ContainsKey(account.Gather.resourceName))
                account.Gather.Stats[account.Gather.resourceName] += (int)msg.BaseQuantity;
            else
                account.Gather.Stats.Add(account.Gather.resourceName, (int)msg.BaseQuantity);
            //account.Gather.Current_Job.ActualizeStats(account.Gather.Stats);
        }

        ////////////////////////////////// PACKET DELETED ///////////////////////////////////////////////

        //[MessageHandler(typeof(DisplayNumericalValueWithAgeBonusMessage))]
        //public static void DisplayNumericalValueWithAgeBonusTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        //{
        //    DisplayNumericalValueWithAgeBonusMessage msg = (DisplayNumericalValueWithAgeBonusMessage)message;

        //    using (BigEndianReader reader = new BigEndianReader(packetDatas))
        //    {
        //        msg.Deserialize(reader);
        //    }
        //    account.ModifBar(6, 0, 0, "Connecté");
        //    if ((int)msg.type == 1 && msg.entityId == account.CharacterBaseInformations.id)
        //    {
        //        if (account.Gather.resourceName == "Unknown")
        //            return;
        //        account.Log(new ActionTextInformation("Ressource récoltée : " + account.Gather.resourceName + " +" + msg.value + msg.valueOfBonus), 3);
        //        if (account.Gather.Stats.ContainsKey(account.Gather.resourceName))
        //            account.Gather.Stats[account.Gather.resourceName] += msg.value + msg.valueOfBonus;
        //        else
        //            account.Gather.Stats.Add(account.Gather.resourceName, msg.value + msg.valueOfBonus);
        //        account.Gather.Current_Job.ActualizeStats(account.Gather.Stats);
        //        if (account.PerformGather() == false && account.Path != null)
        //            account.Path.PerformActionsStack();
        //    }
        //}

        [MessageHandler(typeof(InteractiveUseErrorMessage))]
        public static void InteractiveUseErrorMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            InteractiveUseErrorMessage msg = (InteractiveUseErrorMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            //if (account.Gather.Error())
            //    return;
            account.Gather.BanElementId(account.Gather.Id);
            //account.Log(new ErrorTextInformation("Erreur lors de l'utilisation de l'element numero " + msg.elemId + ". Pg lelz. Poursuite du trajet."), 0);
            if (account.Config.Path != null)
                account.Config.Path.PerformFlag();
        }

        [MessageHandler(typeof(InteractiveUseEndedMessage))]
        public static void InteractiveUseEndedMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            InteractiveUseEndedMessage msg = (InteractiveUseEndedMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (account.Gather.Id == -1)
                return;
            account.SetStatus(Status.None);
            account.Gather.Id = -1;
            if (account.Config.Path != null)
                account.Config.Path.PerformFlag();
        }

        [MessageHandler(typeof(ExchangeStartedWithPodsMessage))]
        public async static void ExchangeStartedWithPodsMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            ExchangeStartedWithPodsMessage msg = (ExchangeStartedWithPodsMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (!account.Inventory.ListeningToExchange)
                return;
            List<int> items = account.Inventory.GetItemsToTransfer();
            account.Inventory.TransferItems(items);
            await account.PutTaskDelay(3000);
            account.Inventory.ExchangeReady();
        }

        [MessageHandler(typeof(ExchangeRequestedTradeMessage))]
        public static void ExchangeRequestedTradeMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            ExchangeRequestedTradeMessage msg = (ExchangeRequestedTradeMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (account.Inventory.ListeningToExchange)
                account.Inventory.AcceptExchange();
        }

        [MessageHandler(typeof(ExchangeIsReadyMessage))]
        public static void ExchangeIsReadyMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            ExchangeIsReadyMessage msg = (ExchangeIsReadyMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (msg.Ready && account.Inventory.ListeningToExchange)
                account.Inventory.ExchangeReady();
        }


        #endregion
    }
}
