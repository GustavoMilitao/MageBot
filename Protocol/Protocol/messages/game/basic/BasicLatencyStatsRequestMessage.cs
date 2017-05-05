









// Generated on 12/11/2014 19:01:22
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class BasicLatencyStatsRequestMessage : Message
    {
        public new const uint ID =5816;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public BasicLatencyStatsRequestMessage()
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