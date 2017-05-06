using BlueSheep.Common.IO;
using BlueSheep.Common.Protocol.Messages;
using BlueSheep.Common.Protocol.Messages.Game.Inventory.Items;
using BlueSheep.Common.Types;
using BlueSheep.Engine.Types;
using BlueSheep.Interface;

namespace BlueSheep.Core.Meal
{
    public class Feeding
    {
        #region Properties
        public bool SecondFeeding { get; set; }
        private AccountUC account;
        #endregion

        #region Public methods

        public Feeding(AccountUC accountform)
        {
            account = accountform;
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
            
            //account.Wait(500, 1000);
        }
        #endregion
    }
}
