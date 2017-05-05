









// Generated on 12/11/2014 19:01:57
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class StorageObjectUpdateMessage : Message
    {
        public new const uint ID =5647;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.ObjectItem @object;
        
        public StorageObjectUpdateMessage()
        {
        }
        
        public StorageObjectUpdateMessage(Types.ObjectItem @object)
        {
            this.@object = @object;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            @object.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            @object = new Types.ObjectItem();
            @object.Deserialize(reader);
        }
        
    }
    
}