









// Generated on 12/11/2014 19:01:19
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameActionFightTriggerEffectMessage : GameActionFightDispellEffectMessage
    {
        public new const uint ID =6147;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public GameActionFightTriggerEffectMessage()
        {
        }
        
        public GameActionFightTriggerEffectMessage(short actionId, int sourceId, int targetId, int boostUID)
         : base(actionId, sourceId, targetId, boostUID)
        {
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
        }
        
    }
    
}