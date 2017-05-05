









// Generated on 12/11/2014 19:01:57
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PrismInfoCloseMessage : Message
    {
        public new const uint ID =5853;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public PrismInfoCloseMessage()
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