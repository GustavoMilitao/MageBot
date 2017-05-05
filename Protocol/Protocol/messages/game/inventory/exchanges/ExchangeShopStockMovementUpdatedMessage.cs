









// Generated on 12/11/2014 19:01:52
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeShopStockMovementUpdatedMessage : Message
    {
        public new const uint ID =5909;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.ObjectItemToSell objectInfo;
        
        public ExchangeShopStockMovementUpdatedMessage()
        {
        }
        
        public ExchangeShopStockMovementUpdatedMessage(Types.ObjectItemToSell objectInfo)
        {
            this.objectInfo = objectInfo;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            objectInfo.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            objectInfo = new Types.ObjectItemToSell();
            objectInfo.Deserialize(reader);
        }
        
    }
    
}