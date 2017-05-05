









// Generated on 12/11/2014 19:01:58
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PrismModuleExchangeRequestMessage : Message
    {
        public new const uint ID =6531;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public PrismModuleExchangeRequestMessage()
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