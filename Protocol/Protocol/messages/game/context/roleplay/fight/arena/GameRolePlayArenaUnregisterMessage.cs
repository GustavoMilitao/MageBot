









// Generated on 12/11/2014 19:01:33
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameRolePlayArenaUnregisterMessage : Message
    {
        public new const uint ID =6282;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public GameRolePlayArenaUnregisterMessage()
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