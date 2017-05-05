









// Generated on 12/11/2014 19:01:42
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class FriendsGetListMessage : Message
    {
        public new const uint ID =4001;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public FriendsGetListMessage()
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