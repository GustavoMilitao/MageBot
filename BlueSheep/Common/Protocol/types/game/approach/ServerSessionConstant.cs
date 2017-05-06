namespace BlueSheep.Common.Protocol.Types.Game.Approach
{
    public class ServerSessionConstant 
    {
        public new const int ID = 430;
        public virtual int TypeID { get { return ID; } }

        public ushort ObjectID { get; set; }

        public ServerSessionConstant() { }

        public ServerSessionConstant(ushort objectId)
        {
            ObjectID = objectId;
        }

        public void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(ObjectID);
        }

        public void Deserialize(IDataReader reader)
        {
            ObjectID = reader.ReadVarUhShort();
        }
    }
}
