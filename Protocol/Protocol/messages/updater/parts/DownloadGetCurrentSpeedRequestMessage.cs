









// Generated on 12/11/2014 19:02:01
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class DownloadGetCurrentSpeedRequestMessage : Message
    {
        public new const uint ID =1510;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public DownloadGetCurrentSpeedRequestMessage()
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