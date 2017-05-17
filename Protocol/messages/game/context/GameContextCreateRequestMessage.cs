 

namespace MageBot.Protocol.Messages.Game.Context
{
    public class GameContextCreateRequestMessage : Message
    {
        protected override int ProtocolId { get; set; } = 250;
        public override int MessageID { get { return ProtocolId; } }

        public GameContextCreateRequestMessage() { }

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
