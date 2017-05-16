using BlueSheep.Protocol.Messages.Game.Inventory.Exchanges;
using BlueSheep.Core.Pets;

namespace BlueSheep.Core.Storage
{
    public class Leaving
    {
        #region Fields
        private Account.Account account;
        #endregion

        #region Public methods

        public Leaving(Account.Account account)
        {
            this.account = account;
        }

        public void Init()
        {
            int quantity = 0;
            int objectUID = 0;

            foreach (BlueSheep.Core.Inventory.Item item in account.Inventory.Items)
            {
                bool isFood = false;

                foreach (Pet pet in account.PetsList)
                {
                    foreach (Food food in pet.FoodList)
                    {
                        if (item.UID == food.Informations.UID)
                        {
                            isFood = true;
                            break;
                        }
                    }
                }

                if (isFood)
                {
                    quantity = item.Quantity;
                    objectUID = item.UID;
                    break;
                }
            }

            if (objectUID == 0)
            {
                account.Running.GettingFoodFromSafe();
                return;
            }

            account.Running.OnLeaving = true;

            ExchangeObjectMoveMessage exchangeObjectMoveMessage =
                new ExchangeObjectMoveMessage((uint)objectUID, quantity);
            account.SocketManager.Send(exchangeObjectMoveMessage);
            account.LastPacketID.Clear();

        }
        #endregion
    }
}
