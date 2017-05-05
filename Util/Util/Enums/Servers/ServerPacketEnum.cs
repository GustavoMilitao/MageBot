namespace BlueSheep.Util.Enums.Servers
{
    public enum ServerPacketEnum
    {
        ProtocolRequired = 1,
        HelloConnectMessage = 3,
        LoginQueueStatusMessage = 10,
        IdentificationFailedMessage = 20,
        IdentificationSuccessMessage = 22,
        ServerListMessage = 30,
        SelectedServerDataMessage = 42,
        HelloGameMessage = 101,
        RawDataMessage = 6253,
        CredentialsAcknowledgementMessage = 6314,
        BasicAckMessage = 6362,
        SelectedServerDataExtendedMessage = 6469,
    }
}
