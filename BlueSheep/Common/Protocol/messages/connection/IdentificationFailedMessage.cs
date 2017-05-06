namespace BlueSheep.Common.Protocol.Messages.Connection
{
    using BlueSheep.Engine.Types;

 	 public class IdentificationFailedMessage : Message 
    {
        public new const int ID = 20;
        public override int MessageID { get { return ID; } }

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