namespace BlueSheep.Protocol.Messages.Game.Character.Choice
{
    public class CharactersListMessage : BasicCharactersListMessage
    {
        protected override int ProtocolId { get; set; } = 151;
        public override int MessageID { get { return ProtocolId; } }

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
