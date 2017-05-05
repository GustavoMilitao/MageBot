









// Generated on 12/11/2014 19:01:50
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeObjectTransfertExistingToInvMessage : Message
    {
        public new const uint ID =6326;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public ExchangeObjectTransfertExistingToInvMessage()
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