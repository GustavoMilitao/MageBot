









// Generated on 12/11/2014 19:01:55
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ObjectErrorMessage : Message
    {
        public new const uint ID =3004;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte reason;
        
        public ObjectErrorMessage()
        {
        }
        
        public ObjectErrorMessage(sbyte reason)
        {
            this.reason = reason;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(reason);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            reason = reader.ReadSByte();
        }
        
    }
    
}