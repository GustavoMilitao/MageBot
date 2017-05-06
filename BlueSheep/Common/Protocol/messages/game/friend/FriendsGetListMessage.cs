namespace BlueSheep.Common.Protocol.Messages.Game.Friend
{
    using BlueSheep.Engine.Types;

 	 public class FriendsGetListMessage : Message 
    {
        public new const int ID = 4001;
        public override int MessageID { get { return ID; } }

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
