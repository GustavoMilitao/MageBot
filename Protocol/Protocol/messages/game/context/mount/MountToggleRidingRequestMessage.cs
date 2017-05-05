









// Generated on 12/11/2014 19:01:30
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class MountToggleRidingRequestMessage : Message
    {
        public new const uint ID =5976;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public MountToggleRidingRequestMessage()
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