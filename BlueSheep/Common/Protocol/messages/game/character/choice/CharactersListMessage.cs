namespace BlueSheep.Common.Protocol.Messages.Game.Character.Choice
{
    using BlueSheep.Engine.Types;

 	 public class CharactersListMessage : BasicCharactersListMessage 
    {
        public new const int ID = 151;
        public override int MessageID { get { return ID; } }

        public bool HasStartupActions;

        public CharactersListMessage() { }

        public CharactersListMessage(bool hasStartupActions)
        {
            HasStartupActions = hasStartupActions;
        }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(HasStartupActions);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            HasStartupActions = reader.ReadBoolean();
        }
    }
}
