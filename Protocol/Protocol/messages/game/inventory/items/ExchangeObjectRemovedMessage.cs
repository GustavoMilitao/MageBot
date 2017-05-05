









// Generated on 12/11/2014 19:01:53
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeObjectRemovedMessage : ExchangeObjectMessage
    {
        public new const uint ID =5517;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int objectUID;
        
        public ExchangeObjectRemovedMessage()
        {
        }
        
        public ExchangeObjectRemovedMessage(bool remote, int objectUID)
         : base(remote)
        {
            this.objectUID = objectUID;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt(objectUID);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            objectUID = reader.ReadVarInt();
            if (objectUID < 0)
                throw new Exception("Forbidden value on objectUID = " + objectUID + ", it doesn't respect the following condition : objectUID < 0");
        }
        
    }
    
}