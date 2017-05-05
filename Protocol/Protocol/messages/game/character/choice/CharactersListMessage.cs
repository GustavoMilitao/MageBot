









// Generated on 12/11/2014 19:01:23
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class CharactersListMessage : BasicCharactersListMessage
    {
        public new const uint ID =151;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool hasStartupActions;
        
        public CharactersListMessage()
        {
        }
        
        public CharactersListMessage(Types.CharacterBaseInformations[] characters, bool hasStartupActions)
         : base(characters)
        {
            this.hasStartupActions = hasStartupActions;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(hasStartupActions);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            hasStartupActions = reader.ReadBoolean();
        }
        
    }
    
}