 

namespace MageBot.Protocol.Messages.Game.Friend
{
    public class FriendsGetListMessage : Message
    {
        protected override int ProtocolId { get; set; } = 4001;
        public override int MessageID { get { return ProtocolId; } }

        public FriendsGetListMessage() { }

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
