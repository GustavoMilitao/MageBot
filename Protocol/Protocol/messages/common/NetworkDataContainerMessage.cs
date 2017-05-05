









// Generated on 12/11/2014 19:01:13
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class NetworkDataContainerMessage : Message
    {
        public new const uint ID =2;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public NetworkDataContainerMessage()
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