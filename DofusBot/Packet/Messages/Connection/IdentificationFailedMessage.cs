using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Connection
{
    public class IdentificationFailedMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.IdentificationFailedMessage; }
        }

        public uint Reason = 99;
        public const uint ProtocolId = 20;
        public override uint MessageID { get { return ProtocolId; } }

        public IdentificationFailedMessage()
        {
        }

        public IdentificationFailedMessage(uint reason = 99)
        {
            Reason = reason;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)Reason);
        }

        public override void Deserialize(IDataReader reader)
        {
            Reason = reader.ReadByte();
        }
    }
}