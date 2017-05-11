 

namespace BlueSheep.Protocol.Messages.Game.Initialization
{
    public class CharacterLoadingCompleteMessage : Message
    {
        public const int ProtocolId = 6471;
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
