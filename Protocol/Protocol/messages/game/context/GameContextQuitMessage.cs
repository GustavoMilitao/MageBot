









// Generated on 12/11/2014 19:01:26
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameContextQuitMessage : Message
    {
        public new const uint ID =255;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public GameContextQuitMessage()
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