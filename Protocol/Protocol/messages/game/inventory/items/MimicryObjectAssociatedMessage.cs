









// Generated on 12/11/2014 19:01:54
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class MimicryObjectAssociatedMessage : SymbioticObjectAssociatedMessage
    {
        public new const uint ID =6462;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public MimicryObjectAssociatedMessage()
        {
        }
        
        public MimicryObjectAssociatedMessage(int hostUID)
         : base(hostUID)
        {
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
        }
        
    }
    
}