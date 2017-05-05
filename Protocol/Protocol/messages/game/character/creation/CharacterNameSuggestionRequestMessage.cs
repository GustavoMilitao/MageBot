









// Generated on 12/11/2014 19:01:23
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class CharacterNameSuggestionRequestMessage : Message
    {
        public new const uint ID =162;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public CharacterNameSuggestionRequestMessage()
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