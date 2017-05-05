









// Generated on 12/11/2014 19:01:52
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeShopStockMovementRemovedMessage : Message
    {
        public new const uint ID =5907;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int objectId;
        
        public ExchangeShopStockMovementRemovedMessage()
        {
        }
        
        public ExchangeShopStockMovementRemovedMessage(int objectId)
        {
            this.objectId = objectId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarInt(objectId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            objectId = reader.ReadVarInt();
            if (objectId < 0)
                throw new Exception("Forbidden value on objectId = " + objectId + ", it doesn't respect the following condition : objectId < 0");
        }
        
    }
    
}