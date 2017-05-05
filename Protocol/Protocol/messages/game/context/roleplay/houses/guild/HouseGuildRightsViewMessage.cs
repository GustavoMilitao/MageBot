









// Generated on 12/11/2014 19:01:34
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class HouseGuildRightsViewMessage : Message
    {
        public new const uint ID =5700;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public HouseGuildRightsViewMessage()
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