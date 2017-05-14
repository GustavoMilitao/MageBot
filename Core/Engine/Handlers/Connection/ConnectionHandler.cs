using BlueSheep.Common.Data.D2o;
using BlueSheep.Util.IO;
using BlueSheep.Engine.Constants;
using BlueSheep.Util.Text.Log;
using BlueSheep.Util.Text;
using System;
using System.Collections.Generic;
using BlueSheep.Util.Enums.EnumHelper;
using BlueSheep.Engine.Network;
using BotForgeAPI.Protocol.Messages;
using BotForgeAPI.Network.Messages;
using BotForgeAPI.Protocol.Types;
using BotForge.Core.Crypto;
using BotForgeAPI.Protocol.Enums;
using System.Linq;
using BotForgeAPI.Game.Map;
using Core.Engine.Types;

namespace BlueSheep.Engine.Handlers.Connection
{
    class ConnectionHandler
    {
        #region Public methods
        [MessageHandler(typeof(HelloConnectMessage))]
        public static void HelloConnectMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            account.Game.Character.SetStatus(Status.None);
            if (account.IsFullSocket)
            {
                HelloConnectMessage helloConnectMessage = (HelloConnectMessage)message;
                using (BigEndianReader reader = new BigEndianReader(packetDatas))
                {
                    helloConnectMessage.Deserialize(reader);
                }
                sbyte[] credentials = RSAKey.Encrypt(helloConnectMessage.Key,
                                                     account,
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
                //account.SocketManager.Send(msg);
            }
            account.Logger.Log("Identification in progress.", BotForgeAPI.Logger.LogEnum.Connection);
        }

        [MessageHandler(typeof(IdentificationSuccessMessage))]
        public static void IdentificationSuccessMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            IdentificationSuccessMessage msg = (IdentificationSuccessMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddMilliseconds(msg.SubscriptionEndDate).ToLocalTime();
            DateTime subscriptionDate = dtDateTime;
            if (subscriptionDate > DateTime.Now)
                account.ModifBar(9, 0, 0, subscriptionDate.Date.ToShortDateString());
            account.Logger.Log("Identification réussie." , BotForgeAPI.Logger.LogEnum.Connection);
        }

        [MessageHandler(typeof(IdentificationFailedMessage))]
        public static void IdentificationFailedMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            IdentificationFailedMessage identificationFailedMessage = (IdentificationFailedMessage)message;
            account.Logger.Log("Failed to identify.", BotForgeAPI.Logger.LogEnum.TextInformationError );
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                identificationFailedMessage.Deserialize(reader);
            }
            IdentificationFailureReason.Test((IdentificationFailureReasonEnum)identificationFailedMessage.Reason, account);
            //account.SocketManager.DisconnectFromGUI();
        }

        [MessageHandler(typeof(SelectedServerDataExtendedMessage))]
        public static void SelectedServerDataExtendedMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            SelectedServerDataExtendedMessage msg = (SelectedServerDataExtendedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            //account.Log(new BotTextInformation(selectedServerDataExtendedMessage.address + " " + (int)selectedServerDataExtendedMessage.port));
            account.Ticket = AES.AES.TicketTrans(msg.Ticket.Select(s => (int)s).ToList()).ToString();
            account.HumanCheck = new BotForge.Core.Crypto.HumanCheck(account.Ticket);
            //account.IsChangingServer = true;
            if (account.IsFullSocket)
            {
                account.Logger.Log("Connexion au serveur " + BlueSheep.Common.Data.I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Servers, msg.ServerId).Fields["nameId"]),BotForgeAPI.Logger.LogEnum.Connection);
                //account.SocketManager.Connect(new ConnectionInformations(msg.Address, msg.Port, "de jeu"));
            }
            else
            {
                SelectedServerDataExtendedMessage nmsg = new SelectedServerDataExtendedMessage(msg.ServerId,
                                                                                               msg.Address,
                                                                                               msg.Port,
                                                                                               msg.CanCreateNewCharacter,
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
                account.Logger.Log("Connected to server " + Common.Data.I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Servers, msg.ServerId).Fields["nameId"]),BotForgeAPI.Logger.LogEnum.Connection );
                //account.SocketManager.Connect(new ConnectionInformations(msg.Address, msg.Port, "of game"));
            }
        }

        [MessageHandler(typeof(ServerStatusUpdateMessage))]
        public static void ServerStatusUpdateMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            // Lecture du paquet ServerStatusUpdateMessage
            ServerStatusUpdateMessage serverStatusUpdateMessage = (ServerStatusUpdateMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                serverStatusUpdateMessage.Deserialize(reader);
            }
            // Cherche le statut du serveur
            ServerStatus.Test((ServerStatusEnum)serverStatusUpdateMessage.Server.Status, account);
        }

        [MessageHandler(typeof(ServersListMessage))]
        public static void ServerListMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            ServersListMessage msg = new ServersListMessage();
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }

            account.Logger.Log("< --- Probably, your server is under maintenance --- >", BotForgeAPI.Logger.LogEnum.Connection);
            msg.Servers.ToList().ForEach(server => account.Logger.Log("< --- Server : " +
                Common.Data.I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Servers, server.ObjectId).Fields["nameId"])
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
        public static void HelloGameMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            if (account.IsFullSocket)
            {
                AuthenticationTicketMessage authenticationTicketMessage = new AuthenticationTicketMessage(GameConstants.Lang,
                account.Ticket);
                //account.SocketManager.Send(authenticationTicketMessage);
            }
        }
        [MessageHandler(typeof(AuthenticationTicketAcceptedMessage))]
        public static void AuthenticationTicketAcceptedMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            if (account.IsFullSocket)
            {
                CharactersListRequestMessage charactersListRequestMessage = new CharactersListRequestMessage();
                //account.SocketManager.Send(charactersListRequestMessage);
            }
        }

        [MessageHandler(typeof(AuthenticationTicketRefusedMessage))]
        public static void AuthenticationTicketAcceptedRefusedTreatment(Message message, byte[] packetDatas, Account account)
        {
            AuthenticationTicketRefusedMessage msg = new AuthenticationTicketRefusedMessage();
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.Logger.Log("Error : Authentication Ticket Refused", BotForgeAPI.Logger.LogEnum.TextInformationError);
        }

        [MessageHandler(typeof(SelectedServerRefusedMessage))]
        public static void SelectedServerRefusedMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            // Lecture du paquet ServerStatusUpdateMessage
            SelectedServerRefusedMessage selectedServerRefusedMessage = (SelectedServerRefusedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                selectedServerRefusedMessage.Deserialize(reader);
            }
            // Cherche le statut du serveur
            ServerStatus.Test((ServerStatusEnum)selectedServerRefusedMessage.ServerStatus, account);
        }
        [MessageHandler(typeof(IdentificationFailedForBadVersionMessage))]
        public static void IdentificationFailedForBadVersionMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            IdentificationFailedForBadVersionMessage identificationFailedForBadVersionMessage = (IdentificationFailedForBadVersionMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                identificationFailedForBadVersionMessage.Deserialize(reader);
            }
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
        public static void IdentificationFailedBannedMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            IdentificationFailedBannedMessage msg = (IdentificationFailedBannedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.Logger.Log("Sorry, your character is banned :(", BotForgeAPI.Logger.LogEnum.TextInformationError);
            //account.SocketManager.DisconnectFromGUI();
        }
        #endregion
    }
}