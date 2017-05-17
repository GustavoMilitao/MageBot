using Util.Util.I18n.Strings;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MageBot.Core.Engine.Constants
{
    public class Translate
    {
        #region Fields
        private static Dictionary<string, string> Dic = new Dictionary<string, string>();
        private static int ENInitialized = 0;
        #endregion

        #region Public Methods

        public static string GetTranslation(string content)
        {
            if (ENInitialized == 0)
                InitEN();

            if (Dic.ContainsKey(content))
                return Strings.ResourceManager.GetString(
                    getResourceStringKey(Dic[content])
                    );
            return content;
        }
        #endregion

        #region Private Methods
        private static void InitEN()
        {
            Dic.Clear();
            Dic.Add("Connecté", "Connected");
            Dic.Add("Déconnecté", "Disconnected");
            Dic.Add("Dialogue", "Speaking");
            Dic.Add("Régénération", "Regenerating");
            Dic.Add("Récolte", "Gathering");
            Dic.Add("Déplacement", "Moving");
            Dic.Add("Combat", "Fighting");
            Dic.Add("Téléportation", "Teleportating");
            Dic.Add("est mort", "is dead");
            Dic.Add("Fin du tour", "Turn ended");
            Dic.Add("Equipement rapide numero", "Stuff preset number");
            Dic.Add("Trajet arrêté", "Path Stopped");
            Dic.Add("Reconnexion automatique dans minutes", "Reconnection in X minutes");
            Dic.Add("Prochain repas dans", "Next meal in");
            Dic.Add("Aucune nourriture disponible, pas de prochaine connexion", "No food available, stay disconnected.");
            Dic.Add("Connexion", "Connecting");
            Dic.Add("Je suis le chef de groupe biatch", "I'm the group leader biatch !");
            Dic.Add("Trajet chargé", "Path loaded ");
            Dic.Add("Erreur lors de la sélection du personnage", "Error during the selection of the character");
            Dic.Add("Serveur complet", "The server is full, try again later");
            Dic.Add("Echec de connexion : Dofus a été mis à jour", "Connection failure, Dofus has been updated. Try again later");
            Dic.Add("Echec de connexion : Vous êtes bannis", "Connection failure, your account has been banned");
            Dic.Add("Identification en cours", "Identification in process");
            Dic.Add("Identification réussie", "Identification successful");
            Dic.Add("Echec de l'identification", "Identification failure");
            Dic.Add("J'ai rejoint le groupe", "I joined the group");
            Dic.Add("Ancienne cellid of", "Last cellid of");
            Dic.Add("Nouvelle cellid of", "New cellid of ");
            Dic.Add("Début du combat", "Fight started");
            Dic.Add("Combat fini !", "Fight is over !");
            Dic.Add("Echec de l'ouverture du coffre", "Failed to open the chest");
            Dic.Add("File d'attente", "Position in queue");
            Dic.Add("Déconnecté du serveur", "Disconnected from the server");
            Dic.Add("Inactivité prolongée", "Prolonged inactivity");
            Dic.Add("Connexion établie au serveur", "Connected to server");
            Dic.Add("Connexion échouée", " Connection failed");
            Dic.Add("Lancement du trajet", "Path launched");
            Dic.Add("Echec de connexion : compte banni temporairement", "Connection failure, your account is temporarily banned");
            Dic.Add("Echec de connexion : compte banni", "Connection failure, your account is banned");
            Dic.Add("Echec de connexion : serveur en maintenance", "Connection failure, server's is down for maintenance");
            Dic.Add("Echec de connexion : erreur inconnue", "Connection failure : Uknown error");
            Dic.Add("Echec de connnexion : serveur déconnecté", "connection failure, servor disconnected");
            Dic.Add("Echec de connexion : serveur en sauvegarde", "Connection failure, the server is down for backup");
            Dic.Add("Echec de connexion : serveur complet", "connection failure, servor is full");
            Dic.Add("Echec de connexion : raison inconnue", "Connection failure : Unknown cause");
            Dic.Add("Récupération d'un objet du coffre", "Taking an item from the chest");
            Dic.Add("Fermeture du coffre", "Closing the chest");
            Dic.Add("Ouverture du coffre", "Opening the chest");
            Dic.Add("Dépôt d'un objet dans le coffre", "Putting an item in the chest");
            Dic.Add("est connecté", "is connected");
            Dic.Add("Fight Turn", "Fight Turn");
            Dic.Add("Sort validé", "Spell agreed-upon");
            Dic.Add("Cible en vue à la cellule", "Traget is on the cell n°");
            Dic.Add("It's Time to capture ! POKEBALL GOOOOOOOO", "It's Time to capture ! POKEBALL GOOOOOOOO");
            Dic.Add("Lancement d'un combat contre monstres de niveau", "Starting a fight against monsters of level ");
            Dic.Add("Lancement d'un sort en", "Launching a spell at");
            Dic.Add("Placement du personnage", "Character's in-fight position");
            Dic.Add("Impossible d'enclencher le déplacement", "Unable to move");
            Dic.Add("Le bot n'a pas encore reçu les informations de la map, veuillez patienter. Si le problème persiste, rapportez le beug sur forum : http://forum.magebot.com ", "The bot didn't receive the map's informations yet, please check out later, if the poblem persists, report it on the forum : http://forum.magebot.com");
            Dic.Add("Up de caractéristique", "Enhance characteristics");
            Dic.Add("Envoi de la réponse", "Sending the answer");
            Dic.Add("Aucune réponse disponible dans le trajet", "No answer available in the path for this ask");
            Dic.Add("au serveur de jeu", "to the game's server");
            Dic.Add("au serveur d'identification", "to the authentication's server");
            Dic.Add("La récolte n'est pas encore implémentée, veuillez attendre la mise à jour. Tenez vous au courant sur", "Gathering isn't already implemented, please wait a release. Stay updated on");
            ENInitialized = 1;
        }

        private static string getResourceStringKey(string unformattedKey)
        {
            string key = unformattedKey.TrimEnd().TrimStart().Replace(" ", "");
            return Regex.Replace(key, @"[^\p{L}\p{M}p{N}]+", "");
        }

        #endregion
    }
}
