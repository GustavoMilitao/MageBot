namespace DofusBot.Packet.Types.Game.Approach
{
    public class ServerSessionConstant : NetworkType
    {
        public TypeEnum Type
        {
            get { return TypeEnum.ServerSessionConstant; }
        }

        public const short ProtocolId = 430;
        public override short TypeID { get { return ProtocolId; } }

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
