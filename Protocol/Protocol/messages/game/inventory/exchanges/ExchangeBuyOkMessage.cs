









// Generated on 12/11/2014 19:01:48
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeBuyOkMessage : Message
    {
        public new const uint ID =5759;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public ExchangeBuyOkMessage()
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