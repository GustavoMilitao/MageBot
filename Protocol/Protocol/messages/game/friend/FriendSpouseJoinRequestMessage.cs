









// Generated on 12/11/2014 19:01:42
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class FriendSpouseJoinRequestMessage : Message
    {
        public new const uint ID =5604;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public FriendSpouseJoinRequestMessage()
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