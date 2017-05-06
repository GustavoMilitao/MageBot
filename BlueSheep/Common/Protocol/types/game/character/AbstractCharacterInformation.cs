namespace BlueSheep.Common.Protocol.Types.Game.Character
{
    public class AbstractCharacterInformation : NetworkType
    {
        public const int ProtocolId = 400;
        public override int TypeID { get { return ProtocolId; } }

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
