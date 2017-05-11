 

namespace BlueSheep.Protocol.Messages.Game.Friend
{
    public class SpouseGetInformationsMessage : Message
    {
        public const int ProtocolId = 6355;
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
