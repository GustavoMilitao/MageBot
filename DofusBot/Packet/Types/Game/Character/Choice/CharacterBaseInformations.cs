namespace DofusBot.Packet.Types.Game.Character.Choice
{
    public class CharacterBaseInformations : CharacterMinimalPlusLookInformations
    {
        public new TypeEnum Type
        {
            get { return TypeEnum.CharacterBaseInformations; }
        }

        public new const short ProtocolId = 45;
        public new short TypeID { get { return ProtocolId; } }

        public sbyte Breed { get; set; }
        public bool Sex { get; set; }

        public CharacterBaseInformations() { }

        public CharacterBaseInformations(sbyte breed, bool sex)
        {
            Breed = breed;
            Sex = sex;
        }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(Breed);
            writer.WriteBoolean(Sex);
        }

        public override void Deserialize(IDataReader reader)
        {         
            base.Deserialize(reader);
            Breed = reader.ReadSByte();
            Sex = reader.ReadBoolean();
        }
    }
}
