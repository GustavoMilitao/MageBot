









// Generated on 12/11/2014 19:01:54
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ObjectAddedMessage : Message
    {
        public new const uint ID =3025;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.ObjectItem @object;
        
        public ObjectAddedMessage()
        {
        }
        
        public ObjectAddedMessage(Types.ObjectItem @object)
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