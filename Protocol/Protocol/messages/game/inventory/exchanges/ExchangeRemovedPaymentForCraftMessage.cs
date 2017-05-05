









// Generated on 12/11/2014 19:01:51
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeRemovedPaymentForCraftMessage : Message
    {
        public new const uint ID =6031;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool onlySuccess;
        public int objectUID;
        
        public ExchangeRemovedPaymentForCraftMessage()
        {
        }
        
        public ExchangeRemovedPaymentForCraftMessage(bool onlySuccess, int objectUID)
        {
            this.onlySuccess = onlySuccess;
            this.objectUID = objectUID;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteBoolean(onlySuccess);
            writer.WriteVarInt(objectUID);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            onlySuccess = reader.ReadBoolean();
            objectUID = reader.ReadVarInt();
            if (objectUID < 0)
                throw new Exception("Forbidden value on objectUID = " + objectUID + ", it doesn't respect the following condition : objectUID < 0");
        }
        
    }
    
}