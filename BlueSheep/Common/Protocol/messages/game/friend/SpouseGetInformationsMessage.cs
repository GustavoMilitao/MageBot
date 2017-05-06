namespace BlueSheep.Common.Protocol.Messages.Game.Friend
{
    using BlueSheep.Engine.Types;

 	 public class SpouseGetInformationsMessage : Message 
    {
        public new const int ID = 6355;
        public override int MessageID { get { return ID; } }

        public SpouseGetInformationsMessage() { }

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
