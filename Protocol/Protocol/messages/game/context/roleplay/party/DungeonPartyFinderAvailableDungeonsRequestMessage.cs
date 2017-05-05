









// Generated on 12/11/2014 19:01:36
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class DungeonPartyFinderAvailableDungeonsRequestMessage : Message
    {
        public new const uint ID =6240;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public DungeonPartyFinderAvailableDungeonsRequestMessage()
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