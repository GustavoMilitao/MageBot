namespace BlueSheep.Common.Protocol.Messages.Game.Friend
{
    using BlueSheep.Engine.Types;

 	 public class IgnoredGetListMessage : Message 
    {
        public new const int ID = 5676;
        public override int MessageID { get { return ID; } }

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
