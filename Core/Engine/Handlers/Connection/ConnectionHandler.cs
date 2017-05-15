using BlueSheep.Common.Data.D2o;
using BlueSheep.Util.IO;
using System;
using System.Collections.Generic;
using BlueSheep.Util.Enums.EnumHelper;
using BotForgeAPI.Protocol.Messages;
using BotForgeAPI.Network.Messages;
using BotForgeAPI.Protocol.Types;
using BotForgeAPI.Protocol.Enums;
using System.Linq;
using BotForgeAPI.Game.Map;
using Core.Engine.Types;
using BlueSheep.Engine.Constants;
using BotForgeAPI.Network;
using BlueSheep.Common.Data;
using RSA;
using BotForge.Core.Server;
//using BotForge.Core.Constants;

namespace Core.Engine.Handlers.Connection
{
    class ConnectionHandler
    {
        #region Public methods
        [MessageHandler(typeof(HelloConnectMessage))]
        public static void HelloConnectMessageTreatment(Message message, Account account)
        {
            account.Game.Character.SetStatus(Status.None);
            if (account.IsFullSocket)
            {
                HelloConnectMessage helloConnectMessage = (HelloConnectMessage)message;
                sbyte[] credentials = RSAKey.Encrypt(helloConnectMessage.Key,
                                                     account.Username,
                                                     account.Password,
                                                     helloConnectMessage.Salt);
                IdentificationMessage msg = new IdentificationMessage(
                    GameConstants.AutoConnect,
                    GameConstants.UseCertificate,
                    GameConstants.UseLoginToken,
                    new VersionExtended(GameConstants.Major,
                                        GameConstants.Minor,
                                        GameConstants.Release,
                                        GameConstants.Revision,
                                        GameConstants.Patch,
                                        GameConstants.BuildType,
                                        GameConstants.Install,
                                        GameConstants.Technology),
                    GameConstants.Lang,
                    credentials,
                    GameConstants.ServerID,
                    GameConstants.SessionOptionalSalt,
                    new List<ushort>().ToArray());
                account.Network.Connection.Send(msg);
            }
            account.Logger.Log("Identification in progress.", BotForgeAPI.Logger.LogEnum.Connection);
        }

        [MessageHandler(typeof(IdentificationSuccessMessage))]
        public static void IdentificationSuccessMessageTreatment(Message message, Account account)
        {
            IdentificationSuccessMessage msg = (IdentificationSuccessMessage)message;
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddMilliseconds(msg.SubscriptionEndDate).ToLocalTime();
            DateTime subscriptionDate = dtDateTime;
            if (subscriptionDate > DateTime.Now)
                account.ModifBar(9, 0, 0, subscriptionDate.Date.ToShortDateString());
            account.Logger.Log("Successful identification.", BotForgeAPI.Logger.LogEnum.Connection);
        }

        [MessageHandler(typeof(IdentificationFailedMessage))]
        public static void IdentificationFailedMessageTreatment(Message message, Account account)
        {
            IdentificationFailedMessage identificationFailedMessage = (IdentificationFailedMessage)message;
            account.Logger.Log("Failed to identify.", BotForgeAPI.Logger.LogEnum.TextInformationError);
            IdentificationFailureReason.Test((IdentificationFailureReasonEnum)identificationFailedMessage.Reason, account);
            //account.SocketManager.DisconnectFromGUI();
        }

        [MessageHandler(typeof(Types.SelectedServerDataExtendedMessage))]
        public static void SelectedServerDataExtendedMessageTreatment(Message message, Account account)
        {
            Types.SelectedServerDataExtendedMessage msg = (Types.SelectedServerDataExtendedMessage)message;
            //account.Log(new BotTextInformation(selectedServerDataExtendedMessage.address + " " + (int)selectedServerDataExtendedMessage.port));
            account.Ticket = AES.AES.TicketTrans(msg.Ticket).ToString();
            account.HumanCheck = new BotForge.Core.Crypto.HumanCheck(account.Ticket);
            //account.IsChangingServer = true;
            if (account.IsFullSocket)
            {
                account.Logger.Log("Connecting to server : " + I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Servers, msg.ServerId).Fields["nameId"]), BotForgeAPI.Logger.LogEnum.Connection);
                Types.Connection connection = new Types.Connection(account, msg.Address, msg.Port);
            }
            else
            {
                Types.SelectedServerDataExtendedMessage nmsg = new Types.SelectedServerDataExtendedMessage(
                                                                                               msg.CanCreateNewCharacter,
                                                                                               msg.ServerId,
                                                                                               msg.Address,
                                                                                               msg.Port,
                                                                                               msg.Ticket,
                                                                                               msg.ServerIds);
                using (BigEndianWriter writer = new BigEndianWriter())
                {
                    nmsg.Serialize(writer);
                    nmsg.Pack(writer);
                    //account.SocketManager.SendToDofusClient(writer.Content);
                    //account.SocketManager.DisconnectFromDofusClient();
                    //account.SocketManager.DisconnectServer("42 packet handling.");
                    //account.SocketManager.ListenDofus();
                    //await account.PutTaskDelay(200);
                }
                account.Logger.Log("Connected to server " + I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Servers, msg.ServerId).Fields["nameId"]), BotForgeAPI.Logger.LogEnum.Connection);
                //account.SocketManager.Connect(new ConnectionInformations(msg.Address, msg.Port, "of game"));
            }
        }

        [MessageHandler(typeof(ServerStatusUpdateMessage))]
        public static void ServerStatusUpdateMessageTreatment(Message message, Account account)
        {
            // Lecture du paquet ServerStatusUpdateMessage
            ServerStatusUpdateMessage serverStatusUpdateMessage = (ServerStatusUpdateMessage)message;
            // Cherche le statut du serveur
            ServerStatus.Test((ServerStatusEnum)serverStatusUpdateMessage.Server.Status, account);
        }

        [MessageHandler(typeof(ServersListMessage))]
        public static void ServerListMessageTreatment(Message message, Account account)
        {
            ServersListMessage msg = new ServersListMessage();

            account.Logger.Log("< --- Probably, your server is under maintenance --- >", BotForgeAPI.Logger.LogEnum.Connection);
            msg.Servers.ToList().ForEach(server => account.Logger.Log("< --- Server : " +
                I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Servers, server.ObjectId).Fields["nameId"])
                         + " Status : " + ((ServerStatusEnum)server.Status).Description() + " --- >", BotForgeAPI.Logger.LogEnum.Connection));

            //foreach (GameServerInformations gsi in msg.servers)
            //{
            //    account.Log(new ConnectionTextInformation("Server : "+
            //        IntelliSense.ServersList.Where(server => server.Id == gsi.id).FirstOrDefault().Name), 0);
            //}

            account.Logger.Log("Serveur complet.", BotForgeAPI.Logger.LogEnum.Connection);
            //account.TryReconnect(600);
        }

        [MessageHandler(typeof(HelloGameMessage))]
        public static void HelloGameMessageTreatment(Message message, Account account)
        {
            if (account.IsFullSocket)
            {
                AuthenticationTicketMessage authenticationTicketMessage = new AuthenticationTicketMessage(GameConstants.Lang,
                account.Ticket);
                account.Network.Connection.Send(authenticationTicketMessage);
            }
        }
        [MessageHandler(typeof(AuthenticationTicketAcceptedMessage))]
        public static void AuthenticationTicketAcceptedMessageTreatment(Message message, Account account)
        {
            if (account.IsFullSocket)
            {
                CharactersListRequestMessage charactersListRequestMessage = new CharactersListRequestMessage();
                (account.Network.Connection as ConnectionServer).Send(charactersListRequestMessage);
            }
        }

        [MessageHandler(typeof(AuthenticationTicketRefusedMessage))]
        public static void AuthenticationTicketAcceptedRefusedTreatment(Message message, Account account)
        {
            AuthenticationTicketRefusedMessage msg = new AuthenticationTicketRefusedMessage();
            account.Logger.Log("Error : Authentication Ticket Refused", BotForgeAPI.Logger.LogEnum.TextInformationError);
        }

        [MessageHandler(typeof(SelectedServerRefusedMessage))]
        public static void SelectedServerRefusedMessageTreatment(Message message, Account account)
        {
            // Lecture du paquet ServerStatusUpdateMessage
            SelectedServerRefusedMessage selectedServerRefusedMessage = (SelectedServerRefusedMessage)message;
            // Cherche le statut du serveur
            ServerStatus.Test((ServerStatusEnum)selectedServerRefusedMessage.ServerStatus, account);
        }
        [MessageHandler(typeof(IdentificationFailedForBadVersionMessage))]
        public static void IdentificationFailedForBadVersionMessageTreatment(Message message, Account account)
        {
            IdentificationFailedForBadVersionMessage identificationFailedForBadVersionMessage = (IdentificationFailedForBadVersionMessage)message;
            account.Logger.Log("Echec de connexion : Dofus a été mis à jour ("
            + identificationFailedForBadVersionMessage.RequiredVersion.Major + "."
            + identificationFailedForBadVersionMessage.RequiredVersion.Minor + "."
            + identificationFailedForBadVersionMessage.RequiredVersion.Release + "."
            + identificationFailedForBadVersionMessage.RequiredVersion.Revision + "."
            + identificationFailedForBadVersionMessage.RequiredVersion.Patch + "."
            + identificationFailedForBadVersionMessage.RequiredVersion.BuildType + ")."
            + " BlueSheep supporte uniquement la version " + GameConstants.Major + "."
            + GameConstants.Minor + "." + GameConstants.Release + "."
            + GameConstants.Revision + "." + GameConstants.Patch + "."
            + GameConstants.BuildType + " du jeu. Consultez le forum pour être alerté de la mise à jour du bot.", BotForgeAPI.Logger.LogEnum.TextInformationError);
            //account.SocketManager.DisconnectFromGUI();
        }
        [MessageHandler(typeof(IdentificationFailedBannedMessage))]
        public static void IdentificationFailedBannedMessageTreatment(Message message, Account account)
        {
            IdentificationFailedBannedMessage msg = (IdentificationFailedBannedMessage)message;
            account.Logger.Log("Sorry, your character is banned :(", BotForgeAPI.Logger.LogEnum.TextInformationError);
            //account.SocketManager.DisconnectFromGUI();
        }
        #endregion
    }
}