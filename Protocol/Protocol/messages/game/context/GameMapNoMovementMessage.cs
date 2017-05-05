









// Generated on 12/11/2014 19:01:27
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameMapNoMovementMessage : Message
    {
        public new const uint ID =954;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public GameMapNoMovementMessage()
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