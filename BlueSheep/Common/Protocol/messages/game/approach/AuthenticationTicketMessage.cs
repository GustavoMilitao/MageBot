namespace BlueSheep.Common.Protocol.Messages.Game.Approach
{
    using BlueSheep.Engine.Types;

 	 public class AuthenticationTicketMessage : Message 
    {
        public new const int ID = 110;
        public override int MessageID { get { return ID; } }

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
