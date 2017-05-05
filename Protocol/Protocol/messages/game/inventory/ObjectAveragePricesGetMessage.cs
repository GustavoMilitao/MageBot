









// Generated on 12/11/2014 19:01:47
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ObjectAveragePricesGetMessage : Message
    {
        public new const uint ID =6334;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public ObjectAveragePricesGetMessage()
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