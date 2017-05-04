using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Game.Approach
{
    class AuthenticationTicketRefusedMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.AuthenticationTicketRefusedMessage; }
        }

        public const uint ProtocolId = 112;
        public override uint MessageID { get { return ProtocolId; } }

        public AuthenticationTicketRefusedMessage() { }

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
