namespace BlueSheep.Common.Protocol.Messages.Game.Initialization
{
    using BlueSheep.Engine.Types;

 	 public class CharacterLoadingCompleteMessage : Message 
    {
        public new const int ID = 6471;
        public override int MessageID { get { return ID; } }

        public CharacterLoadingCompleteMessage() { }

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
