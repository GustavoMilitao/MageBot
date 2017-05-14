using BlueSheep.Util.Text.Log;
using BotForgeAPI.Protocol.Enums;

namespace Core.Engine.Types
{
    public class IdentificationFailureReason
    { 
        #region Public methods
        public static void Test(IdentificationFailureReasonEnum reason, Account account)
        {
            switch (reason)
            {
                case IdentificationFailureReasonEnum.WRONG_CREDENTIALS:
                    account.Logger.Log("Echec de connexion : mauvais identifiants.", BotForgeAPI.Logger.LogEnum.TextInformationError);
                    break;

                case IdentificationFailureReasonEnum.BANNED:
                    account.Logger.Log("Echec de connexion : compte banni.", BotForgeAPI.Logger.LogEnum.TextInformationError);
                    break;

                case IdentificationFailureReasonEnum.KICKED:
                    account.Logger.Log("Echec de connexion : compte banni temporairement.", BotForgeAPI.Logger.LogEnum.TextInformationError);
                    break;

                case IdentificationFailureReasonEnum.IN_MAINTENANCE:
                    account.Logger.Log("Echec de connexion : serveur en maintenance.", BotForgeAPI.Logger.LogEnum.TextInformationError);
                    //account.TryReconnect(15);
                    break;

                default:
                    account.Logger.Log("Echec de connexion : erreur inconnue.", BotForgeAPI.Logger.LogEnum.TextInformationError);
                    break;
            }
        }
        #endregion
    }
}
