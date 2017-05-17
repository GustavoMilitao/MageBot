using System.Collections.Generic;
using MageBot.DataFiles.Data.D2o;
using MageBot.Protocol.Messages.Game.Inventory.Exchanges;
using MageBot.Protocol.Types.Game.Data.Items;
using MageBot.Core.Pets;

namespace MageBot.Core.Storage
{
    class Getting
    {
        #region Fields
        public Account.Account account;
        #endregion

        #region Public methods

        public Getting(Account.Account account)
        {
            this.account = account;
        }

        public void Init()
        {
            account.Running.OnGetting = true;

            List<int> foodIndex = Food.GetFoods(account.PetsList[account.Running.CurrentPetIndex].Informations.GID);

            ObjectItem objectItem = null;

            foreach (ObjectItem item1 in account.SafeItems)
            {
                if (foodIndex.Contains(item1.ObjectGID))
                    objectItem = item1;
            }

            if (objectItem == null)
            {
                account.Running.NoFood();
                return;
            }

            int abbleWeight = account.Pods.WeightMax -
                              account.Pods.Weight - 1;
            //return;
            DataClass item = GameData.GetDataObject(D2oFileEnum.Items, objectItem.ObjectGID);
            int objectWeight = (int)item.Fields["realWeight"];
            int quantity = abbleWeight / objectWeight;

            if (objectItem.Quantity < quantity)
                quantity = (int)objectItem.Quantity;

            ExchangeObjectMoveMessage exchangeObjectMoveMessage =
                new ExchangeObjectMoveMessage(objectItem.ObjectUID, -quantity);
            account.SocketManager.Send(exchangeObjectMoveMessage);
            account.LastPacketID.Clear();

        }
        #endregion
    }
}
