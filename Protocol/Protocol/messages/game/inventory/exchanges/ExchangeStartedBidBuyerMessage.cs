









// Generated on 12/11/2014 19:01:52
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeStartedBidBuyerMessage : Message
    {
        public new const uint ID =5904;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.SellerBuyerDescriptor buyerDescriptor;
        
        public ExchangeStartedBidBuyerMessage()
        {
        }
        
        public ExchangeStartedBidBuyerMessage(Types.SellerBuyerDescriptor buyerDescriptor)
        {
            this.buyerDescriptor = buyerDescriptor;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            buyerDescriptor.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            buyerDescriptor = new Types.SellerBuyerDescriptor();
            buyerDescriptor.Deserialize(reader);
        }
        
    }
    
}