using BlueSheep.Util.Text.Log;
using BotForgeAPI.Protocol.Enums;

namespace Core.Engine.Types
{
    public class ServerStatus
    {
        #region Attirbuts
        private const uint m_Offline = 1;
        private const uint m_Saving = 5;
        private const uint m_Full = 7;
        #endregion

        #region Public methods
        public static void Test(ServerStatusEnum status, Account account)
        {
            switch (status)
            {
                case ServerStatusEnum.OFFLINE:
                    account.Logger.Log("Server Offline", BotForgeAPI.Logger.LogEnum.Connection);
                    //account.TryReconnect(600);
                    break;

                case ServerStatusEnum.SAVING:
                    account.Logger.Log("Server Saving", BotForgeAPI.Logger.LogEnum.Connection);
                    //account.TryReconnect(600);
                    break;

                case ServerStatusEnum.FULL:
                    account.Logger.Log("Server full", BotForgeAPI.Logger.LogEnum.Connection);
                    //account.TryReconnect(60);
                    break;

                default:
                    account.Logger.Log("Can't connect to server: unknown reason.", BotForgeAPI.Logger.LogEnum.Connection);
                    //account.TryReconnect(30);
                    break;
            }
        }
        #endregion
    }
}
