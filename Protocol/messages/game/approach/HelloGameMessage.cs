 

namespace BlueSheep.Protocol.Messages.Game.Approach
{
    public class HelloGameMessage : Message
    {
        public const int ProtocolId = 101;
        public override int MessageID { get { return ProtocolId; } }

        public HelloGameMessage() { }

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
