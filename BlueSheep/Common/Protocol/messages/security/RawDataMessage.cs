namespace BlueSheep.Common.Protocol.Messages.Security
{
    using BlueSheep.Engine.Types;

 	 public class RawDataMessage : Message 
    {
        public new const int ID = 6253;
        public override int MessageID { get { return ID; } }

        public byte[] Content { get; set; }

        public RawDataMessage() { }

        public RawDataMessage(byte[] content)
        {
            Content = content;
        }

        public override void Serialize(IDataWriter writer)
        {
            int contentLength = Content.Length;
            writer.WriteVarInt(contentLength);
            for (int i = 0; i < contentLength; i++)
            {
                writer.WriteByte(Content[i]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            int contentLength = reader.ReadVarInt();
            reader.ReadBytes(contentLength);
        }

    }
}
