









// Generated on 12/11/2014 19:01:33
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameRolePlayRemoveChallengeMessage : Message
    {
        public new const uint ID =300;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int fightId;
        
        public GameRolePlayRemoveChallengeMessage()
        {
        }
        
        public GameRolePlayRemoveChallengeMessage(int fightId)
        {
            this.fightId = fightId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(fightId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            fightId = reader.ReadInt();
        }
        
    }
    
}