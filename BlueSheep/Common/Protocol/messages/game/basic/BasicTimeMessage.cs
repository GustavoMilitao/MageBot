 

namespace BlueSheep.Common.Protocol.Messages.Game.Basic
{
    class BasicTimeMessage : Message
    {
        public const int ProtocolId = 175;
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
