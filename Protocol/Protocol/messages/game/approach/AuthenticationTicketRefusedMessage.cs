









// Generated on 12/11/2014 19:01:21
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class AuthenticationTicketRefusedMessage : Message
    {
        public new const uint ID =112;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public AuthenticationTicketRefusedMessage()
        {
        }
        
        
        public override void Serialize(BigEndianWriter writer)
        {
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
        }
        
    }
    
}