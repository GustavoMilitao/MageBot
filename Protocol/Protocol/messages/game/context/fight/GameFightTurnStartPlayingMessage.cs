









// Generated on 12/11/2014 19:01:29
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameFightTurnStartPlayingMessage : Message
    {
        public new const uint ID =6465;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public GameFightTurnStartPlayingMessage()
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