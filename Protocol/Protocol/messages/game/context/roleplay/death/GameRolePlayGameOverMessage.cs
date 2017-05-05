









// Generated on 12/11/2014 19:01:32
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameRolePlayGameOverMessage : Message
    {
        public new const uint ID =746;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public GameRolePlayGameOverMessage()
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