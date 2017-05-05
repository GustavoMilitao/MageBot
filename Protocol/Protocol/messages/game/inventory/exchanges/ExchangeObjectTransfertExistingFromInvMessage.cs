









// Generated on 12/11/2014 19:01:50
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeObjectTransfertExistingFromInvMessage : Message
    {
        public new const uint ID =6325;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public ExchangeObjectTransfertExistingFromInvMessage()
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