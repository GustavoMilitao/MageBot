using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Game.Approach
{
    class AuthenticationTicketAcceptedMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.AuthenticationTicketAcceptedMessage; }
        }

        public const uint ProtocolId = 111;
        public override uint MessageID { get { return ProtocolId; } }

        public AuthenticationTicketAcceptedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            //
        }

        public override void Deserialize(IDataReader reader)
        {
            //
        }
    }
}
