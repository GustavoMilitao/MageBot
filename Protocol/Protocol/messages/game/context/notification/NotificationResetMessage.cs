









// Generated on 12/11/2014 19:01:31
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class NotificationResetMessage : Message
    {
        public new const uint ID =6089;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public NotificationResetMessage()
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