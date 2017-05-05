









// Generated on 12/11/2014 19:01:22
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class SequenceNumberRequestMessage : Message
    {
        public new const uint ID =6316;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public SequenceNumberRequestMessage()
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