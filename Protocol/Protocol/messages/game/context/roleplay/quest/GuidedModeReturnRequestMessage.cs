









// Generated on 12/11/2014 19:01:40
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GuidedModeReturnRequestMessage : Message
    {
        public new const uint ID =6088;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public GuidedModeReturnRequestMessage()
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