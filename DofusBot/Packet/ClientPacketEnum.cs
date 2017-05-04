namespace DofusBot.Packet
{
    public enum ClientPacketEnum
    {
        IdentificationMessage = 4,
        ServerSelectionMessage = 40,
        AuthenticationTicketMessage = 110,
        CharactersListRequestMessage = 150,
        CharacterSelectionMessage = 152,
        GameContextCreateRequestMessage = 250,
        CheckIntegrityMessage = 6372,
        FriendsGetListMessage = 4001,
        ClientKeyMessage = 5607,
        IgnoredGetListMessage = 5676,
        SpouseGetInformationsMessage = 6355,
        ChannelEnablingMessage = 890,
        MapInformationsRequestMessage = 225,
    }
}
