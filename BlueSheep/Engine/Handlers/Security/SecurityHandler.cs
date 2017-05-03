using BlueSheep.Common.IO;
using BlueSheep.Common.Protocol.Messages;
using BlueSheep.Engine.Types;
using System;
using BlueSheep.Interface;
using System.IO;
using System.Text;
using BlueSheep.Interface.Text;
using System.Collections.Generic;
using BlueSheep.Engine.Constants;

namespace BlueSheep.Engine.Handlers.Security
{
    class SecurityHandler
    {
        #region Public methods
        [MessageHandler(typeof(RawDataMessage))]
        public static void RawDataMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            //TODO : Bypass this fucking anti-bot


            List<int> tt = new List<int>();
            for (int i = 0; i <= 255; i++)
            {
                Random random = new Random();
                int test = random.Next(-127, 127);
                tt.Add(test);
            }

            //tt.AddRange(HumanCheck.ToListOf(new HumanCheck(account)._hashKey, BitConverter.ToInt32));

            CheckIntegrityMessage checkIntegrityMessage = new CheckIntegrityMessage(tt);

            account.SocketManager.Send(checkIntegrityMessage);
        }

        [MessageHandler(typeof(CheckIntegrityMessage))]
        public static void CheckIntegrityMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            //using (BigEndianWriter writer = new BigEndianWriter())
            //{
            //    writer.WriteBytes(packetDatas);
            //    MessagePackaging pack = new MessagePackaging(writer);
            //    pack.Pack(6372);
            //    account.SocketManager.Send(pack.Writer.Content);
            //    account.Log(new BotTextInformation("Raw data traité avec succès."), 0);
            //}
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
