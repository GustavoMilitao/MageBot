namespace DofusBot.Packet
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
        AuthenticationTicketAcceptedMessage = 111,
        AuthenticationTicketRefusedMessage = 112,
        CharactersListMessage = 151,
        BasicTimeMessage = 175,
        TextInformationMessage = 780,
        QueueStatusMessage = 6100,
        AccountCapabilitiesMessage = 6216,
        RawDataMessage = 6253,
        TrustStatusMessage = 6267,
        ServerOptionalFeaturesMessage = 6305,
        CredentialsAcknowledgementMessage = 6314,
        ServerSettingsMessage = 6340,
        BasicAckMessage = 6362,
        ServerSessionConstantsMessage = 6434,
        SelectedServerDataExtendedMessage = 6469,
        CharacterLoadingCompleteMessage = 6471,
        BasicCharactersListMessage = 6475,
        CurrentMapMessage = 220,
    }
}
