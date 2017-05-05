









// Generated on 12/11/2014 19:01:14
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class NicknameAcceptedMessage : Message
    {
        public new const uint ID =5641;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public NicknameAcceptedMessage()
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