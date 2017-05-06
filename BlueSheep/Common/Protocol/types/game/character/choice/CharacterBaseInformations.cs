namespace BlueSheep.Common.Protocol.Types.Game.Character.Choice
{
    public class CharacterBaseInformations : CharacterMinimalPlusLookInformations
    {
        public new const int ID = 45;
        public virtual int TypeID { get { return ID; } }

        public sbyte Breed { get; set; }
        public bool Sex { get; set; }

        public CharacterBaseInformations() { }

        public CharacterBaseInformations(sbyte breed, bool sex)
        {
            Breed = breed;
            Sex = sex;
        }

        public void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(Breed);
            writer.WriteBoolean(Sex);
        }

        public void Deserialize(IDataReader reader)
        {         
            base.Deserialize(reader);
            Breed = reader.ReadSByte();
            Sex = reader.ReadBoolean();
        }
    }
}
