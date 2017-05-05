









// Generated on 12/11/2014 19:02:00
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class TitlesAndOrnamentsListRequestMessage : Message
    {
        public new const uint ID =6363;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public TitlesAndOrnamentsListRequestMessage()
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