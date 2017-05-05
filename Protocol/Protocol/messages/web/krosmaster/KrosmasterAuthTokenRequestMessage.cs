









// Generated on 12/11/2014 19:02:01
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class KrosmasterAuthTokenRequestMessage : Message
    {
        public new const uint ID =6346;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public KrosmasterAuthTokenRequestMessage()
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