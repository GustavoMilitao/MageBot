namespace BlueSheep.Common.Protocol.Types.Game.Character
{
    public class CharacterBasicMinimalInformations : AbstractCharacterInformation
    {
        public new const int ID = 503;
        public virtual int TypeID { get { return ID; } }

        public string Name { get; set; }

        public CharacterBasicMinimalInformations() { }

        public CharacterBasicMinimalInformations(string name)
        {
            Name = name;
        }

        public void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Name);
        }

        public void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Name = reader.ReadUTF();
        }
    }
}
