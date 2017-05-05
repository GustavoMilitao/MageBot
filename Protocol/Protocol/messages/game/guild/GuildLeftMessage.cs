









// Generated on 12/11/2014 19:01:45
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GuildLeftMessage : Message
    {
        public new const uint ID =5562;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public GuildLeftMessage()
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