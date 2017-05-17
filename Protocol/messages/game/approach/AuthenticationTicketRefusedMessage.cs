 

namespace MageBot.Protocol.Messages.Game.Approach
{
    public class AuthenticationTicketRefusedMessage : Message
    {
        public override int ProtocolId { get; } = 112;
        public override int MessageID { get { return ProtocolId; } }

        public AuthenticationTicketRefusedMessage() { }

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
