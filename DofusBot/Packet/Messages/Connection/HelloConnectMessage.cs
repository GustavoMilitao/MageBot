using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Connection
{
    public class HelloConnectMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.HelloConnectMessage; }
        }

        public string salt;
        public sbyte[] key;
        public const uint ProtocolId = 3;
        public override uint MessageID { get { return ProtocolId; } }

        public HelloConnectMessage()
        {
        }

        public HelloConnectMessage(string salt, sbyte[] key)
        {
            this.salt = salt;
            this.key = key;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(this.salt);
            writer.WriteVarInt((int)(ushort)this.key.Length);
            foreach (sbyte @byte in this.key)
                writer.WriteSByte(@byte);
        }

        public override void Deserialize(IDataReader reader)
        {
            this.salt = reader.ReadUTF();
            ushort num = (ushort)reader.ReadVarInt();
            this.key = new sbyte[(int)num];
            for (int index = 0; index < (int)num; ++index)
                this.key[index] = reader.ReadSByte();
        }
    }
}