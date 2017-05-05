









// Generated on 12/11/2014 19:01:56
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class WrapperObjectErrorMessage : SymbioticObjectErrorMessage
    {
        public new const uint ID =6529;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public WrapperObjectErrorMessage()
        {
        }
        
        public WrapperObjectErrorMessage(sbyte reason, sbyte errorCode)
         : base(reason, errorCode)
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