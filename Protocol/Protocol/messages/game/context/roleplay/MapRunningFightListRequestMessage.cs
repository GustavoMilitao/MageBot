









// Generated on 12/11/2014 19:01:31
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class MapRunningFightListRequestMessage : Message
    {
        public new const uint ID =5742;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public MapRunningFightListRequestMessage()
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