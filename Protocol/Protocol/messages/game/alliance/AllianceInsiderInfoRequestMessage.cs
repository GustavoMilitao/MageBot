









// Generated on 12/11/2014 19:01:20
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class AllianceInsiderInfoRequestMessage : Message
    {
        public new const uint ID =6417;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public AllianceInsiderInfoRequestMessage()
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