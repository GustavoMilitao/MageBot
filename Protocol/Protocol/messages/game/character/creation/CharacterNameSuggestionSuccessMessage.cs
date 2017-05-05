









// Generated on 12/11/2014 19:01:23
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class CharacterNameSuggestionSuccessMessage : Message
    {
        public new const uint ID =5544;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public string suggestion;
        
        public CharacterNameSuggestionSuccessMessage()
        {
        }
        
        public CharacterNameSuggestionSuccessMessage(string suggestion)
        {
            this.suggestion = suggestion;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUTF(suggestion);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            suggestion = reader.ReadUTF();
        }
        
    }
    
}