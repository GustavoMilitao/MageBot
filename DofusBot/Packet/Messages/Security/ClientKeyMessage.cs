using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Security
{
    public class ClientKeyMessage : NetworkMessage
    {
        public ClientPacketEnum PacketType
        {
            get { return ClientPacketEnum.ClientKeyMessage; }
        }

        public const uint ProtocolId = 5607;
        public override uint MessageID { get { return ProtocolId; } }

        public string Key { get; set; }

        public ClientKeyMessage() { }

        public ClientKeyMessage(string key)
        {
            Key = key;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Key);
        }

        public override void Deserialize(IDataReader reader)
        {
            _keyFunc(reader);
        }

        private void _keyFunc(IDataReader Reader)
        {
            Key = Reader.ReadUTF();
        }
    }
}
