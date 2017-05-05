









// Generated on 12/11/2014 19:01:43
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class IgnoredGetListMessage : Message
    {
        public new const uint ID =5676;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public IgnoredGetListMessage()
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