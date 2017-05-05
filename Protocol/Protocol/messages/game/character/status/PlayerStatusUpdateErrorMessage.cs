









// Generated on 12/11/2014 19:01:24
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PlayerStatusUpdateErrorMessage : Message
    {
        public new const uint ID =6385;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public PlayerStatusUpdateErrorMessage()
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