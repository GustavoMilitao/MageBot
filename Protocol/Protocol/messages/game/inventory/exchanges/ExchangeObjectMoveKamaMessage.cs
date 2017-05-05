









// Generated on 12/11/2014 19:01:50
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeObjectMoveKamaMessage : Message
    {
        public new const uint ID =5520;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int quantity;
        
        public ExchangeObjectMoveKamaMessage()
        {
        }
        
        public ExchangeObjectMoveKamaMessage(int quantity)
        {
            this.quantity = quantity;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarInt(quantity);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            quantity = reader.ReadVarInt();
        }
        
    }
    
}