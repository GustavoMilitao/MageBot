 

namespace MageBot.Protocol.Messages.Queues
{
    public class QueueStatusMessage : Message
    {
        public override int ProtocolId { get; } = 6100;
        public override int MessageID { get { return ProtocolId; } }

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
