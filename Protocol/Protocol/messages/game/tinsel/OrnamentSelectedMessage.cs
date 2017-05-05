









// Generated on 12/11/2014 19:01:59
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class OrnamentSelectedMessage : Message
    {
        public new const uint ID =6369;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public short ornamentId;
        
        public OrnamentSelectedMessage()
        {
        }
        
        public OrnamentSelectedMessage(short ornamentId)
        {
            this.ornamentId = ornamentId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort(ornamentId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            ornamentId = reader.ReadVarShort();
            if (ornamentId < 0)
                throw new Exception("Forbidden value on ornamentId = " + ornamentId + ", it doesn't respect the following condition : ornamentId < 0");
        }
        
    }
    
}