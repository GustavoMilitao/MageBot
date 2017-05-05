









// Generated on 12/11/2014 19:01:48
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeBidHouseItemAddOkMessage : Message
    {
        public new const uint ID =5945;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.ObjectItemToSellInBid itemInfo;
        
        public ExchangeBidHouseItemAddOkMessage()
        {
        }
        
        public ExchangeBidHouseItemAddOkMessage(Types.ObjectItemToSellInBid itemInfo)
        {
            this.itemInfo = itemInfo;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            itemInfo.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            itemInfo = new Types.ObjectItemToSellInBid();
            itemInfo.Deserialize(reader);
        }
        
    }
    
}