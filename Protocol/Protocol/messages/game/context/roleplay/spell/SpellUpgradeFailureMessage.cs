









// Generated on 12/11/2014 19:01:41
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class SpellUpgradeFailureMessage : Message
    {
        public new const uint ID =1202;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public SpellUpgradeFailureMessage()
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