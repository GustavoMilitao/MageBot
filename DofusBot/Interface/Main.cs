using DofusBot.Enums;
using DofusBot.Misc;
using DofusBot.Network;
using DofusBot.Packet;
using DofusBot.Packet.Messages.Connection;
using DofusBot.Packet.Messages.Game.Approach;
using DofusBot.Packet.Messages.Game.Basic;
using DofusBot.Packet.Messages.Game.Character.Choice;
using DofusBot.Packet.Messages.Game.Chat.Channel;
using DofusBot.Packet.Messages.Game.Context;
using DofusBot.Packet.Messages.Game.Context.Roleplay;
using DofusBot.Packet.Messages.Game.Friend;
using DofusBot.Packet.Messages.Queues;
using DofusBot.Packet.Messages.Secure;
using DofusBot.Packet.Messages.Security;
using DofusBot.Packet.Types;
using DofusBot.Packet.Types.Connection;
using DofusBot.Packet.Types.Game.Character.Choice;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace DofusBot.Interface
{
    public partial class Main : Form
    {
        private DofusBotSocket _ServerSocket;
        private DofusBotSocket _GameSocket;
        private DofusBotPacketDeserializer _deserializer;
        private object _ticket;

        public Main()
        {
            InitializeComponent();
            _deserializer = new DofusBotPacketDeserializer();
            _deserializer.ReceivePacket += OnReceivedPacket;
            _deserializer.ReceiveNullPacket += OnReceivedNullPacket;

            logTextBox.Font = new Font("Tahoma", 8, FontStyle.Bold);
        }

        private void Log(LogMessageType type, string Text)
        {
            Action log_callback = delegate
            {
                Console.WriteLine(Text);

                switch (type)
                {
                    case LogMessageType.Global:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#E9E9E9");
                        break;
                    case LogMessageType.Team:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#FF006C");
                        break;
                    case LogMessageType.Guild:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#975FFF");
                        break;
                    case LogMessageType.Alliance:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#FFAD42");
                        break;
                    case LogMessageType.Party:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#00E4FF");
                        break;
                    case LogMessageType.Sales:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#B3783E");
                        break;
                    case LogMessageType.Seek:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#E4A0D5");
                        break;
                    case LogMessageType.Noob:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#D3AA07");
                        break;
                    case LogMessageType.Admin:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#FF00FF");
                        break;
                    case LogMessageType.Private:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#7EC3FF");
                        break;
                    case LogMessageType.Info:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#46A324");
                        break;
                    case LogMessageType.FightLog:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#9DFF00");
                        break;
                    case LogMessageType.Public:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#EF3A3E");
                        break;
                    case LogMessageType.Arena:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#F16392");
                        break;
                    case LogMessageType.Community:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#9EC79D");
                        break;
                    case LogMessageType.Sender:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#1B96FF");
                        break;
                    case LogMessageType.Default:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#E8890D");
                        break;
                    default:
                        logTextBox.SelectionColor = ColorTranslator.FromHtml("#E8890D");
                        break;
                }

                logTextBox.AppendText("[");
                logTextBox.AppendText(DateTime.Now.ToLongTimeString());
                logTextBox.AppendText("] " + Text + "\r\n");
                logTextBox.SelectionColor = logTextBox.ForeColor;
                logTextBox.Select(logTextBox.Text.Length, 0);
                logTextBox.ScrollToCaret();
            };
            Invoke(log_callback);
        }

        private void ConnectionButton_Click(object sender, EventArgs e)
        {
            string Connect = "Connexion";
            string Disconnect = "Deconnexion";

            Invoke((MethodInvoker)delegate
            {
                logTextBox.Text = "";

                if (connectionButton.Text == Connect)
                {
                    if (string.IsNullOrWhiteSpace(accountNameTextBox.Text) || string.IsNullOrWhiteSpace(accountPasswdTextBox.Text))
                        Log(LogMessageType.Admin, "Vous devez rentrer vos identifiants.");
                    else
                    {
                        string DofusIP = "213.248.126.40";
                        int DofusPort = 5555;
                        _ServerSocket = new DofusBotSocket(_deserializer, new IPEndPoint(IPAddress.Parse(DofusIP), DofusPort));
                        Log(LogMessageType.Info, "Connexion en cours <" + DofusIP + ":" + DofusPort + ">");
                        _ServerSocket.ConnectEndListen();

                        connectionButton.Text = Disconnect;
                    }
                }
                else
                {
                    _ServerSocket.CloseSocket();
                    _ServerSocket = null;
                    
                    if (_GameSocket != null)
                    {
                        _GameSocket.CloseSocket();
                        _GameSocket = null;
                    }

                    Log(LogMessageType.Info, "Déconnecté.");
                    connectionButton.Text = Connect;
                }
            });             
        }

        public void OnReceivedNullPacket(object source, NullPacketEventArg e)
        {
            Log(LogMessageType.Admin, "Packet: [" + e.PacketType + "] is not implemented.");
        }

        public void OnReceivedPacket(object source, PacketEventArg e)
        {
            ServerPacketEnum packetType = (ServerPacketEnum)e.Packet.MessageID;
            switch (packetType)
            {
                case ServerPacketEnum.ProtocolRequired:
                    break;
                case ServerPacketEnum.CredentialsAcknowledgementMessage:
                    break;
                case ServerPacketEnum.BasicAckMessage:
                    break;
                case ServerPacketEnum.TextInformationMessage:
                    TextInformationMessage text = (TextInformationMessage)e.Packet;
                    Log(LogMessageType.Arena, ((TextInformationTypeEnum)text.MsgType).ToString() + "ID = " + text.MsgId);
                    for (int i = 0; i < text.Parameters.Count; i++)
                    {
                        string t = text.Parameters[i];
                        Log(LogMessageType.Arena, "Parameter[" + i + "] " + t);
                    }
                    break;
                case ServerPacketEnum.HelloGameMessage:
                    Log(LogMessageType.Info, "Connecté au serveur de jeu.");
                    HelloGameMessage helloGame = (HelloGameMessage)e.Packet;
                    AuthenticationTicketMessage ATM = new AuthenticationTicketMessage("fr", _ticket.ToString());
                    _GameSocket.Send(ATM);
                    break;
                case ServerPacketEnum.RawDataMessage:
                    List<int> tt = new List<int>();
                    for (int i = 0; i <= 255; i++)
                    {
                        Random random = new Random();
                        int test = random.Next(-127, 127);
                    }
                    CheckIntegrityMessage rawData = new CheckIntegrityMessage(tt);
                    _GameSocket.Send(rawData);
                    break;
                case ServerPacketEnum.HelloConnectMessage:
                    Log(LogMessageType.Info, "Connecté au serveur d'authentification.");
                    HelloConnectMessage helloConnectMessage = (HelloConnectMessage)e.Packet;
                    sbyte[] credentials = RSA.RSAKey.Encrypt(helloConnectMessage.key, accountNameTextBox.Text, accountPasswdTextBox.Text, helloConnectMessage.salt);
                    VersionExtended version = new VersionExtended(2, 41, 1, 120264, 1, (sbyte)BuildTypeEnum.RELEASE, 1, 1);
                    IdentificationMessage idm = new IdentificationMessage(false, false, false, version, "fr", credentials, 0, 0, new ushort[0]);
                    Log(LogMessageType.Info, "Envois des informations d'identification...");
                    _ServerSocket.Send(idm);
                    break;
                case ServerPacketEnum.LoginQueueStatusMessage:
                    LoginQueueStatusMessage loginQueueStatusMessage = (LoginQueueStatusMessage)e.Packet;
                    if (loginQueueStatusMessage.Position != 0 && loginQueueStatusMessage.Total != 0)
                        Log(LogMessageType.Info, "Vous êtes en position " + loginQueueStatusMessage.Position + " sur " + loginQueueStatusMessage.Total + " dans la file d'attente.");
                    break;
                case ServerPacketEnum.CurrentMapMessage:
                    CurrentMapMessage currentMap = (CurrentMapMessage)e.Packet;
                    _GameSocket.Send(new MapInformationsRequestMessage(currentMap.MapId));
                    break;
                case ServerPacketEnum.QueueStatusMessage:
                    QueueStatusMessage queueStatusMessage = (QueueStatusMessage)e.Packet;
                    if (queueStatusMessage.Position != 0 && queueStatusMessage.Total != 0)
                        Log(LogMessageType.Info, "Vous êtes en position " + queueStatusMessage.Position + " sur " + queueStatusMessage.Total + " dans la file d'attente.");
                    break;
                case ServerPacketEnum.IdentificationFailedMessage:
                    IdentificationFailedMessage msg = (IdentificationFailedMessage)e.Packet;
                    Log(LogMessageType.Public, "Identification échouée !");
                    Log(LogMessageType.Public, ((IdentificationFailureReasonEnum)msg.Reason).ToString());
                    Invoke((MethodInvoker)delegate
                    {
                        if (_ServerSocket != null)
                        {
                            _ServerSocket.CloseSocket();
                            _ServerSocket = null;
                        }
                        connectionButton.Text = "Connexion";
                    });
                    break;
                case ServerPacketEnum.IdentificationSuccessMessage:
                    IdentificationSuccessMessage idSuccess = (IdentificationSuccessMessage)e.Packet;
                    break;
                case ServerPacketEnum.ServerListMessage:
                    ServerListMessage servers = (ServerListMessage)e.Packet;
                    foreach(GameServerInformations i in servers.Servers ){
                        if (i.CharactersCount > 0 && i.IsSelectable && (ServerStatusEnum)i.Status == ServerStatusEnum.ONLINE)
                        {
                            _ServerSocket.Send(new ServerSelectionMessage(i.ObjectID));
                            break;
                        }
                    }
                    break;
                case ServerPacketEnum.SelectedServerDataMessage:
                    SelectedServerDataMessage selected = (SelectedServerDataMessage)e.Packet;
                    Log(LogMessageType.Info, "Connexion au serveur " + selected.ServerId + "...");     
                    _ticket = AES.AES.TicketTrans(selected.Ticket);
                    _GameSocket = new DofusBotSocket(_deserializer, new IPEndPoint(IPAddress.Parse(selected.Address), selected.Port));
                    Log(LogMessageType.Info, "Connexion en cours <" + selected.Address + ":" + selected.Port + ">");
                    _GameSocket.ConnectEndListen();
                    _ServerSocket.CloseSocket();
                    _ServerSocket = null;
                    break;
                case ServerPacketEnum.SelectedServerDataExtendedMessage:
                    SelectedServerDataExtendedMessage selectedExtended = (SelectedServerDataExtendedMessage)e.Packet;
                    Log(LogMessageType.Info, "Connecté au serveur : " + selectedExtended.ServerId);
                    _ticket = AES.AES.TicketTrans(selectedExtended.Ticket);
                    _GameSocket = new DofusBotSocket(_deserializer, new IPEndPoint(IPAddress.Parse(selectedExtended.Address), selectedExtended.Port));
                    Log(LogMessageType.Info, "Connexion en cours <" + selectedExtended.Address + ":" + selectedExtended.Port + ">");
                    _GameSocket.ConnectEndListen();
                    _ServerSocket.CloseSocket();
                    _ServerSocket = null;
                    break;
                case ServerPacketEnum.AuthenticationTicketAcceptedMessage:
                    AuthenticationTicketAcceptedMessage accepted = (AuthenticationTicketAcceptedMessage)e.Packet;
                    Thread.Sleep(500);
                    _GameSocket.Send(new CharactersListRequestMessage());
                    break;
                case ServerPacketEnum.AuthenticationTicketRefusedMessage:
                    AuthenticationTicketRefusedMessage refused = (AuthenticationTicketRefusedMessage)e.Packet;
                    break;
                case ServerPacketEnum.BasicTimeMessage:
                    BasicTimeMessage time = (BasicTimeMessage)e.Packet;
                    break;
                case ServerPacketEnum.ServerSettingsMessage:
                    ServerSettingsMessage serverSettings = (ServerSettingsMessage)e.Packet;
                    break;
                case ServerPacketEnum.ServerOptionalFeaturesMessage:
                    ServerOptionalFeaturesMessage serverFeatures = (ServerOptionalFeaturesMessage)e.Packet;
                    break;
                case ServerPacketEnum.ServerSessionConstantsMessage:
                    ServerSessionConstantsMessage serverSession = (ServerSessionConstantsMessage)e.Packet;
                    break;
                case ServerPacketEnum.AccountCapabilitiesMessage:
                    AccountCapabilitiesMessage accountCapabilities = (AccountCapabilitiesMessage)e.Packet;
                    break;
                case ServerPacketEnum.TrustStatusMessage:
                    TrustStatusMessage trust = (TrustStatusMessage)e.Packet;
                    break;
                case ServerPacketEnum.CharacterLoadingCompleteMessage:
                    _GameSocket.Send(new FriendsGetListMessage());
                    _GameSocket.Send(new IgnoredGetListMessage());
                    _GameSocket.Send(new SpouseGetInformationsMessage());
                    _GameSocket.Send(new ClientKeyMessage(FlashKey.GetRandomFlashKey()));
                    _GameSocket.Send(new GameContextCreateRequestMessage());
                    _GameSocket.Send(new ChannelEnablingMessage(7, false));
                    break;
                case ServerPacketEnum.CharactersListMessage:
                    CharactersListMessage charactersList = (CharactersListMessage)e.Packet;
                    List<CharacterBaseInformations> characters = charactersList.Characters;
                    for (int i = 0; i < characters.Count; i++)
                    {
                        CharacterBaseInformations c = characters[i];
                        if (c.Level > 0)
                        {
                            Log(LogMessageType.Info, "Connexion sur le personnage " + c.Name);
                            _GameSocket.Send(new CharacterSelectionMessage((ulong)c.ObjectID));
                            break;
                        }
                    }
                    break;
                default:
                    Log(LogMessageType.Admin, "Packet: [" + (ServerPacketEnum)e.Packet.MessageID + "] is not handled.");
                    break;
            }
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (_ServerSocket != null || _GameSocket != null)
            {
                if (MessageBox.Show("Une connexion est en cours. Voulez-vous vraiment fermer le bot? Si oui, la déconnexion s'effectuera automatiquement.", "Attention", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (_ServerSocket != null)
                    {
                        _ServerSocket.CloseSocket();
                        _ServerSocket = null;
                    }
                    if (_GameSocket != null)
                    {
                        _GameSocket.CloseSocket();
                        _GameSocket = null;
                    }
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //
        }
    }
}
