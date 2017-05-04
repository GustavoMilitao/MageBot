namespace DofusBot.Packet.Types.Game.Social
{
    public class AbstractSocialGroupInfos
    {
        public TypeEnum Type
        {
            get { return TypeEnum.AbstractSocialGroupInfos; }
        }

        public const short ProtocolId = 416;
        public virtual short TypeID { get { return ProtocolId; } }

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
