 

namespace BlueSheep.Protocol.Messages.Game.Friend
{
    public class FriendsGetListMessage : Message
    {
        public const int ProtocolId = 4001;
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
