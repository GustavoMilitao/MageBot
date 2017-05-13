using BlueSheep.Util.IO;
using BlueSheep.Protocol.Messages.Queues;
using BlueSheep.Protocol.Messages;
using BlueSheep.Util.Text.Log;
using BlueSheep.Util.I18n.Strings;

namespace BlueSheep.Engine.Handlers.Queues
{
    class QueuesHandler
    {
        #region Public methods
        [MessageHandler(typeof(LoginQueueStatusMessage))]
        public static void LoginQueueStatusTreatment(Message message, byte[] packetDatas, Account account)
        {
            LoginQueueStatusMessage loginQueueStatusMessage = (LoginQueueStatusMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                loginQueueStatusMessage.Deserialize(reader);
            }

            account.Log(new QueueTextInformation("File d'attente : " + loginQueueStatusMessage.Position +
                "/" + loginQueueStatusMessage.Total + "."),0);
        }

        [MessageHandler(typeof(QueueStatusMessage))]
        public static void QueueStatusMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            QueueStatusMessage queueStatusMessage = (QueueStatusMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                queueStatusMessage.Deserialize(reader);
            }

            account.Log(new QueueTextInformation(Strings.PositionInQueue + " : " + queueStatusMessage.Position + "/"
                + queueStatusMessage.Total + "."),0);
        }
        #endregion
    }
}
