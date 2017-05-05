









// Generated on 12/11/2014 19:01:14
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class NicknameRegistrationMessage : Message
    {
        public new const uint ID =5640;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public NicknameRegistrationMessage()
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