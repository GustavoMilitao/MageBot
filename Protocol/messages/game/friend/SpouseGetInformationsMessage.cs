 

namespace MageBot.Protocol.Messages.Game.Friend
{
    public class SpouseGetInformationsMessage : Message
    {
        protected override int ProtocolId { get; set; } = 6355;
        public override int MessageID { get { return ProtocolId; } }

        public SpouseGetInformationsMessage() { }

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
