namespace MageBot.Protocol.Types.Game.Social
{
    public class AbstractSocialGroupInfos: NetworkType
    {
        public override int ProtocolId { get; } = 416;
        public override int TypeID { get { return ProtocolId; } }

        public AbstractSocialGroupInfos() { }

        public override void Serialize(IDataWriter writer)
        {
            //
        }

        public override void Deserialize(IDataReader reader)
        {
            //
        }
    }
}
