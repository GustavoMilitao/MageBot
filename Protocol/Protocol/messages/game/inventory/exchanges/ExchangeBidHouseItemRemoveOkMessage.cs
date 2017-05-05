









// Generated on 12/11/2014 19:01:48
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeBidHouseItemRemoveOkMessage : Message
    {
        public new const uint ID =5946;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int sellerId;
        
        public ExchangeBidHouseItemRemoveOkMessage()
        {
        }
        
        public ExchangeBidHouseItemRemoveOkMessage(int sellerId)
        {
            this.sellerId = sellerId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(sellerId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            sellerId = reader.ReadInt();
        }
        
    }
    
}