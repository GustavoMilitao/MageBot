 

namespace MageBot.Protocol.Messages.Game.Friend
{
    public class IgnoredGetListMessage : Message
    {
        public override int ProtocolId { get; } = 5676;
        public override int MessageID { get { return ProtocolId; } }

        public IgnoredGetListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            //
        }

        public override void Deserialize(IDataReader reader)
        {
            //
        }
    }
}
