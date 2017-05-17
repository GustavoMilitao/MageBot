 

namespace MageBot.Protocol.Messages.Game.Context
{
    public class GameContextCreateRequestMessage : Message
    {
        public override int ProtocolId { get; } = 250;
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
