using System.Linq;
using MageBot.Util.IO;
using MageBot.Protocol.Messages;
using Util.Util.Text.Log;
using System;
using System.Collections.Generic;
using MageBot.DataFiles.Data.D2o;
using MageBot.Protocol.Messages.Game.Inventory.Items;
using MageBot.Protocol.Types.Game.Data.Items;
using MageBot.Protocol.Messages.Game.Inventory.Storage;
using MageBot.Protocol.Messages.Game.Inventory.Exchanges;
using MageBot.Protocol.Messages.Game.Chat.Channel;
using MageBot.Protocol.Messages.Game.Context;
using MageBot.Protocol.Messages.Security;
using MageBot.Protocol.Messages.Game.Friend;
using MageBot.Core.Pets;
using MageBot.Core.Inventory;

namespace MageBot.Core.Engine.Handlers.Inventory
{
    class InventoryHandler
    {
        #region Public methods
        [MessageHandler(typeof(InventoryContentMessage))]
        public static void InventoryContentMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            InventoryContentMessage inventoryContentMessage = (InventoryContentMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                inventoryContentMessage.Deserialize(reader);
            }
            foreach (ObjectItem item in inventoryContentMessage.Objects)
            {
                MageBot.Core.Inventory.Item i = new MageBot.Core.Inventory.Item(item.Effects.ToList(), item.ObjectGID, item.Position, (int)item.Quantity, (int)item.ObjectUID);
                account.Inventory.Items.AddOrUpdate(i.UID, i, (key, oldValue) => i);
            }
            account.UpdateInventory();
            account.PetsList = new List<Pet>();
            foreach (var item in account.Inventory.Items)
            {
                DataClass itemData = GameData.GetDataObject(D2oFileEnum.Items, item.Value.GID);
                if ((int)itemData.Fields["typeId"] == 18)
                {
                    Pet pet = new Pet(item.Value, itemData, account);
                    account.PetsList.Add(pet);
                    pet.SetFood();
                }
            }
            if (account.PetsList.Count > 0)
                account.Log(new BotTextInformation("Vos " + account.PetsList.Count + " familiers vous font un gros bisou de la part de MageBot."), 3);
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
        public static void InventoryContentAndPresetMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            InventoryContentAndPresetMessage msg = (InventoryContentAndPresetMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            foreach (ObjectItem item in msg.Objects)
            {
                MageBot.Core.Inventory.Item i = new MageBot.Core.Inventory.Item(item.Effects.ToList(), item.ObjectGID, item.Position, (int)item.Quantity, (int)item.ObjectUID);
                account.Inventory.Items.AddOrUpdate(i.UID, i, (key, oldValue) => i);
            }
            account.UpdateInventory();
            account.PetsList = new List<Pet>();
            foreach (var item in account.Inventory.Items)
            {
                DataClass itemData = GameData.GetDataObject(D2oFileEnum.Items, item.Value.GID);
                if ((int)itemData.Fields["typeId"] == 18)
                {
                    Pet pet = new Pet(item.Value, itemData, account);
                    account.PetsList.Add(pet);
                    pet.SetFood();
                }
            }
            account.Log(new BotTextInformation("Your " +
            account.PetsList.Count + " pets send you a big kiss from MageBot."), 5);
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
        public static void ObjectModifiedMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            ObjectModifiedMessage msg = (ObjectModifiedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            var i = new Core.Inventory.Item(msg.Object.Effects, msg.Object.ObjectGID, msg.Object.Position, (int)msg.Object.Quantity, (int)msg.Object.ObjectUID);
            account.Inventory.Items.AddOrUpdate(i.UID, i, (key, oldValue) => i);
            DataClass ItemData = GameData.GetDataObject(D2oFileEnum.Items, msg.Object.ObjectGID);
            if ((int)ItemData.Fields["typeId"] == 18)
            {
                Pet pet = new Pet(new MageBot.Core.Inventory.Item(msg.Object.Effects.ToList(), msg.Object.ObjectGID, msg.Object.Position, (int)msg.Object.Quantity, (int)msg.Object.ObjectUID), ItemData, account);
                if (account.PetsModifiedList == null)
                    account.PetsModifiedList = new List<Pet>();
                account.PetsModifiedList.Add(pet);
                account.Log(new ActionTextInformation("Pet fed : " + MageBot.DataFiles.Data.I18n.I18N.GetText((int)ItemData.Fields["nameId"]) + " " + "."), 3);
            }
        }

        [MessageHandler(typeof(ObjectQuantityMessage))]
        public static void ObjectQuantityMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            ObjectQuantityMessage msg = (ObjectQuantityMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if(account.Inventory.Items.ContainsKey((int)msg.ObjectUID))
            {
                account.Inventory.Items[(int)msg.ObjectUID].Quantity = (int)msg.Quantity;
                account.UpdateInventory();
            }
            if (account.Running != null)
            {
                foreach (Pet pet in account.PetsList)
                    pet.SetFood();
            }
        }

        [MessageHandler(typeof(ExchangeErrorMessage))]
        public static void ExchangeErrorMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            account.Log(new CharacterTextInformation("Failed to open trunk."), 0);
            if (account.Running != null)
                account.Running.OnSafe = false;
        }

        [MessageHandler(typeof(StorageInventoryContentMessage))]
        public static void StorageInventoryContentMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
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
        public static void InventoryWeightMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
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
        public static void ObjectAddedMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            ObjectAddedMessage msg = (ObjectAddedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            ObjectItem item = msg.Object;
            Core.Inventory.Item i = new Core.Inventory.Item(item.Effects, item.ObjectGID, item.Position, (int)item.Quantity, (int)item.ObjectUID);
            account.Inventory.Items.AddOrUpdate(i.UID,i,(key,oldValue)=>i);
            string[] row1 = { i.GID.ToString(), i.UID.ToString(), i.Name, i.Quantity.ToString(), i.Type.ToString(), i.Price.ToString() };
            if (account.Running != null)
            {
                foreach (Pet pet in account.PetsList)
                    pet.SetFood();
            }
        }
        [MessageHandler(typeof(ObjectDeletedMessage))]
        public static void ObjectDeletedMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            ObjectDeletedMessage objectDeletedMessage = (ObjectDeletedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                objectDeletedMessage.Deserialize(reader);
            }
            Core.Inventory.Item i = new Core.Inventory.Item(new List<Protocol.Types.Game.Data.Items.Effects.ObjectEffect>(),0,0,0,0);
            account.Inventory.Items.TryRemove((int)objectDeletedMessage.ObjectUID,out i);
            account.UpdateInventory();
            if (account.Running != null)
            {
                foreach (Pet pet in account.PetsList)
                    pet.SetFood();
            }
        }
        [MessageHandler(typeof(StorageObjectUpdateMessage))]
        public static void StorageObjectUpdateMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
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
        public static void StorageObjectRemoveMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
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
        public static void ExchangeLeaveMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            if (account.Running != null)
                account.Running.OnSafe = false;
            account.Busy = false;
        }
        [MessageHandler(typeof(ExchangeShopStockStartedMessage))]
        public static void ExchangeShopStockStartedMessageTreatment(Message message, byte[] packetDatas, Account.Account account)
        {
            ExchangeShopStockStartedMessage msg = (ExchangeShopStockStartedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.UpdateShop(msg.ObjectsInfos.ToList());
            //if (account.Config.ItemsToAddToShop.Count > 0)
            //    account.Config.ItemsToAddToShop.ForEach(item => account.Inventory.AddItemToShop(item.Item1,item.Item2,item.Item3));
        }
        #endregion
    }
}
