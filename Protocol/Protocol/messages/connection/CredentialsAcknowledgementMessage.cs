









// Generated on 12/11/2014 19:01:13
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class CredentialsAcknowledgementMessage : Message
    {
        public new const uint ID =6314;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public CredentialsAcknowledgementMessage()
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