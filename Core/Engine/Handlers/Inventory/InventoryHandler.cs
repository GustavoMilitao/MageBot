using System.Linq;
using BlueSheep.Util.IO;
using BlueSheep.Util.Text.Log;
using System;
using System.Collections.Generic;
using BlueSheep.Common.Data.D2o;
using BotForgeAPI.Protocol.Messages;
using Core.Engine.Types;
using BotForgeAPI.Network.Messages;
using BotForge.Core.Game.Inventory;
using BotForge.Core.Game.Pets;
using BlueSheep.Common.Data;

namespace Core.Engine.Handlers.Inventory
{
    class InventoryHandler
    {
        #region Public methods
        [MessageHandler(typeof(InventoryContentMessage))]
        public static void InventoryContentMessageTreatment(Message message, Account account)
        {
            InventoryContentMessage inventoryContentMessage = (InventoryContentMessage)message;
            foreach (BotForgeAPI.Protocol.Types.ObjectItem item in inventoryContentMessage.Objects)
            {
                BotForge.Core.Game.Inventory.Item i = new BotForge.Core.Game.Inventory.Item(item);
                account.Game.Inventory.Data.Items.Add(i);
            }
            //account.ActualizeInventory();
            // TODO Militão: Populate the new interface
            foreach (BotForge.Core.Game.Inventory.Item item in account.Game.Inventory.Data.Items)
            {
                DataClass itemData = GameData.GetDataObject(D2oFileEnum.Items, item.GID);
                if ((int)itemData.Fields["typeId"] == 18)
                {
                    Pet pet = new Pet(item, account);
                    account.Game.Pets.Data.Pets.Add(pet);
                    pet.Set();
                }
            }
            if (account.Game.Pets.Data.Pets.Count > 0)
                account.Logger.Log("Your " + account.Game.Pets.Data.Pets.Count + " Familiar make you a big kiss from BlueSheep.", BotForgeAPI.Logger.LogEnum.Bot);
            if (account.IsFullSocket)
            {
                FriendsGetListMessage friendGetListMessage = new FriendsGetListMessage();
                account.Network.Connection.Send(friendGetListMessage);
                IgnoredGetListMessage ignoredGetListMessage = new IgnoredGetListMessage();
                account.Network.Connection.Send(ignoredGetListMessage);
                SpouseGetInformationsMessage spouseGetInformationsMessage = new SpouseGetInformationsMessage();
                account.Network.Connection.Send(spouseGetInformationsMessage);
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
                account.Network.Connection.Send(clientKeyMessage);
                GameContextCreateRequestMessage gameContextCreateRequestMessage = new GameContextCreateRequestMessage();
                account.Network.Connection.Send(gameContextCreateRequestMessage);
                ChannelEnablingMessage channelEnablingMessage = new ChannelEnablingMessage(7, false);
                account.Network.Connection.Send(channelEnablingMessage);
            }
        }
        [MessageHandler(typeof(InventoryContentAndPresetMessage))]
        public static void InventoryContentAndPresetMessageTreatment(Message message, Account account)
        {
            InventoryContentAndPresetMessage msg = (InventoryContentAndPresetMessage)message;
            foreach (BotForgeAPI.Protocol.Types.ObjectItem item in msg.Objects)
            {
                Item i = new Item(item);
                account.Game.Inventory.Data.Items.Add(i);
            }
            //account.ActualizeInventory();
            // TODO Militão: Populate the new interface
            foreach (Item item in account.Game.Inventory.Data.Items)
            {
                DataClass itemData = GameData.GetDataObject(D2oFileEnum.Items, item.GID);
                if ((int)itemData.Fields["typeId"] == 18)
                {
                    Pet pet = new Pet(item, account);
                    account.Game.Pets.Data.Pets.Add(pet);
                    pet.Set();
                }
            }
            account.Logger.Log("Your " + account.Game.Pets.Data.Pets.Count + " Familiar make you a big kiss from BlueSheep.", BotForgeAPI.Logger.LogEnum.Bot);
            if (account.IsFullSocket)
            {
                FriendsGetListMessage friendGetListMessage = new FriendsGetListMessage();
                account.Network.Connection.Send(friendGetListMessage);
                IgnoredGetListMessage ignoredGetListMessage = new IgnoredGetListMessage();
                account.Network.Connection.Send(ignoredGetListMessage);
                SpouseGetInformationsMessage spouseGetInformationsMessage = new SpouseGetInformationsMessage();
                account.Network.Connection.Send(spouseGetInformationsMessage);
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
                account.Network.Connection.Send(clientKeyMessage);
                GameContextCreateRequestMessage gameContextCreateRequestMessage = new GameContextCreateRequestMessage();
                account.Network.Connection.Send(gameContextCreateRequestMessage);
                ChannelEnablingMessage channelEnablingMessage = new ChannelEnablingMessage(7, false);
                account.Network.Connection.Send(channelEnablingMessage);
            }
        }

        [MessageHandler(typeof(ObjectModifiedMessage))]
        public static void ObjectModifiedMessageTreatment(Message message, Account account)
        {
            ObjectModifiedMessage msg = (ObjectModifiedMessage)message;
            for (int index = 0; index < account.Game.Inventory.Data.Items.Count; index++)
            {
                if (account.Game.Inventory.Data.Items[index].UID == msg.@object.ObjectUID)
                    account.Game.Inventory.Data.Items[index] = new Item(msg.@object);
            }
            DataClass ItemData = GameData.GetDataObject(D2oFileEnum.Items, msg.@object.ObjectGID);
            if ((int)ItemData.Fields["typeId"] == 18)
            {
                Pet pet = new Pet(new Item(msg.@object), account);
                //if (account.Game.Pets.Data.PetsModifiedList == null)
                //    account.PetsModifiedList = new List<Pet>();
                //account.PetsModifiedList.Add(pet);
                account.Logger.Log("Pet fed : " + I18N.GetText((int)ItemData.Fields["nameId"]) + " " + ".", BotForgeAPI.Logger.LogEnum.TextInformationMessage);
            }
        }

        [MessageHandler(typeof(ObjectQuantityMessage))]
        public static void ObjectQuantityMessageTreatment(Message message, Account account)
        {
            ObjectQuantityMessage msg = (ObjectQuantityMessage)message;
            for (int index = 0; index < account.Game.Inventory.Data.Items.Count; index++)
            {
                if (account.Game.Inventory.Data.Items[index].UID == msg.ObjectUID)
                {
                    account.Game.Inventory.Data.Items[index].Update(msg);
                    //account.ActualizeInventory();
                    // TODO Militão: Populate the new interface
                }
            }
            //if (account.Running != null)
            //{
            foreach (Pet pet in account.Game.Pets.Data.Pets)
                pet.Set();
            //}
        }

        [MessageHandler(typeof(ExchangeErrorMessage))]
        public static void ExchangeErrorMessageTreatment(Message message, Account account)
        {
            account.Logger.Log("Failed to open trunk.", BotForgeAPI.Logger.LogEnum.TextInformationMessage);
            //if (account.Running != null)
            //    account.Running.OnSafe = false;
        }

        [MessageHandler(typeof(StorageInventoryContentMessage))]
        public static void StorageInventoryContentMessageTreatment(Message message, Account account)
        {
            StorageInventoryContentMessage storageInventoryContentMessage = (StorageInventoryContentMessage)message;
            foreach (BotForgeAPI.Protocol.Types.ObjectItem item in storageInventoryContentMessage.Objects)
                //account.Game.Inventory..Add(item);
                (account.Game.Inventory.Data as InventoryData).Update(storageInventoryContentMessage);
            // TODO Militão 2.0: Verify
        }

        [MessageHandler(typeof(StorageKamasUpdateMessage))]
        public static void StorageKamasUpdateMessageTreatment(Message message, Account account)
        {
            StorageKamasUpdateMessage msg = (StorageKamasUpdateMessage)message;

            (account.Game.Inventory.Data as InventoryData).StorageKamas = (int)msg.KamasTotal;
        }

        [MessageHandler(typeof(ExchangeKamaModifiedMessage))]
        public static void ExchangeKamaModifiedMessageTreatment(Message message, Account account)
        {
            ExchangeKamaModifiedMessage msg = (ExchangeKamaModifiedMessage)message;
            (account.Game.Inventory.Data as InventoryData).Update(msg);
        }

        [MessageHandler(typeof(InventoryWeightMessage))]
        public static void InventoryWeightMessageTreatment(Message message, Account account)
        {
            InventoryWeightMessage msg = (InventoryWeightMessage)message;
            int Percent = (((int)msg.Weight / (int)msg.WeightMax) * 100);
            string text = Convert.ToString(msg.Weight) + "/" + Convert.ToString((int)msg.WeightMax) + "(" + Percent + "% )";
            int w = Convert.ToInt32(msg.Weight);
            int wmax = Convert.ToInt32(msg.WeightMax);
            account.ModifBar(3, wmax, w, "Pods");
            (account.Game.Inventory.Data as InventoryData).Update(msg);
        }

        [MessageHandler(typeof(ObjectAddedMessage))]
        public static void ObjectAddedMessageTreatment(Message message, Account account)
        {
            ObjectAddedMessage msg = (ObjectAddedMessage)message;
            BotForgeAPI.Protocol.Types.ObjectItem item = msg.@object;
            Item i = new Item(item);
            account.Game.Inventory.Data.Items.Add(i);
            //string[] row1 = { i.GID.ToString(), i.UID.ToString(), i.Name, i.Quantity.ToString(), i.Type.ToString(), i.Price.ToString() };
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
            //if (account.Running != null)
            //{
            foreach (Pet pet in account.Game.Pets.Data.Pets)
                pet.Set();
            //}
        }
        [MessageHandler(typeof(ObjectDeletedMessage))]
        public static void ObjectDeletedMessageTreatment(Message message, Account account)
        {
            ObjectDeletedMessage objectDeletedMessage = (ObjectDeletedMessage)message;
            for (int index = 0; index < account.Game.Inventory.Data.Items.Count; index++)
            {
                if (account.Game.Inventory.Data.Items[index].UID == objectDeletedMessage.ObjectUID)
                {
                    account.Game.Inventory.Data.Items.RemoveAt(index);
                    break;
                }
            }
            //account.ActualizeInventory();
            // TODO Militão: Populate the new interface
            //if (account.Running != null)
            //{
            foreach (Pet pet in account.Game.Pets.Data.Pets)
                pet.Set();
            //}
        }
        [MessageHandler(typeof(StorageObjectUpdateMessage))]
        public static void StorageObjectUpdateMessageTreatment(Message message, Account account)
        {
            StorageObjectUpdateMessage storageObjectUpdateMessage = (StorageObjectUpdateMessage)message;
            //bool exists = false;
            for (int index = 0; index < account.Game.Inventory.Data.StorageItems.Count; index++)
            {
                if (account.Game.Inventory.Data.StorageItems[index].UID ==
                storageObjectUpdateMessage.@object.ObjectUID)
                {
                    (account.Game.Inventory.Data as InventoryData).Update(storageObjectUpdateMessage);
                    //exists = true;
                }
            }
            //if (!exists)
            //    account.Game.Inventory.Data.StorageItems.Add(new Item(storageObjectUpdateMessage.@object));
            //TODO Militã0 2.0: Verify
        }
        [MessageHandler(typeof(StorageObjectRemoveMessage))]
        public static void StorageObjectRemoveMessageTreatment(Message message, Account account)
        {
            StorageObjectRemoveMessage storageObjectRemoveMessage = (StorageObjectRemoveMessage)message;
            for (int index = 0; index < account.Game.Inventory.Data.StorageItems.Count; index++)
            {
                if (account.Game.Inventory.Data.StorageItems[index].UID ==
                storageObjectRemoveMessage.ObjectUID)
                    account.Game.Inventory.Data.StorageItems.RemoveAt(index);
            }
        }
        [MessageHandler(typeof(ExchangeLeaveMessage))]
        public static void ExchangeLeaveMessageTreatment(Message message, Account account)
        {
            account.Game.Character.SetStatus(BotForgeAPI.Game.Map.Status.None);
        }
        [MessageHandler(typeof(ExchangeShopStockStartedMessage))]
        public static void ExchangeShopStockStartedMessageTreatment(Message message, Account account)
        {
            ExchangeShopStockStartedMessage msg = (ExchangeShopStockStartedMessage)message;
            //account.actualizeshop(msg.ObjectsInfos.ToList());
            // TODO Militão: Populate the new interface
            //if ((account.Game.Inventory.Data as InventoryData).Items.ItemsToAddToShop.Count > 0)
            //    account.Inventory.ItemsToAddToShop.ForEach(item => account.Inventory.AddItemToShop(item.Item1, item.Item2, item.Item3));
            // TODO Militão 2.0: Add Merchant module
        }
        #endregion
    }
}
