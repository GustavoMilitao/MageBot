 

namespace MageBot.Protocol.Messages.Game.Basic
{
    public class BasicTimeMessage : Message
    {
        public override int ProtocolId { get; } = 175;
        public override int MessageID { get { return ProtocolId; } }

        public double Timestamp;
        public short TimezoneOffset;

        public BasicTimeMessage() { }

        public BasicTimeMessage(double timestamp, short timezoneOffset)
        {
            Timestamp = timestamp;
            TimezoneOffset = timezoneOffset;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(Timestamp);
            writer.WriteShort(TimezoneOffset);
        }

        public override void Deserialize(IDataReader reader)
        {
            Timestamp = reader.ReadDouble();
            TimezoneOffset = reader.ReadShort();
        }
    }
}
