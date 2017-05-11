 

namespace BlueSheep.Protocol.Messages.Game.Approach
{
    public class AuthenticationTicketMessage : Message
    {
        public const int ProtocolId = 110;
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
