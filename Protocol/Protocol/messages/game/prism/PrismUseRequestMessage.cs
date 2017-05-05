









// Generated on 12/11/2014 19:01:58
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PrismUseRequestMessage : Message
    {
        public new const uint ID =6041;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public PrismUseRequestMessage()
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