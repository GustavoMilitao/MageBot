









// Generated on 12/11/2014 19:01:21
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class AuthenticationTicketMessage : Message
    {
        public new const uint ID =110;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public string lang;
        public string ticket;
        
        public AuthenticationTicketMessage()
        {
        }
        
        public AuthenticationTicketMessage(string lang, string ticket)
        {
            this.lang = lang;
            this.ticket = ticket;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUTF(lang);
            writer.WriteUTF(ticket);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            lang = reader.ReadUTF();
            ticket = reader.ReadUTF();
        }
        
    }
    
}