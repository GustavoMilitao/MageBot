using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Game.Chat.Channel
{
    public class ChannelEnablingMessage : NetworkMessage
    {
        public ClientPacketEnum PacketType
        {
            get { return ClientPacketEnum.ChannelEnablingMessage; }
        }

        public const uint ProtocolId = 890;
        public override uint MessageID { get { return ProtocolId; } }

        public uint Channel { get; set; }
        public bool Enable { get; set; }

        public ChannelEnablingMessage() { }

        public ChannelEnablingMessage(uint channel, bool enable)
        {
            Channel = channel;
            Enable = enable;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)Channel);
            writer.WriteBoolean(Enable);
        }

        public override void Deserialize(IDataReader reader)
        {
            _channelFunc(reader);
            _enableFunc(reader);
        }

        private void _channelFunc(IDataReader reader)
        {
            Channel = reader.ReadByte();
        }

        private void _enableFunc(IDataReader reader)
        {
            Enable = reader.ReadBoolean();
        }
    }
}
