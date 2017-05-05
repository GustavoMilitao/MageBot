









// Generated on 12/11/2014 19:01:51
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeReplayStopMessage : Message
    {
        public new const uint ID =6001;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public ExchangeReplayStopMessage()
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