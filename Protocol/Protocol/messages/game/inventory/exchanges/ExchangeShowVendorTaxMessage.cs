









// Generated on 12/11/2014 19:01:52
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeShowVendorTaxMessage : Message
    {
        public new const uint ID =5783;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public ExchangeShowVendorTaxMessage()
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