namespace BlueSheep.Protocol.Types.Game.Character
{
    public class CharacterMinimalInformations : CharacterBasicMinimalInformations
    {
        public new const int ProtocolId = 110;
        public override int TypeID { get { return ProtocolId; } }

        public byte Level { get; set; }

        public CharacterMinimalInformations() { }

        public CharacterMinimalInformations(byte level)
        {
            Level = level;
        }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(Level);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Level = reader.ReadByte();
        }
    }
}
