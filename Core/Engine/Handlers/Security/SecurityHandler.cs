using System.Collections.Generic;
using BotForgeAPI.Protocol.Messages;
using BotForgeAPI.Network.Messages;
using Core.Engine.Types;

namespace Core.Engine.Handlers.Security
{
    class SecurityHandler
    {
        #region Public methods
        [MessageHandler(typeof(RawDataMessage))]
        public static void RawDataMessageTreatment(Message message,  Account account)
        {
            List<sbyte> tt = new List<sbyte>();
            CheckIntegrityMessage checkIntegrityMessage = new CheckIntegrityMessage(tt.ToArray());

            account.Network.Connection.Send(checkIntegrityMessage);
        }

        [MessageHandler(typeof(CheckIntegrityMessage))]
        public static void CheckIntegrityMessageTreatment(Message message,  Account account)
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
