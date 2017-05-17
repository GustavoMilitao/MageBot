 

namespace MageBot.Protocol.Messages.Connection
{
    public class IdentificationFailedMessage : Message
    {
        public override int ProtocolId { get; } = 20;
        public override int MessageID { get { return ProtocolId; } }

        public uint Reason = 99;

        public IdentificationFailedMessage() { }

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