









// Generated on 12/11/2014 19:01:35
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class NpcGenericActionFailureMessage : Message
    {
        public new const uint ID =5900;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public NpcGenericActionFailureMessage()
        {
        }
        
        
        public override void Serialize(BigEndianWriter writer)
        {
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
        }
        
    }
    
}