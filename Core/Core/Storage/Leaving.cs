using MageBot.Protocol.Messages.Game.Inventory.Exchanges;
using MageBot.Core.Pets;

namespace MageBot.Core.Storage
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

            foreach (var item in account.Inventory.Items)
            {
                bool isFood = false;

                foreach (Pet pet in account.PetsList)
                {
                    foreach (Food food in pet.FoodList)
                    {
                        if (item.Key == food.Informations.UID)
                        {
                            isFood = true;
                            break;
                        }
                    }
                }

                if (isFood)
                {
                    quantity = item.Value.Quantity;
                    objectUID = item.Key;
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
