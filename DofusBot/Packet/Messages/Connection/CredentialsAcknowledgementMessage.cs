using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Connection
{
    public class CredentialsAcknowledgementMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.CredentialsAcknowledgementMessage; }
        }

        public const uint ProtocolId = 6314;
        public override uint MessageID { get { return ProtocolId; } }

        public CredentialsAcknowledgementMessage() { }

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