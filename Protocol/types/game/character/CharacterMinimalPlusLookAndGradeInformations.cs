namespace BlueSheep.Protocol.Types.Game.Character
{
    public class CharacterMinimalPlusLookAndGradeInformations : CharacterMinimalPlusLookInformations
    {
        public new const int ProtocolId = 193;
        public override int TypeID { get { return ProtocolId; } }

        public uint Grade { get; set; }

        public CharacterMinimalPlusLookAndGradeInformations() { }

        public CharacterMinimalPlusLookAndGradeInformations(uint grade)
        {
            Grade = grade;
        }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt(Grade);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Grade = reader.ReadVarUhInt();
        }
    }
}
