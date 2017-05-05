









// Generated on 12/11/2014 19:01:33
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameRolePlayShowChallengeMessage : Message
    {
        public new const uint ID =301;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.FightCommonInformations commonsInfos;
        
        public GameRolePlayShowChallengeMessage()
        {
        }
        
        public GameRolePlayShowChallengeMessage(Types.FightCommonInformations commonsInfos)
        {
            this.commonsInfos = commonsInfos;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            commonsInfos.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            commonsInfos = new Types.FightCommonInformations();
            commonsInfos.Deserialize(reader);
        }
        
    }
    
}