









// Generated on 12/11/2014 19:01:57
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class StorageObjectRemoveMessage : Message
    {
        public new const uint ID =5648;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int objectUID;
        
        public StorageObjectRemoveMessage()
        {
        }
        
        public StorageObjectRemoveMessage(int objectUID)
        {
            this.objectUID = objectUID;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarInt(objectUID);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            objectUID = reader.ReadVarInt();
            if (objectUID < 0)
                throw new Exception("Forbidden value on objectUID = " + objectUID + ", it doesn't respect the following condition : objectUID < 0");
        }
        
    }
    
}