 

namespace MageBot.Protocol.Messages.Connection
{
    public class CredentialsAcknowledgementMessage : Message
    {
        protected override int ProtocolId { get; set; } = 6314;
        public override int MessageID { get { return ProtocolId; } }

        public CredentialsAcknowledgementMessage() { }

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