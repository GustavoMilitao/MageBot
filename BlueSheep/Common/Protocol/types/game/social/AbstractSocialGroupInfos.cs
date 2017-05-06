namespace BlueSheep.Common.Protocol.Types.Game.Social
{
    public class AbstractSocialGroupInfos
    {
        public new const int ID = 416;
        public virtual int TypeID { get { return ID; } }

        public AbstractSocialGroupInfos() { }

        public virtual void Serialize(IDataWriter writer)
        {
            //
        }

        public virtual void Deserialize(IDataReader reader)
        {
            //
        }
    }
}
