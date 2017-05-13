using System;
using System.Threading;
using BlueSheep.Util.IO;
using BlueSheep.Util.Text.Log;
using BlueSheep.Util.Enums.EnumHelper;
using BotForgeAPI.Protocol.Messages;
using BotForgeAPI.Network.Messages;

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
            //account.SocketManager.Send(sequenceNumberMessage);
        }

        [MessageHandler(typeof(BasicLatencyStatsRequestMessage))]
        public static void BasicLatencyStatsRequestMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            BasicLatencyStatsMessage basicLatencyStatsMessage = new BasicLatencyStatsMessage((ushort)account.LatencyFrame.GetLatencyAvg(),
                (ushort)account.LatencyFrame.GetSamplesCount(), (ushort)account.LatencyFrame.GetSamplesMax());
            if (account.IsFullSocket)
            {
                using (BigEndianWriter writer = new BigEndianWriter())
                {
                    basicLatencyStatsMessage.Serialize(writer);
                    account.HumanCheck.Hash_Function(writer);
                    basicLatencyStatsMessage.Pack(writer);

                    //account.SocketManager.Send(writer.Content);
                    account.Logger.Log("[SND] 5663 (BasicLatencyStatsMessage)", BotForgeAPI.Logger.LogEnum.Debug);
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

            account.LastPacketID.Enqueue(basicAckMessage.LastPacketId);
            account.LastPacket = basicAckMessage.LastPacketId;


        }

        [MessageHandler(typeof(BasicNoOperationMessage))]
        public static void BasicNoOperationMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            //MainForm.ActualMainForm.ActualizeAccountInformations();
            //if (account.MapData.Begin)
            //    account.ActualizeFamis();
            // TODO Militão: Populate the new interface
            //Thread.Sleep(GetRandomTime());

            if (account.LastPacketID.Count == 0)
                return;

            switch ((uint)account.LastPacket)
            {
                case InteractiveUseRequestMessage.Id:
                    //if (account.Running != null && account.Running.OnSafe)
                    //{
                    //    account.Log(new CharacterTextInformation("Ouverture du coffre."), 2);

                    //    account.Running.Init();
                    //}
                    //return;
                    break;
                case ExchangeObjectMoveMessage.Id:
                    //if (account.Running.OnLeaving)
                    //{
                    //    account.Running.OnLeaving = false;
                    //    account.Log(new ActionTextInformation("Dépôt d'un objet dans le coffre."), 3);
                    //    account.Running.Init();
                    //}
                    //else if (account.Running.OnGetting)
                    //{
                    //    account.Running.OnGetting = false;
                    //    account.Log(new ActionTextInformation("Récupération d'un objet du coffre."), 3);
                    //    account.Running.Init();
                    //}
                    //return;
                    break;
                case ObjectFeedMessage.Id:
                    //if (account.Running != null && !account.Running.Feeding.SecondFeeding)
                    //    account.Running.CheckStatisticsUp();
                    //else if (account.Running != null)
                    //{
                    //    account.Running.CurrentPetIndex++;
                    //    account.Running.Init();
                    //}
                    //return;
                    break;

                case LeaveDialogRequestMessage.Id:
                    //account.Log(new ActionTextInformation("Fermeture du coffre."), 3);
                    //if (account.Running != null)
                    //    account.Running.Init();
                    //return;
                    break;
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
            account.Logger.Log(String.Format("Compte banni {0} jours, {1} heures, {2} minutes :'( ", btmsg.Days, btmsg.Hours, btmsg.Minutes), BotForgeAPI.Logger.LogEnum.TextInformationError);
        }

        [MessageHandler(typeof(ServerSettingsMessage))]
        public static void ServerSettingsMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            ServerSettingsMessage msg = (ServerSettingsMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            //account.Log(
            //    new ConnectionTextInformation(" Server Settings : Language : " + msg.Lang +
            //                             ", GameType : " + ((GameTypeIdEnum)msg.GameType).Description() +
            //                             ", Comunity : " + ((CommunityIdEnum)msg.Community).Description()), 0);
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
