 

namespace MageBot.Protocol.Messages.Connection
{
    public class HelloConnectMessage : Message
    {
        public override int ProtocolId { get; } = 3;
        public override int MessageID { get { return ProtocolId; } }

        public string salt;
        public sbyte[] key;

        public HelloConnectMessage() { }

        public HelloConnectMessage(string salt, sbyte[] key)
        {
            this.salt = salt;
            this.key = key;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(this.salt);
            writer.WriteVarInt((ushort)this.key.Length);
            foreach (sbyte @byte in this.key)
                writer.WriteSByte(@byte);
        }

        public override void Deserialize(IDataReader reader)
        {
            this.salt = reader.ReadUTF();
            ushort num = (ushort)reader.ReadVarInt();
            this.key = new sbyte[num];
            for (int index = 0; index < num; ++index)
                this.key[index] = reader.ReadSByte();
        }
    }
}
