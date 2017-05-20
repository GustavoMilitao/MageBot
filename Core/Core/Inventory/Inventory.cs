using MageBot.DataFiles.Data.D2o;
using MageBot.Protocol.Messages.Game.Dialog;
using MageBot.Protocol.Messages.Game.Inventory.Exchanges;
using MageBot.Protocol.Messages.Game.Inventory.Items;
using MageBot.Util.Enums.Internal;
using Util.Util.I18n.Strings;
using Util.Util.Text.Log;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MageBot.Core.Inventory
{
    public class Inventory
    {
        #region Fields
        public ulong Kamas { get; set; }
        public int maxWeight;
        public int weight;
        private Account.Account Account;
        public List<Item> Items; // All items
        public int weightPercent
        {
            get
            {
                double per = weight / (double)maxWeight;
                return (int)(per * 100);
            }

        }
        #endregion

        #region Constructors
        public Inventory(Account.Account account)
        {
            Account = account;

            Items = new List<Item>();
            Kamas = 0;
            maxWeight = 0;
            weight = 0;
        }
        #endregion

        #region Public Methods
        public bool HasFishingRod
        {
            get
            {
                Item weapon = Items.FirstOrDefault(i => i.Position == (int)InventoryPositionEnum.Weapon);
                if (weapon == null)
                    return false;

                DataClass fishingRod = GameData.GetDataObject(D2oFileEnum.Items, weapon.GID);
                return (int)fishingRod.Fields["typeId"] == 20 && (int)fishingRod.Fields["useAnimationId"] == 18 ? true : false;
            }
        }

        public int WeaponRange
        {
            get
            {
                Item weapon = Items.FirstOrDefault(i => i.Position == (int)InventoryPositionEnum.Weapon);
                return weapon != null ? (int)GameData.GetDataObject(D2oFileEnum.Items, weapon.GID).Fields["range"] : 1;
            }
        }

        public Item Weapon
        {
            get
            {
                Item weapon = Items.FirstOrDefault(i => i.Position == (int)InventoryPositionEnum.Weapon);
                return weapon;
            }
        }

        public void DeleteItem(int uid, int quantity)
        {
            if (ItemExists(uid) && ItemQuantity(uid) > 0)
            {
                ObjectDeleteMessage msg = new ObjectDeleteMessage((uint)uid, (uint)quantity);
                Account.SocketManager.Send(msg);
                Account.Log(new ActionTextInformation("Suppression de " + GetItemFromUID(uid).Name + "(x" + quantity + ")."), 2);
            }

        }

        public void SendItemToShop(int uid, int quantity, int price)
        {
            if (ItemExists(uid) && ItemQuantity(uid) > 0)
            {
                ExchangeRequestOnShopStockMessage packetshop = new ExchangeRequestOnShopStockMessage();
                Account.SocketManager.Send(packetshop);
                ExchangeObjectMovePricedMessage msg = new ExchangeObjectMovePricedMessage((uint)uid, quantity, (ulong)price);
                Account.SocketManager.Send(msg);
                Account.Log(new ActionTextInformation("Ajout de " + Account.Inventory.GetItemFromUID(uid).Name + "(x " + quantity + ") dans le magasin magasin au prix de : " + price + " Kamas"), 2);
                LeaveDialogRequestMessage packetleave = new LeaveDialogRequestMessage();
                Account.SocketManager.Send(packetleave);
            }

        }

        public void DropItem(int uid, int quantity)
        {
            if (ItemExists(uid) && ItemQuantity(uid) > 0)
            {
                ObjectDropMessage msg = new ObjectDropMessage((uint)uid, (uint)quantity);
                Account.SocketManager.Send(msg);
                Account.Log(new ActionTextInformation("Jet de " + GetItemFromUID(uid).Name + "(x" + quantity + ")."), 2);
            }
        }

        public void EquipItem(int uid)
        {
            if (ItemExists(uid) && ItemQuantity(uid) > 0)
            {
                ObjectSetPositionMessage msg = new ObjectSetPositionMessage((uint)uid, (sbyte)GetItemFromUID(uid).typeId, 1);
                Account.SocketManager.Send(msg);
                Account.Log(new ActionTextInformation(GetItemFromUID(uid).Name + " Equip."), 2);

            }
        }

        public void AddItemToShop(Item item, int quantity, ulong price)
        {
            ExchangeObjectMovePricedMessage msg = new ExchangeObjectMovePricedMessage((uint)item.UID, quantity, price);

            Account.SocketManager.Send(msg);
            Account.Log(new ActionTextInformation(Strings.AdditionOf + item.Name + "(x " + quantity + ") " + Strings.InTheStoreAtThePriceOf + " : " + price + " " + Strings.Kamas), 2);
            LeaveDialogRequestMessage packetleave = new LeaveDialogRequestMessage();
            Account.Wait(2000);
            Account.SocketManager.Send(packetleave);
        }

        public Item GetItemFromName(string name)
        {
            return Items.FirstOrDefault(i => i.Name == name);
        }

        public Item GetItemFromGID(int gid)
        {
            return Items.FirstOrDefault(i => i.GID == gid);
        }

        public Item GetItemFromUID(int uid)
        {
            return Items.FirstOrDefault(i => i.UID == uid);
        }

        public void UseItem(int uid)
        {
            if (!ItemExists(uid))
                return;

            ObjectUseMessage msg = new ObjectUseMessage((uint)uid);
            Account.SocketManager.Send(msg);
            Account.Log(new BotTextInformation("Utilisation de : " + GetItemFromUID(uid).Name), 3);
        }

        public void TransferItems(List<int> items)
        {
            foreach (int i in items)
            {
                Account.Log(new ActionTextInformation("Object transfered : " + GetItemFromUID(i).Name + " (x" + GetItemFromUID(i).Quantity + ")."), 2);
            }
            ExchangeObjectTransfertListFromInvMessage msg = new ExchangeObjectTransfertListFromInvMessage(items.Select(item => (uint)item).ToList());
            Account.SocketManager.Send(msg);
            Account.Log(new BotTextInformation("Path : All objects were transfered."), 3);
        }

        public void TransferKamas(long quantity = -1)
        {
            ExchangeObjectMoveKamaMessage kmsg = null;
            //-1 means ALL Kamas in inventory
            if (quantity == -1)
                kmsg = new ExchangeObjectMoveKamaMessage((long)Account.Inventory.Kamas);
            else
                kmsg = new ExchangeObjectMoveKamaMessage(quantity);
            Account.SocketManager.Send(kmsg);
        }

        public List<int> GetItemsToTransfer()
        {
            List<int> stayingItems = Account.Config.ItemsToStayOnCharacter.Select(item => item.UID).ToList();
            List<int> items = new List<int>();
            foreach (Item i in Items)
            {
                if (!stayingItems.Contains(i.UID))
                    items.Add(i.UID);
            }
            return items;
        }

        public void GetItems(List<int> items)
        {
            if (items.Count > 0)
            {
                foreach (int i in items)
                {
                    Account.Log(new ActionTextInformation("Objet pris du coffre : " + GetItemFromUID(i).Name + " (x" + GetItemFromUID(i).Quantity + ")."), 2);
                }
                ExchangeObjectTransfertListToInvMessage msg = new ExchangeObjectTransfertListToInvMessage(items.Select(item => (uint)item).ToList());
                Account.SocketManager.Send(msg);
                Account.Log(new BotTextInformation("Trajet : Tous les objets pris du coffre."), 3);
            }
            Account.Npc.CloseDialog();
        }

        public void ExchangeReady()
        {
            Account.SocketManager.Send(new ExchangeReadyMessage(true, 2));
            Account.Log(new ActionTextInformation("Exchange done"), 5);
        }

        public void RequestExchange(string name)
        {
            ulong targetId = (ulong)Account.MapData.Players.Find(p => p.Name == name).ContextualId;
            if (targetId != 0)
            {
                Account.SocketManager.Send(new ExchangePlayerRequestMessage(1, targetId));
                Account.Log(new ActionTextInformation("Demande d'échange"), 5);
            }
            else
            {
                Account.Log(new ErrorTextInformation("Le joueur " + name + " ne semble pas être sur la map."), 5);
            }
        }

        public void AcceptExchange()
        {
            Account.SocketManager.Send(new ExchangeAcceptMessage());
            Account.Log(new ActionTextInformation("Echange accepté"), 5);
        }


        public bool ItemExists(int uid)
        {
            return Items.FirstOrDefault(i => i.UID == uid) != null ? true : false;
        }

        public void PerformAutoDeletion()
        {
            if (Account.State != Status.Fighting)
            {
                if (Account.Config.ItemsToAutoDelete.Count > 0)
                {
                    foreach (Item item in Account.Config.ItemsToAutoDelete)
                    {
                        Account.Inventory.DeleteItem(item.UID, item.Quantity);
                    }
                }
            }
            else
            {
                Account.Log(new ErrorTextInformation("Impossible to destroy an item in combat"), 2);
            }
        }

        #endregion

        #region Private Methods

        private int ItemQuantity(int uid)
        {
            return Items.FirstOrDefault(i => i.UID == uid) != null ? Items.First(i => i.UID == uid).Quantity : 0;
        }
        #endregion
    }
}
