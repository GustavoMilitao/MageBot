namespace BlueSheep.Common.Protocol.Types.Game.Character
{
    public class CharacterMinimalPlusLookAndGradeInformations : CharacterMinimalPlusLookInformations
    {
        public new const int ID = 193;
        public virtual int TypeID { get { return ID; } }

        public uint Grade { get; set; }

        public CharacterMinimalPlusLookAndGradeInformations() { }

        public CharacterMinimalPlusLookAndGradeInformations(uint grade)
        {
            Grade = grade;
        }

        public void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt(Grade);
        }

        public void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Grade = reader.ReadVarUhInt();
        }
    }
}
