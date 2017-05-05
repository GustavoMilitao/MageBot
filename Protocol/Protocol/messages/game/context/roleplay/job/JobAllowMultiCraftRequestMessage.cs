









// Generated on 12/11/2014 19:01:34
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class JobAllowMultiCraftRequestMessage : Message
    {
        public new const uint ID =5748;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool enabled;
        
        public JobAllowMultiCraftRequestMessage()
        {
        }
        
        public JobAllowMultiCraftRequestMessage(bool enabled)
        {
            this.enabled = enabled;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteBoolean(enabled);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            enabled = reader.ReadBoolean();
        }
        
    }
    
}