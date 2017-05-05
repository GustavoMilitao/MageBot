









// Generated on 12/11/2014 19:01:31
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameRolePlayShowActorMessage : Message
    {
        public new const uint ID =5632;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.GameRolePlayActorInformations informations;
        
        public GameRolePlayShowActorMessage()
        {
        }
        
        public GameRolePlayShowActorMessage(Types.GameRolePlayActorInformations informations)
        {
            this.informations = informations;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteShort(informations.TypeId);
            informations.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            informations = Types.ProtocolTypeManager.GetInstance<Types.GameRolePlayActorInformations>(reader.ReadShort());
            informations.Deserialize(reader);
        }
        
    }
    
}