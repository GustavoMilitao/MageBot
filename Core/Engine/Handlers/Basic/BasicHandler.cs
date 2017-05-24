using System;
using System.Threading;
using MageBot.Util.IO;
using MageBot.Protocol.Messages;
using Util.Util.Text.Log;
using MageBot.Protocol.Enums;
using MageBot.Util.Enums.EnumHelper;
using MageBot.Protocol.Messages.Game.Basic;
using MageBot.Protocol.Messages.Game.Interactive;
using MageBot.Protocol.Messages.Game.Inventory.Exchanges;
using MageBot.Protocol.Messages.Game.Inventory.Items;
using MageBot.Protocol.Messages.Game.Dialog;
using MageBot.Protocol.Messages.Game.Approach;

namespace MageBot.Core.Engine.Handlers.Basic
{
    class BasicHandler
    {
        #region Public methods
        [MessageHandler(typeof(SequenceNumberRequestMessage))]
        public static void SequenceNumberRequestMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            account.Sequence++;

            SequenceNumberMessage sequenceNumberMessage = new SequenceNumberMessage((ushort)account.Sequence);
            account.SocketManager.Send(sequenceNumberMessage);
        }

        [MessageHandler(typeof(BasicLatencyStatsRequestMessage))]
        public static void BasicLatencyStatsRequestMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            BasicLatencyStatsMessage basicLatencyStatsMessage = new BasicLatencyStatsMessage((ushort)account.LatencyFrame.GetLatencyAvg(),
                (ushort)account.LatencyFrame.GetSamplesCount(), (ushort)account.LatencyFrame.GetSamplesMax());
            if (account.Config.IsSocket)
            {
                using (BigEndianWriter writer = new BigEndianWriter())
                {
                    basicLatencyStatsMessage.Serialize(writer);
                    writer.Content = account.HumanCheck.Hash_function(writer.Content);
                    basicLatencyStatsMessage.Pack(writer);

                    account.SocketManager.Send(writer.Content);
                    account.Log(new DebugTextInformation("[SND] 5663 (BasicLatencyStatsMessage)"), 0);
                }
            }

        }

        [MessageHandler(typeof(BasicAckMessage))]
        public static void BasicAckMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            BasicAckMessage basicAckMessage = (BasicAckMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                basicAckMessage.Deserialize(reader);
            }

            account.LastPacketID.Enqueue(basicAckMessage.LastProtocolId);
            account.LastPacket = basicAckMessage.LastProtocolId;


        }

        [MessageHandler(typeof(BasicNoOperationMessage))]
        public static void BasicNoOperationMessageTreatment(Message message, byte[] packetDatas, Account.Account account)
        {
            if (account.Config.Begin)
                account.UpdatePets();
            account.Wait(GetRandomTime());

            if (account.LastPacketID.Count == 0)
                return;

            switch ((uint)account.LastPacket)
            {
                case InteractiveUseRequestMessage.ProtocolID:
                    if (account.Running != null && account.Running.OnSafe)
                    {
                        account.Log(new CharacterTextInformation("Trunk Opening."), 2);

                        account.Running.Init();
                    }
                    return;

                case ExchangeObjectMoveMessage.ProtocolID:
                    if (account.Running.OnLeaving)
                    {
                        account.Running.OnLeaving = false;
                        account.Log(new ActionTextInformation("Depositing an object in the trunk."), 3);
                        account.Running.Init();
                    }
                    else if (account.Running.OnGetting)
                    {
                        account.Running.OnGetting = false;
                        account.Log(new ActionTextInformation("Retrieving a Trunk Object."), 3);
                        account.Running.Init();
                    }
                    return;

                case ObjectFeedMessage.ProtocolID:
                    if (account.Running != null && !account.Running.Feeding.SecondFeeding)
                        account.Running.CheckStatisticsUp();
                    else if (account.Running != null)
                    {
                        account.Running.CurrentPetIndex++;
                        account.Running.Init();
                    }
                    return;

                case LeaveDialogRequestMessage.ProtocolID:
                    account.Log(new ActionTextInformation("Closed trunk."), 3);
                    if (account.Running != null)
                        account.Running.Init();
                    return;
                default:
                    return;
            }

        }
        [MessageHandler(typeof(BasicTimeMessage))]
        public static void BasicTimeMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            BasicTimeMessage btmsg = (BasicTimeMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                btmsg.Deserialize(reader);
            }
        }

        [MessageHandler(typeof(AccountLoggingKickedMessage))]
        public static void AccountLoggingKickedMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            AccountLoggingKickedMessage btmsg = (AccountLoggingKickedMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                btmsg.Deserialize(reader);
            }
            account.Log(new ErrorTextInformation(String.Format("Account kicked for {0} days, {1} hours, {2} minutes :'( ", btmsg.Days, btmsg.Hours, btmsg.Minutes)), 0);
        }

        [MessageHandler(typeof(ServerSettingsMessage))]
        public static void ServerSettingsMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            ServerSettingsMessage msg = (ServerSettingsMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.Log(
                new ConnectionTextInformation(" Server Settings : Language : " + msg.Lang +
                                         ", GameType : " + ((GameTypeIdEnum)msg.GameType).Description() +
                                         ", Comunity : " + ((CommunityIdEnum)msg.Community).Description()), 0);
        }

        #endregion

        #region Private methods
        private static int GetRandomTime()
        {
            Random random = new Random();

            return random.Next(250, 750);
        }
        #endregion
    }
}
