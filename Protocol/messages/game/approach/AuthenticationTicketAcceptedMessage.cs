 

namespace MageBot.Protocol.Messages.Game.Approach
{
    public class AuthenticationTicketAcceptedMessage : Message
    {
        public override int ProtocolId { get; } = 111;
        public override int MessageID { get { return ProtocolId; } }

        public AuthenticationTicketAcceptedMessage() { }

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
