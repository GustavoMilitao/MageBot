using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Queues
{
    class QueueStatusMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.QueueStatusMessage; }
        }

        public const uint ProtocolId = 6100;
        public override uint MessageID { get { return ProtocolId; } }

        public ushort Position;
        public ushort Total;

        public QueueStatusMessage() { }

        public QueueStatusMessage(ushort position, ushort total)
        {
            Position = position;
            Total = total;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Position);
            writer.WriteShort((short)Total);
        }

        public override void Deserialize(IDataReader reader)
        {
            Position = reader.ReadUShort();
            Total = reader.ReadUShort();
        }
    }
}
