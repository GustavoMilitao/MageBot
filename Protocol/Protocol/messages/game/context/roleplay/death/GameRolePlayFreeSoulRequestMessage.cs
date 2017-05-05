









// Generated on 12/11/2014 19:01:32
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameRolePlayFreeSoulRequestMessage : Message
    {
        public new const uint ID =745;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public GameRolePlayFreeSoulRequestMessage()
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