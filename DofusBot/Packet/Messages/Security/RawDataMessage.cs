using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Security
{
    public class RawDataMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.RawDataMessage; }
        }

        public const uint ProtocolId = 6253;
        public override uint MessageID { get { return ProtocolId; } }

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
