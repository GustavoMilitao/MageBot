namespace DofusBot.Packet.Types.Game.Character
{
    public class AbstractCharacterInformation : NetworkType
    {
        public TypeEnum Type
        {
            get { return TypeEnum.AbstractCharacterInformation; }
        }

        public const short ProtocolId = 400;
        public override short TypeID { get { return ProtocolId; } }

        public double ObjectID { get; set; }

        public AbstractCharacterInformation() { }

        public AbstractCharacterInformation(double objectId)
        {
            ObjectID = objectId;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarLong((long)ObjectID);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectID = reader.ReadVarUhLong();
        }
    }
}
