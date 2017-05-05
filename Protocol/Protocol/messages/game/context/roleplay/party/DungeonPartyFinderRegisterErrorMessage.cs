









// Generated on 12/11/2014 19:01:36
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class DungeonPartyFinderRegisterErrorMessage : Message
    {
        public new const uint ID =6243;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public DungeonPartyFinderRegisterErrorMessage()
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