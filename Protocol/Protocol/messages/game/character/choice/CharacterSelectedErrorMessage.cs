









// Generated on 12/11/2014 19:01:23
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class CharacterSelectedErrorMessage : Message
    {
        public new const uint ID =5836;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public CharacterSelectedErrorMessage()
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