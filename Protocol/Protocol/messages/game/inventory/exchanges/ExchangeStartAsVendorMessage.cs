









// Generated on 12/11/2014 19:01:52
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeStartAsVendorMessage : Message
    {
        public new const uint ID =5775;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public ExchangeStartAsVendorMessage()
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