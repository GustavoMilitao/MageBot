 

namespace MageBot.Protocol.Messages.Game.Initialization
{
    public class CharacterLoadingCompleteMessage : Message
    {
        public override int ProtocolId { get; } = 6471;
        public override int MessageID { get { return ProtocolId; } }

        public CharacterLoadingCompleteMessage() { }

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
