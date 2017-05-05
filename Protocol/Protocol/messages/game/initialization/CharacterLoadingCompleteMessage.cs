









// Generated on 12/11/2014 19:01:46
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class CharacterLoadingCompleteMessage : Message
    {
        public new const uint ID =6471;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public CharacterLoadingCompleteMessage()
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