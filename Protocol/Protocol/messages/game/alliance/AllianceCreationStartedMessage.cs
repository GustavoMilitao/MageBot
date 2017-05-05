









// Generated on 12/11/2014 19:01:19
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class AllianceCreationStartedMessage : Message
    {
        public new const uint ID =6394;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public AllianceCreationStartedMessage()
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