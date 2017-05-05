









// Generated on 12/11/2014 19:01:29
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameFightRefreshFighterMessage : Message
    {
        public new const uint ID =6309;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.GameContextActorInformations informations;
        
        public GameFightRefreshFighterMessage()
        {
        }
        
        public GameFightRefreshFighterMessage(Types.GameContextActorInformations informations)
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
            informations = Types.ProtocolTypeManager.GetInstance<Types.GameContextActorInformations>(reader.ReadShort());
            informations.Deserialize(reader);
        }
        
    }
    
}