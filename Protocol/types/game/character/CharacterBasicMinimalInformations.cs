namespace MageBot.Protocol.Types.Game.Character
{
    public class CharacterBasicMinimalInformations : AbstractCharacterInformation
    {
        public override int ProtocolId { get; } = 503;
        public override int TypeID { get { return ProtocolId; } }

        public string Name { get; set; }

        public CharacterBasicMinimalInformations() { }

        public CharacterBasicMinimalInformations(string name)
        {
            Name = name;
        }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Name);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Name = reader.ReadUTF();
        }
    }
}
