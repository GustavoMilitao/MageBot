









// Generated on 12/11/2014 19:01:56
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class WrapperObjectAssociatedMessage : SymbioticObjectAssociatedMessage
    {
        public new const uint ID =6523;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public WrapperObjectAssociatedMessage()
        {
        }
        
        public WrapperObjectAssociatedMessage(int hostUID)
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