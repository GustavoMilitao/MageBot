using BlueSheep.Protocol.Messages.Game.Inventory.Items;

namespace BlueSheep.Core.Pets
{
    public class Feeding
    {
        #region Properties
        public bool SecondFeeding { get; set; }
        private Account.Account account;
        #endregion

        #region Public methods

        public Feeding(Account.Account account)
        {
            this.account = account;
        }

        public void Init(Pet pet)
        {
            if (account.Running.OnSafe)
            {
                account.Running.LeavingDialogWithSafe();
                return;
            }

            ObjectFeedMessage objectFeedMessage = new ObjectFeedMessage((uint)pet.Informations.UID,
                (uint)pet.FoodList[0].Informations.UID, 1);

                account.SocketManager.Send(objectFeedMessage);
                account.LastPacketID.Clear();
        }
        #endregion
    }
}
