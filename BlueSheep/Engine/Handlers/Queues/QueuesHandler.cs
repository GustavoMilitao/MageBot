using BlueSheep.Common.IO;
using BlueSheep.Common.Protocol.Messages.Queues;
using BlueSheep.Common;
using BlueSheep.Interface;
using BlueSheep.Interface.Text;

namespace BlueSheep.Engine.Handlers.Queues
{
    class QueuesHandler
    {
        #region Public methods
        [MessageHandler(typeof(LoginQueueStatusMessage))]
        public static void LoginQueueStatusTreatment(Message message, byte[] packetDatas, AccountUC account)
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
        public static void QueueStatusMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            QueueStatusMessage queueStatusMessage = (QueueStatusMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                queueStatusMessage.Deserialize(reader);
            }

            account.Log(new QueueTextInformation("File d'attente : " + queueStatusMessage.Position + "/"
                + queueStatusMessage.Total + "."),0);
        }
        #endregion
    }
}
