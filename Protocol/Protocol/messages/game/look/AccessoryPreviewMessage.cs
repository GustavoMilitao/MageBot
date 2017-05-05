









// Generated on 12/11/2014 19:01:57
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class AccessoryPreviewMessage : Message
    {
        public new const uint ID =6517;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.EntityLook look;
        
        public AccessoryPreviewMessage()
        {
        }
        
        public AccessoryPreviewMessage(Types.EntityLook look)
        {
            this.look = look;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            look.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            look = new Types.EntityLook();
            look.Deserialize(reader);
        }
        
    }
    
}