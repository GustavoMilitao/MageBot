namespace BlueSheep.Protocol.Types.Game.Approach
{
    public class ServerSessionConstant : NetworkType
    {
        public const int ProtocolId = 430;
        public override int TypeID { get { return ProtocolId; } }

        public ushort ObjectID { get; set; }

        public ServerSessionConstant() { }

        public ServerSessionConstant(ushort objectId)
        {
            ObjectID = objectId;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(ObjectID);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectID = reader.ReadVarUhShort();
        }
    }
}
