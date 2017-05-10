using System;
using System.Threading;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;
using BlueSheep.Common;
using BlueSheep.Interface;
using BlueSheep.Util.Text.Log;
using BlueSheep.Common.Enums;
using BlueSheep.Util.Enums.EnumHelper;
using BlueSheep.Common.Protocol.Messages.Game.Basic;
using BlueSheep.Common.Protocol.Messages.Game.Interactive;
using BlueSheep.Common.Protocol.Messages.Game.Inventory.Exchanges;
using BlueSheep.Common.Protocol.Messages.Game.Inventory.Items;
using BlueSheep.Common.Protocol.Messages.Game.Dialog;
using BlueSheep.Common.Protocol.Messages.Game.Approach;

namespace BlueSheep.Engine.Handlers.Basic
{
    class BasicHandler
    {
        #region Public methods
        [MessageHandler(typeof(SequenceNumberRequestMessage))]
        public static void SequenceNumberRequestMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            account.Sequence++;

            SequenceNumberMessage sequenceNumberMessage = new SequenceNumberMessage((ushort)account.Sequence);
            account.SocketManager.Send(sequenceNumberMessage);
        }

        [MessageHandler(typeof(BasicLatencyStatsRequestMessage))]
        public static void BasicLatencyStatsRequestMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            BasicLatencyStatsMessage basicLatencyStatsMessage = new BasicLatencyStatsMessage((ushort)account.LatencyFrame.GetLatencyAvg(),
                (ushort)account.LatencyFrame.GetSamplesCount(), (ushort)account.LatencyFrame.GetSamplesMax());
            if (!account.IsMITM)
            {
                using (BigEndianWriter writer = new BigEndianWriter())
                {
                    basicLatencyStatsMessage.Serialize(writer);
                    writer.Content = account.HumanCheck.hash_function(writer.Content);
                    MessagePackaging messagePackaging = new MessagePackaging(writer);

                    messagePackaging.Pack(basicLatencyStatsMessage.MessageID);

                    account.SocketManager.Send(messagePackaging.Writer.Content);
                    account.Log(new BlueSheep.Util.Text.Log.DebugTextInformation("[SND] 5663 (BasicLatencyStatsMessage)"), 0);
                }
            }

        }

        [MessageHandler(typeof(BasicAckMessage))]
        public static void BasicAckMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
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
        public static void BasicNoOperationMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            //MainForm.ActualMainForm.ActualizeAccountInformations();
            //if (account.MapData.Begin)
            //    account.ActualizeFamis();
            // TODO Militão: Populate the new interface
            Thread.Sleep(GetRandomTime());

            if (account.LastPacketID.Count == 0)
                return;

            //switch ((uint)account.LastPacketID.Dequeue())
            switch ((uint)account.LastPacket)
            {
                case InteractiveUseRequestMessage.ProtocolId:
                    if (account.Running != null && account.Running.OnSafe)
                    {
                        account.Log(new CharacterTextInformation("Ouverture du coffre."), 2);

                        account.Running.Init();
                    }
                    return;

                case ExchangeObjectMoveMessage.ProtocolId:
                    if (account.Running.OnLeaving)
                    {
                        account.Running.OnLeaving = false;
                        account.Log(new ActionTextInformation("Dépôt d'un objet dans le coffre."), 3);
                        account.Running.Init();
                    }
                    else if (account.Running.OnGetting)
                    {
                        account.Running.OnGetting = false;
                        account.Log(new ActionTextInformation("Récupération d'un objet du coffre."), 3);
                        account.Running.Init();
                    }
                    return;

                case ObjectFeedMessage.ProtocolId:
                    if (account.Running != null && !account.Running.Feeding.SecondFeeding)
                        account.Running.CheckStatisticsUp();
                    else if (account.Running != null)
                    {
                        account.Running.CurrentPetIndex++;
                        account.Running.Init();
                    }
                    return;

                case LeaveDialogRequestMessage.ProtocolId:
                    account.Log(new ActionTextInformation("Fermeture du coffre."), 3);
                    if (account.Running != null)
                        account.Running.Init();
                    return;
                //case GameMapMovementRequestMessage.ProtocolId:

                //    return;

                //case GameMapMovementConfirmMessage.ProtocolId:
                //    account.Fight.LaunchFight(account.Fight.flag);
                //    return;

                default:
                    return;
            }

        }
        [MessageHandler(typeof(BasicTimeMessage))]
        public static void BasicTimeMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            BasicTimeMessage btmsg = (BasicTimeMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                btmsg.Deserialize(reader);
            }
            //double serverTimeLag = btmsg.timestamp + btmsg.timezoneOffset * 60 * 1000; // - ((new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc) - DateTime.MinValue) + DateTime.MinValue).TotalMilliseconds;
            //DateTime epoch = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            ////System.DateTime date = System.DateTime.FromOADate(msg.subscriptionEndDate); 
            ////epoch.AddSeconds(msg.subscriptionEndDate / 1000) + 3600);
            //epoch = epoch.AddMilliseconds(account.serverTimeLag + serverTimeLag);
            //if (epoch.Minute > 0)
            //{
            //    // account.AboDofLabel.Text = date.Date.ToString();
            //    account.Log(new BotTextInformation(epoch.Date.ToShortDateString()), 0);
            //}
            //account.serverTimeLag = serverTimeLag;
        }

        [MessageHandler(typeof(AccountLoggingKickedMessage))]
        public static void AccountLoggingKickedMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            AccountLoggingKickedMessage btmsg = (AccountLoggingKickedMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                btmsg.Deserialize(reader);
            }
            account.Log(new ErrorTextInformation(String.Format("Compte banni {0} jours, {1} heures, {2} minutes :'( ", btmsg.Days, btmsg.Hours, btmsg.Minutes)), 0);
        }

        [MessageHandler(typeof(ServerSettingsMessage))]
        public static void ServerSettingsMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
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
