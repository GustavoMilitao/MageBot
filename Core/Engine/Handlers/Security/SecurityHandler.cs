using MageBot.Protocol.Messages;
using System.Collections.Generic;
using MageBot.Protocol.Messages.Security;

namespace MageBot.Core.Engine.Handlers.Security
{
    class SecurityHandler
    {
        #region Public methods
        [MessageHandler(typeof(RawDataMessage))]
        public static void RawDataMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            List<int> tt = new List<int>();
            CheckIntegrityMessage checkIntegrityMessage = new CheckIntegrityMessage(tt);

            account.SocketManager.Send(checkIntegrityMessage);
        }

        [MessageHandler(typeof(CheckIntegrityMessage))]
        public static void CheckIntegrityMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
        }

        public static int MakeCrc(int param1, int param2)
        {
            int loc1 = param2 & 65535;
            int loc2 = param1 & 65535;
            int loc3 = loc1 ^ 14736;
            int ret = loc3 << 16 | loc1 ^ loc2;
            return ret;
        }
        #endregion


    }
}
