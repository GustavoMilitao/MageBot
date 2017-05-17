using MageBot.Protocol.Messages.Game.Dialog;

namespace MageBot.Core.Storage
{
    public class LeavingDialog
    {
        #region Public methods
        public void Init(Account.Account account)
        {
            LeaveDialogRequestMessage leaveDialogRequestMessage = new LeaveDialogRequestMessage();
            account.SocketManager.Send(leaveDialogRequestMessage);
            account.LastPacketID.Clear();

        }
        #endregion
    }
}
