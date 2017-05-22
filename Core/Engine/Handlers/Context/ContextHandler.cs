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
using System;
using System.IO;
using MageBot.Protocol.Messages.Game.Inventory;

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
            account.Heroic.AnalysePacket(message, packetDatas);
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
            account.SetStatus(Status.None);
            account.MapData.DoAction();
            account.UpdateMap();
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
                case 36:
                    if (account.Config.LockingSpectators)
                        account.Fight.LockFightForSpectators();
                    break;
                case 34:
                    // Fight lose
                    break;
            }


            DataClass data = GameData.GetDataObject(D2oFileEnum.InfoMessages, msg.MsgType * 10000 + msg.MsgId);
            string text = DataFiles.Data.I18n.I18N.GetText((int)data.Fields["textId"]);
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
            //if (account.Path != null)
            //    account.Path.PerformActionsStack();
        }

        [MessageHandler(typeof(PopupWarningMessage))]
        public static void PopupWarningMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            PopupWarningMessage msg = (PopupWarningMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.Log(new ErrorTextInformation("[FROM " + msg.Author + " ] : " + msg.Content), 0);
            account.Log(new BotTextInformation("You has been locked for " + msg.LockDuration + ". Stopping MageBot actions while blocked..."), 0);
            account.Log(new ErrorTextInformation("There is a popup on the screen, probably a moderator :s"), 0);
            account.Wait(msg.LockDuration);
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
                account.Log(new BotTextInformation("I joined the group :3"), 3);

            }
            else
            {
                PartyRefuseInvitationMessage msg2 = new PartyRefuseInvitationMessage(msg.PartyId);
                account.SocketManager.Send(msg2);
            }
        }

        [MessageHandler(typeof(PartyMemberInFightMessage))]
        public static void PartyMemberInFightMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            PartyMemberInFightMessage msg = (PartyMemberInFightMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (msg.FightMap.MapId == account.MapData.Id && msg.MemberName == account.MyGroup.GetMaster().CharacterBaseInformations.Name)
            {
                account.Wait(1500);
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
            account.UpdateMap();
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
            account.UpdateMap();
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
            HousePropertiesMessage msg = (HousePropertiesMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (account.Config.HouseSearcherEnabled && !String.IsNullOrEmpty(account.Config.HouseSearcherLogPath))
            {
                StreamWriter SourceFile = new StreamWriter(account.Config.HouseSearcherLogPath, true);
                SourceFile.WriteLine("Abandoned house in : " + "[" + account.MapData.X + ";" + account.MapData.Y + "]");
                SourceFile.Close();
            }
            account.Log(new BotTextInformation("Abandoned house in : " + "[" + account.MapData.X + ";" + account.MapData.Y + "]"), 1);
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
            account.Heroic.AnalysePacket(msg, packetDatas);
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }

            account.MapData.ParseActors(new List<GameRolePlayActorInformations>() { msg.Informations }.ToArray());

            if (account.Flood.FloodStarted && account.Config.FloodInPrivateChannel && msg.Informations is GameRolePlayCharacterInformations)
            {
                GameRolePlayCharacterInformations infos = (GameRolePlayCharacterInformations)msg.Informations;
                account.Flood.SendPrivateTo(infos);
                if (account.Config.FloodSaveInMemory)
                {
                    long level = (long)Math.Abs((infos.AlignmentInfos.CharacterPower - infos.ContextualId));
                    account.Flood.ListOfPlayersWithLevel.Add(infos.Name, level);
                    account.Flood.SaveNameInDisk();
                }
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
            if (account.Path != null && account.Inventory != null)
            {
                List<int> items = account.Inventory.GetItemsToTransfer();
                account.Inventory.TransferItems(items);
                account.Inventory.GetItems(account.Config.ItemsToGetFromBank.Select(item => item.UID).ToList());
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

        [MessageHandler(typeof(InteractiveUseErrorMessage))]
        public static void InteractiveUseErrorMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            InteractiveUseErrorMessage msg = (InteractiveUseErrorMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.Gather.BanElementId(account.Gather.Id);
            //if (account.Path != null)
                //account.Path.PerformFlag();
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
            //if (account.Path != null)
            //    account.Path.PerformFlag();
        }

        [MessageHandler(typeof(ExchangeStartedWithPodsMessage))]
        public static void ExchangeStartedWithPodsMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            ExchangeStartedWithPodsMessage msg = (ExchangeStartedWithPodsMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (!account.Config.ListeningToExchange)
                return;
            List<int> items = account.Inventory.GetItemsToTransfer();
            account.Inventory.TransferItems(items);
            account.Wait(3000);
            account.Inventory.TransferKamas();
            account.Wait(3000);
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
            if (account.Config.ListeningToExchange)
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
            if (msg.Ready && account.Config.ListeningToExchange)
                account.Inventory.ExchangeReady();
        }

        [MessageHandler(typeof(KamasUpdateMessage))]
        public static void KamasUpdateMessageTreatment(Message message, byte[] packetDatas, Account.Account account)
        {
            KamasUpdateMessage msg = (KamasUpdateMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.Inventory.Kamas = msg.KamasTotal;
            account.UpdateInfBars();
        }

        #endregion
    }
}
