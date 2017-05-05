using DofusBot.Enums;

namespace BlueSheep.Util.Text.Connection
{
    public class IdentificationFailureReason
    { 
        #region Public methods
        public static void Test(IdentificationFailureReasonEnum reason, AccountUC account)
        {
            switch (reason)
            {
                case IdentificationFailureReasonEnum.WrongCredentials:
                    account.Log(new ErrorTextInformation("Echec de connexion : mauvais identifiants."),0);
                    break;

                case IdentificationFailureReasonEnum.Banned:
                    account.Log(new ErrorTextInformation("Echec de connexion : compte banni."), 0);
                    break;

                case IdentificationFailureReasonEnum.Kicked:
                    account.Log(new ErrorTextInformation("Echec de connexion : compte banni temporairement."), 0);
                    break;

                case IdentificationFailureReasonEnum.InMaintenance:
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
