using BlueSheep.Protocol.Enums;
using BlueSheep.Core.Account;
using Util.Util.Text.Log;

namespace BlueSheep.Util.Text
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
                case ServerStatusEnum.Offline:
                    account.Log(new ConnectionTextInformation("Echec de connexion : serveur déconnecté."), 0);
                    account.TryReconnect(600);
                    break;

                case ServerStatusEnum.Saving:
                    account.Log(new ConnectionTextInformation("Echec de connexion : serveur en sauvegarde."), 0);
                    account.TryReconnect(600);
                    break;

                case ServerStatusEnum.Full:
                    account.Log(new ConnectionTextInformation("Echec de connexion : serveur complet."), 0);
                    account.TryReconnect(60);
                    break;

                default:
                    account.Log(new ConnectionTextInformation("Echec de connexion : raison inconnue."), 0);
                    account.TryReconnect(30);
                    break;
            }
        }
        #endregion
    }
}
