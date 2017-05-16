namespace BlueSheep.Protocol.Types.Game.Social
{
    public class AbstractSocialGroupInfos
    {
        protected virtual int ProtocolId { get; set; } = 416;
        public virtual int TypeID { get { return ProtocolId; } }

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
