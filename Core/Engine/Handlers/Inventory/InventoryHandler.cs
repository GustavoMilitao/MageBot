using System.Linq;
using BlueSheep.Util.IO;
using BlueSheep.Protocol.Types;
using BlueSheep.Protocol.Messages;
using BlueSheep.Util.Text.Log;
using System;
using System.Collections.Generic;
using BlueSheep.Common.Data.D2o;
using BlueSheep.Protocol.Messages.Game.Inventory.Items;
using BlueSheep.Protocol.Types.Game.Data.Items;
using BlueSheep.Protocol.Messages.Game.Inventory.Storage;
using BlueSheep.Protocol.Messages.Game.Inventory.Exchanges;
using BlueSheep.Protocol.Messages.Game.Chat.Channel;
using BlueSheep.Protocol.Messages.Game.Context;
using BlueSheep.Protocol.Messages.Security;
using BlueSheep.Protocol.Messages.Game.Friend;
using BlueSheep.Core.Pets;
using BlueSheep.Core.Inventory;

namespace BlueSheep.Engine.Handlers.Inventory
{
    class InventoryHandler
    {
        #region Public methods
        [MessageHandler(typeof(InventoryContentMessage))]
        public static void InventoryContentMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            InventoryContentMessage inventoryContentMessage = (InventoryContentMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                inventoryContentMessage.Deserialize(reader);
            }
            foreach (ObjectItem item in inventoryContentMessage.Objects)
            {
                Core.Inventory.Item i = new Core.Inventory.Item(item.Effects.ToList(), item.ObjectGID, item.Position, (int)item.Quantity, (int)item.ObjectUID);
                account.Inventory.Items.Add(i);
            }
            //account.ActualizeInventory();
            // TODO Militão: Populate the new interface
            account.petsList = new List<Pet>();
            foreach (Core.Inventory.Item item in account.Inventory.Items)
            {
                DataClass itemData = GameData.GetDataObject(D2oFileEnum.Items, item.GID);
                if ((int)itemData.Fields["typeId"] == 18)
                {
                    Pet pet = new Pet(item, itemData, account);
                    account.petsList.Add(pet);
                    pet.SetFood();
                }
            }
            if (account.petsList.Count > 0)
                account.Log(new BotTextInformation("Vos " + account.petsList.Count + " familiers vous font un gros bisou de la part de BlueSheep."), 3);
            if (!account.Config.IsMITM)
            {
                FriendsGetListMessage friendGetListMessage = new FriendsGetListMessage();
                account.SocketManager.Send(friendGetListMessage);
                IgnoredGetListMessage ignoredGetListMessage = new IgnoredGetListMessage();
                account.SocketManager.Send(ignoredGetListMessage);
                SpouseGetInformationsMessage spouseGetInformationsMessage = new SpouseGetInformationsMessage();
                account.SocketManager.Send(spouseGetInformationsMessage);
                Random random = new Random();
                const string hexChars = "0123456789ABCDEF";
                string key = string.Empty;
                for (int index = 0; index < 20; index++)
                {
                    int randomValue = random.Next(100);
                    if (randomValue <= 40)
                        key += (char)(random.Next(26) + 65);
                    else if (randomValue <= 80)
                        key += (char)(random.Next(26) + 97);
                    else
                        key += (char)(random.Next(10) + 48);
                }
                int pos = key.Sum(t => t % 16);
                key += hexChars[pos % 16];
                ClientKeyMessage clientKeyMessage = new ClientKeyMessage(key);
                account.SocketManager.Send(clientKeyMessage);
                GameContextCreateRequestMessage gameContextCreateRequestMessage = new GameContextCreateRequestMessage();
                account.SocketManager.Send(gameContextCreateRequestMessage);
                ChannelEnablingMessage channelEnablingMessage = new ChannelEnablingMessage(7, false);
                account.SocketManager.Send(channelEnablingMessage);
            }
        }
        [MessageHandler(typeof(InventoryContentAndPresetMessage))]
        public static void InventoryContentAndPresetMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            InventoryContentAndPresetMessage msg = (InventoryContentAndPresetMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            foreach (ObjectItem item in msg.Objects)
            {
                Core.Inventory.Item i = new Core.Inventory.Item(item.Effects.ToList(), item.ObjectGID, item.Position, (int)item.Quantity, (int)item.ObjectUID);
                account.Inventory.Items.Add(i);
            }
            //account.ActualizeInventory();
            // TODO Militão: Populate the new interface
            account.petsList = new List<Pet>();
            foreach (Core.Inventory.Item item in account.Inventory.Items)
            {
                DataClass itemData = GameData.GetDataObject(D2oFileEnum.Items, item.GID);
                if ((int)itemData.Fields["typeId"] == 18)
                {
                    Pet pet = new Pet(item, itemData, account);
                    account.petsList.Add(pet);
                    pet.SetFood();
                }
            }
            account.Log(new BotTextInformation("Vos " +
            account.petsList.Count + " familiers vous font un gros bisou de la part de BlueSheep."), 5);
            if (!account.Config.IsMITM)
            {
                FriendsGetListMessage friendGetListMessage = new FriendsGetListMessage();
                account.SocketManager.Send(friendGetListMessage);
                IgnoredGetListMessage ignoredGetListMessage = new IgnoredGetListMessage();
                account.SocketManager.Send(ignoredGetListMessage);
                SpouseGetInformationsMessage spouseGetInformationsMessage = new SpouseGetInformationsMessage();
                account.SocketManager.Send(spouseGetInformationsMessage);
                Random random = new Random();
                const string hexChars = "0123456789ABCDEF";
                string key = string.Empty;
                for (int index = 0; index < 20; index++)
                {
                    int randomValue = random.Next(100);
                    if (randomValue <= 40)
                        key += (char)(random.Next(26) + 65);
                    else if (randomValue <= 80)
                        key += (char)(random.Next(26) + 97);
                    else
                        key += (char)(random.Next(10) + 48);
                }
                int pos = key.Sum(t => t % 16);
                key += hexChars[pos % 16];
                ClientKeyMessage clientKeyMessage = new ClientKeyMessage(key);
                account.SocketManager.Send(clientKeyMessage);
                GameContextCreateRequestMessage gameContextCreateRequestMessage = new GameContextCreateRequestMessage();
                account.SocketManager.Send(gameContextCreateRequestMessage);
                ChannelEnablingMessage channelEnablingMessage = new ChannelEnablingMessage(7, false);
                account.SocketManager.Send(channelEnablingMessage);
            }
        }

        [MessageHandler(typeof(ObjectModifiedMessage))]
        public static void ObjectModifiedMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            ObjectModifiedMessage msg = (ObjectModifiedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            for (int index = 0; index < account.Inventory.Items.Count; index++)
            {
                if (account.Inventory.Items[index].UID == msg.Object.ObjectUID)
                    account.Inventory.Items[index] = new Core.Inventory.Item(msg.Object.Effects, msg.Object.ObjectGID, msg.Object.Position, (int)msg.Object.Quantity, (int)msg.Object.ObjectUID);
            }
            DataClass ItemData = GameData.GetDataObject(D2oFileEnum.Items, msg.Object.ObjectGID);
            if ((int)ItemData.Fields["typeId"] == 18)
            {
                Pet pet = new Pet(new Core.Inventory.Item(msg.Object.Effects.ToList(), msg.Object.ObjectGID, msg.Object.Position, (int)msg.Object.Quantity, (int)msg.Object.ObjectUID), ItemData, account);
                if (account.PetsModifiedList == null)
                    account.PetsModifiedList = new List<Pet>();
                account.PetsModifiedList.Add(pet);
                account.Log(new ActionTextInformation("Familier nourri : " + BlueSheep.Common.Data.I18N.GetText((int)ItemData.Fields["nameId"]) + " " + "."), 3);
            }
        }

        [MessageHandler(typeof(ObjectQuantityMessage))]
        public static void ObjectQuantityMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            ObjectQuantityMessage msg = (ObjectQuantityMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            for (int index = 0; index < account.Inventory.Items.Count; index++)
            {
                if (account.Inventory.Items[index].UID == msg.ObjectUID)
                {
                    account.Inventory.Items[index].Quantity = (int)msg.Quantity;
                    //account.ActualizeInventory();
                    // TODO Militão: Populate the new interface
                }
            }
            if (account.Running != null)
            {
                foreach (Pet pet in account.petsList)
                    pet.SetFood();
            }
        }

        [MessageHandler(typeof(ExchangeErrorMessage))]
        public static void ExchangeErrorMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            account.Log(new CharacterTextInformation("Echec de l'ouverture du coffre."), 0);
            if (account.Running != null)
                account.Running.OnSafe = false;
        }

        [MessageHandler(typeof(StorageInventoryContentMessage))]
        public static void StorageInventoryContentMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            StorageInventoryContentMessage storageInventoryContentMessage = (StorageInventoryContentMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                storageInventoryContentMessage.Deserialize(reader);
            }
            foreach (ObjectItem item in storageInventoryContentMessage.Objects)
                account.SafeItems.Add(item);
        }

        [MessageHandler(typeof(InventoryWeightMessage))]
        public static void InventoryWeightMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            InventoryWeightMessage msg = (InventoryWeightMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            int Percent = (((int)msg.Weight / (int)msg.WeightMax) * 100);
            string text = Convert.ToString(msg.Weight) + "/" + Convert.ToString((int)msg.WeightMax) + "(" + Percent + "% )";
            int w = Convert.ToInt32(msg.Weight);
            int wmax = Convert.ToInt32(msg.WeightMax);
            account.ModifBar(3, wmax, w, "Pods");
            account.Pods = new Pods((int)msg.Weight, (int)msg.WeightMax);
            account.Inventory.weight = (int)msg.Weight;
            account.Inventory.maxWeight = (int)msg.WeightMax;
        }

        [MessageHandler(typeof(ObjectAddedMessage))]
        public static void ObjectAddedMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            ObjectAddedMessage msg = (ObjectAddedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            ObjectItem item = msg.Object;
            Core.Inventory.Item i = new Core.Inventory.Item(item.Effects, item.ObjectGID, item.Position, (int)item.Quantity, (int)item.ObjectUID);
            account.Inventory.Items.Add(i);
            string[] row1 = { i.GID.ToString(), i.UID.ToString(), i.Name, i.Quantity.ToString(), i.Type.ToString(), i.Price.ToString() };
            //System.Windows.Forms.ListViewItem li = new System.Windows.Forms.ListViewItem(row1);
            //li.ToolTipText = i.Description;
            //account.AddItem(li, account.LVItems);
            //if (i.Type == "Sac de ressource")
            //{
            //    foreach (JobUC uc in account.JobsUC)
            //    {
            //        if (uc.OpenBagCb.Checked)
            //        {
            //            account.Inventory.UseItem(i.UID);
            //            account.Log(new ActionTextInformation("Ouverture automatique d'un sac de récolte : " + i.Name), 2);
            //        }
            //    }
            //}
            // TODO Militão: Populate the new interface
            if (account.Running != null)
            {
                foreach (Pet pet in account.petsList)
                    pet.SetFood();
            }
        }
        [MessageHandler(typeof(ObjectDeletedMessage))]
        public static void ObjectDeletedMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            ObjectDeletedMessage objectDeletedMessage = (ObjectDeletedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                objectDeletedMessage.Deserialize(reader);
            }
            for (int index = 0; index < account.Inventory.Items.Count; index++)
            {
                if (account.Inventory.Items[index].UID == objectDeletedMessage.ObjectUID)
                {
                    account.Inventory.Items.RemoveAt(index);
                    break;
                }
            }
            //account.ActualizeInventory();
            // TODO Militão: Populate the new interface
            if (account.Running != null)
            {
                foreach (Pet pet in account.petsList)
                    pet.SetFood();
            }
        }
        [MessageHandler(typeof(StorageObjectUpdateMessage))]
        public static void StorageObjectUpdateMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            StorageObjectUpdateMessage storageObjectUpdateMessage = (StorageObjectUpdateMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                storageObjectUpdateMessage.Deserialize(reader);
            }
            bool exists = false;
            for (int index = 0; index < account.SafeItems.Count; index++)
            {
                if (account.SafeItems[index].ObjectUID ==
                storageObjectUpdateMessage.Object.ObjectUID)
                {
                    account.SafeItems[index].Quantity +=
                    storageObjectUpdateMessage.Object.Quantity;
                    exists = true;
                }
            }
            if (!exists)
                account.SafeItems.Add(storageObjectUpdateMessage.Object);
        }
        [MessageHandler(typeof(StorageObjectRemoveMessage))]
        public static void StorageObjectRemoveMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            StorageObjectRemoveMessage storageObjectRemoveMessage = (StorageObjectRemoveMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                storageObjectRemoveMessage.Deserialize(reader);
            }
            for (int index = 0; index < account.SafeItems.Count; index++)
            {
                if (account.SafeItems[index].ObjectUID ==
                storageObjectRemoveMessage.ObjectUID)
                    account.SafeItems.RemoveAt(index);
            }
        }
        [MessageHandler(typeof(ExchangeLeaveMessage))]
        public static void ExchangeLeaveMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            if (account.Running != null)
                account.Running.OnSafe = false;
            account.Busy = false;
        }
        [MessageHandler(typeof(ExchangeShopStockStartedMessage))]
        public static void ExchangeShopStockStartedMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            ExchangeShopStockStartedMessage msg = (ExchangeShopStockStartedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            //account.actualizeshop(msg.ObjectsInfos.ToList());
            // TODO Militão: Populate the new interface
            if (account.Inventory.ItemsToAddToShop.Count > 0)
                account.Inventory.ItemsToAddToShop.ForEach(item => account.Inventory.AddItemToShop(item.Item1,item.Item2,item.Item3));
        }
        #endregion
    }
}
