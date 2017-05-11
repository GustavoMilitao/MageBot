 

namespace BlueSheep.Protocol.Messages.Game.Character.Choice
{
    public class CharactersListRequestMessage : Message
    {
        public const int ProtocolId = 150;
        public override int MessageID { get { return ProtocolId; } }

        public CharactersListRequestMessage() { }

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
