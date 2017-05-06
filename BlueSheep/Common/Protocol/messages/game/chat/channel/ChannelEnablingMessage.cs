namespace BlueSheep.Common.Protocol.Messages.Game.Chat.Channel
{
    using BlueSheep.Engine.Types;

 	 public class ChannelEnablingMessage : Message 
    {
        public new const int ID = 890;
        public override int MessageID { get { return ID; } }

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
