









// Generated on 12/11/2014 19:01:51
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeRequestOnShopStockMessage : Message
    {
        public new const uint ID =5753;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public ExchangeRequestOnShopStockMessage()
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