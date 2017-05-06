namespace BlueSheep.Common.Protocol.Messages.Game.Character.Choice
{
    using BlueSheep.Engine.Types;

 	 public class CharacterSelectionMessage : Message 
    {
        public new const int ID = 152;
        public override int MessageID { get { return ID; } }

        public ulong Id { get; set; }

        public CharacterSelectionMessage() { }

        public CharacterSelectionMessage(ulong id)
        {
            Id = id;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarLong(Id);
        }

        public override void Deserialize(IDataReader reader)
        {
            Id = reader.ReadVarUhLong();
        }
    }
}
