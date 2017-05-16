 

namespace BlueSheep.Protocol.Messages.Game.Approach
{
    public class AuthenticationTicketMessage : Message
    {
        protected override int ProtocolId { get; set; } = 110;
        public override int MessageID { get { return ProtocolId; } }

        public string Lang { get; set; }
        public string Ticket { get; set; }

        public AuthenticationTicketMessage() { }

        public AuthenticationTicketMessage(string lang, string ticket)
        {
            Lang = lang;
            Ticket = ticket;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Lang);
            writer.WriteUTF(Ticket);
        }

        public override void Deserialize(IDataReader reader)
        {
            Lang = reader.ReadUTF();
            Ticket = reader.ReadUTF();
        }
    }
}
