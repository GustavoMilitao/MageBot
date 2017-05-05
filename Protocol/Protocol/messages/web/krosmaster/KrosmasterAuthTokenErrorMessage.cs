









// Generated on 12/11/2014 19:02:01
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class KrosmasterAuthTokenErrorMessage : Message
    {
        public new const uint ID =6345;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte reason;
        
        public KrosmasterAuthTokenErrorMessage()
        {
        }
        
        public KrosmasterAuthTokenErrorMessage(sbyte reason)
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
            if (reason < 0)
                throw new Exception("Forbidden value on reason = " + reason + ", it doesn't respect the following condition : reason < 0");
        }
        
    }
    
}