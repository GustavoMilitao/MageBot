









// Generated on 12/11/2014 19:01:58
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PrismSettingsErrorMessage : Message
    {
        public new const uint ID =6442;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public PrismSettingsErrorMessage()
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