 

namespace BlueSheep.Protocol.Messages.Game.Approach
{
    public class AuthenticationTicketRefusedMessage : Message
    {
        protected override int ProtocolId { get; set; } = 112;
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
