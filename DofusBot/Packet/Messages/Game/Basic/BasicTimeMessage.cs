using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Game.Basic
{
    class BasicTimeMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.BasicTimeMessage; }
        }

        public double Timestamp;
        public short TimezoneOffset;

        public const uint ProtocolId = 175;
        public override uint MessageID { get { return ProtocolId; } }

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
