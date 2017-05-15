using BlueSheep.Util.IO;
using BlueSheep.Util.Text.Log;
using BlueSheep.Util.I18n.Strings;
using BotForgeAPI.Protocol.Messages;
using BotForgeAPI.Network.Messages;
using Core.Engine.Types;

namespace BlueSheep.Engine.Handlers.Queues
{
    class QueuesHandler
    {
        #region Public methods
        [MessageHandler(typeof(LoginQueueStatusMessage))]
        public static void LoginQueueStatusTreatment(Message message,  Account account)
        {
            LoginQueueStatusMessage loginQueueStatusMessage = (LoginQueueStatusMessage)message;

            account.Logger.Log(Strings.PositionInQueue+" : " + loginQueueStatusMessage.Position +
                "/" + loginQueueStatusMessage.Total + ".", BotForgeAPI.Logger.LogEnum.Queue);
        }

        [MessageHandler(typeof(QueueStatusMessage))]
        public static void QueueStatusMessageTreatment(Message message,  Account account)
        {
            QueueStatusMessage queueStatusMessage = (QueueStatusMessage)message;

            account.Logger.Log(Strings.PositionInQueue + " : " + queueStatusMessage.Position + "/"
                + queueStatusMessage.Total + ".", BotForgeAPI.Logger.LogEnum.Queue);
        }
        #endregion
    }
}
