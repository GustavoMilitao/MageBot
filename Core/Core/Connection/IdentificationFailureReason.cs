using MageBot.Protocol.Enums;
using MageBot.Core.Account;
using Util.Util.Text.Log;

namespace MageBot.Util.Text.Connection
{
    public class IdentificationFailureReason
    { 
        #region Public methods
        public static void Test(IdentificationFailureReasonEnum reason, Account account)
        {
            switch (reason)
            {
                case IdentificationFailureReasonEnum.WRONG_CREDENTIALS:
                    account.Log(new ErrorTextInformation("Echec de connexion : mauvais identifiants."),0);
                    break;

                case IdentificationFailureReasonEnum.BANNED:
                    account.Log(new ErrorTextInformation("Echec de connexion : compte banni."), 0);
                    break;

                case IdentificationFailureReasonEnum.KICKED:
                    account.Log(new ErrorTextInformation("Echec de connexion : compte banni temporairement."), 0);
                    break;

                case IdentificationFailureReasonEnum.IN_MAINTENANCE:
                    account.Log(new ErrorTextInformation("Echec de connexion : serveur en maintenance."), 0);
                    account.TryReconnect(15);
                    break;

                default:
                    account.Log(new ErrorTextInformation("Echec de connexion : erreur inconnue."), 0);
                    break;
            }
        }
        #endregion
    }
}
