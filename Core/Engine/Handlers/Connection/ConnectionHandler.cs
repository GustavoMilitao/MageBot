using DataFiles.Data.D2o;
using BlueSheep.Util.IO;
using BlueSheep.Protocol.Messages.Connection;
using BlueSheep.Protocol.Messages.Game.Approach;
using BlueSheep.Protocol.Messages.Game.Character.Choice;
using BlueSheep.Engine.Constants;
using BlueSheep.Util.Enums.Internal;
using BlueSheep.Protocol.Messages;
using Util.Util.Text.Log;
using BlueSheep.Util.Text;
using BlueSheep.Util.Text.Connection;
using RSA;
using System;
using System.Collections.Generic;
using BlueSheep.Util.Enums.EnumHelper;
using BlueSheep.Protocol.Enums;
using BlueSheep.Engine.Network;
using BlueSheep.Protocol.Types;

namespace BlueSheep.Engine.Handlers.Connection
{
    class ConnectionHandler
    {
        #region Public methods
        [MessageHandler(typeof(HelloConnectMessage))]
        public static void HelloConnectMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            account.SetStatus(Status.None);
            if (!account.Config.IsMITM)
            {
                HelloConnectMessage helloConnectMessage = (HelloConnectMessage)message;
                using (BigEndianReader reader = new BigEndianReader(packetDatas))
                {
                    helloConnectMessage.Deserialize(reader);
                }
                sbyte[] credentials = RSAKey.Encrypt(helloConnectMessage.key,
                account.AccountName,
                account.AccountPassword,
                helloConnectMessage.salt);
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
                account.SocketManager.Send(msg);
            }
            account.Log(new ConnectionTextInformation("Identification en cours."), 0);
        }

        [MessageHandler(typeof(IdentificationSuccessMessage))]
        public static void IdentificationSuccessMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
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
            account.Log(new ConnectionTextInformation("Identification réussie."), 0);
        }

        [MessageHandler(typeof(IdentificationFailedMessage))]
        public static void IdentificationFailedMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            IdentificationFailedMessage identificationFailedMessage = (IdentificationFailedMessage)message;
            account.Log(new ErrorTextInformation("Echec de l'identification."), 0);
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                identificationFailedMessage.Deserialize(reader);
            }
            IdentificationFailureReason.Test((IdentificationFailureReasonEnum)identificationFailedMessage.Reason, account);
            account.SocketManager.DisconnectFromGUI();
        }

        [MessageHandler(typeof(SelectedServerDataExtendedMessage))]
        public async static void SelectedServerDataExtendedMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            SelectedServerDataExtendedMessage msg = (SelectedServerDataExtendedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            //account.Log(new BotTextInformation(selectedServerDataExtendedMessage.address + " " + (int)selectedServerDataExtendedMessage.port));
            account.Ticket = AES.AES.TicketTrans(msg.Ticket).ToString();
            account.HumanCheck = new HumanCheck(account);
            account.SocketManager.IsChangingServer = true;
            if (!account.Config.IsMITM)
            {
                account.Log(new ConnectionTextInformation("Connexion au serveur " + DataFiles.Data.I18n.I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Servers, msg.ServerId).Fields["nameId"])), 0);
                account.SocketManager.Connect(new ConnectionInformations(msg.Address, msg.Port, "de jeu"));
            }
            else
            {
                SelectedServerDataExtendedMessage nmsg = new SelectedServerDataExtendedMessage(msg.CanCreateNewCharacter,
                                                                                               msg.ServerId,
                                                                                               msg.Address,
                                                                                               msg.Port,
                                                                                               msg.Ticket,
                                                                                               msg.ServerIds);
                using (BigEndianWriter writer = new BigEndianWriter())
                {
                    nmsg.Serialize(writer);
                    nmsg.Pack(writer);
                    account.SocketManager.SendToDofusClient(writer.Content);
                    //account.SocketManager.DisconnectFromDofusClient();
                    account.SocketManager.DisconnectServer("42 packet handling.");
                    account.SocketManager.ListenDofus();
                    await account.PutTaskDelay(200);
                }
                account.Log(new ConnectionTextInformation("Connexion au serveur " + DataFiles.Data.I18n.I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Servers, msg.ServerId).Fields["nameId"])), 0);
                account.SocketManager.Connect(new ConnectionInformations(msg.Address, msg.Port, "of game"));
            }
        }

        [MessageHandler(typeof(ServerStatusUpdateMessage))]
        public static void ServerStatusUpdateMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
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
        public static void ServerListMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            ServersListMessage msg = new ServersListMessage();
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }

            account.Log(new ConnectionTextInformation("< --- Probably, your server is under maintenance --- >"), 0);
            msg.Servers.ForEach(server => account.Log(new ConnectionTextInformation("< --- Server : " +
                DataFiles.Data.I18n.I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Servers, server.ObjectID).Fields["nameId"])
                         + " Status : " + ((ServerStatusEnum)server.Status).Description() + " --- >"), 0));

            //foreach (GameServerInformations gsi in msg.servers)
            //{
            //    account.Log(new ConnectionTextInformation("Server : "+
            //        IntelliSense.ServersList.Where(server => server.Id == gsi.id).FirstOrDefault().Name), 0);
            //}

            account.Log(new ConnectionTextInformation("Serveur complet."), 0);
            account.TryReconnect(600);
        }

        [MessageHandler(typeof(HelloGameMessage))]
        public static void HelloGameMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            if (!account.Config.IsMITM)
            {
                AuthenticationTicketMessage authenticationTicketMessage = new AuthenticationTicketMessage(GameConstants.Lang,
                account.Ticket);
                account.SocketManager.Send(authenticationTicketMessage);
            }
        }
        [MessageHandler(typeof(AuthenticationTicketAcceptedMessage))]
        public static void AuthenticationTicketAcceptedMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            if (!account.Config.IsMITM)
            {
                CharactersListRequestMessage charactersListRequestMessage = new CharactersListRequestMessage();
                account.SocketManager.Send(charactersListRequestMessage);
            }
        }

        [MessageHandler(typeof(AuthenticationTicketRefusedMessage))]
        public static void AuthenticationTicketAcceptedRefusedTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            AuthenticationTicketRefusedMessage msg = new AuthenticationTicketRefusedMessage();
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.Log(new ErrorTextInformation("Error : Authentication Ticket Refused"), 0);
        }

        [MessageHandler(typeof(SelectedServerRefusedMessage))]
        public static void SelectedServerRefusedMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
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
        public static void IdentificationFailedForBadVersionMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            IdentificationFailedForBadVersionMessage identificationFailedForBadVersionMessage = (IdentificationFailedForBadVersionMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                identificationFailedForBadVersionMessage.Deserialize(reader);
            }
            account.Log(new ErrorTextInformation("Echec de connexion : Dofus a été mis à jour ("
            + identificationFailedForBadVersionMessage.RequiredVersion.Major + "."
            + identificationFailedForBadVersionMessage.RequiredVersion.Minor + "."
            + identificationFailedForBadVersionMessage.RequiredVersion.Release + "."
            + identificationFailedForBadVersionMessage.RequiredVersion.Revision + "."
            + identificationFailedForBadVersionMessage.RequiredVersion.Patch + "."
            + identificationFailedForBadVersionMessage.RequiredVersion.BuildType + ")."
            + " BlueSheep supporte uniquement la version " + GameConstants.Major + "."
            + GameConstants.Minor + "." + GameConstants.Release + "."
            + GameConstants.Revision + "." + GameConstants.Patch + "."
            + GameConstants.BuildType + " du jeu. Consultez le forum pour être alerté de la mise à jour du bot."), 0);
            account.SocketManager.DisconnectFromGUI();
        }
        [MessageHandler(typeof(IdentificationFailedBannedMessage))]
        public static void IdentificationFailedBannedMessageTreatment(Message message, byte[] packetDatas, Core.Account.Account account)
        {
            IdentificationFailedBannedMessage msg = (IdentificationFailedBannedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.Log(new ConnectionTextInformation("Echec de connexion : Vous êtes bannis."), 0);
            account.SocketManager.DisconnectFromGUI();
        }
        #endregion
    }
}