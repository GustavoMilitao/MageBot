 

namespace BlueSheep.Common.Protocol.Messages.Game.Approach
{
    class AuthenticationTicketRefusedMessage : Message
    {
        public const int ProtocolId = 112;
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
