namespace BlueSheep.Common.Protocol.Types.Game.Character
{
    public class AbstractCharacterInformation 
    {
        public new const int ID = 400;
        public virtual int TypeID { get { return ID; } }

        public double ObjectID { get; set; }

        public AbstractCharacterInformation() { }

        public AbstractCharacterInformation(double objectId)
        {
            ObjectID = objectId;
        }

        public void Serialize(IDataWriter writer)
        {
            writer.WriteVarLong((long)ObjectID);
        }

        public void Deserialize(IDataReader reader)
        {
            ObjectID = reader.ReadVarUhLong();
        }
    }
}
