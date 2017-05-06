namespace BlueSheep.Common.Protocol.Messages.Game.Character.Choice
{
    using BlueSheep.Engine.Types;

 	 public class CharactersListRequestMessage : Message 
    {
        public new const int ID = 150;
        public override int MessageID { get { return ID; } }

        public CharactersListRequestMessage() { }

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
