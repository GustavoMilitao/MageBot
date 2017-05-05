









// Generated on 12/11/2014 19:01:47
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class StatedElementUpdatedMessage : Message
    {
        public new const uint ID =5709;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.StatedElement statedElement;
        
        public StatedElementUpdatedMessage()
        {
        }
        
        public StatedElementUpdatedMessage(Types.StatedElement statedElement)
        {
            this.statedElement = statedElement;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            statedElement.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            statedElement = new Types.StatedElement();
            statedElement.Deserialize(reader);
        }
        
    }
    
}