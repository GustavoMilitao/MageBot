









// Generated on 12/11/2014 19:01:57
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PrismFightStateUpdateMessage : Message
    {
        public new const uint ID =6040;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte state;
        
        public PrismFightStateUpdateMessage()
        {
        }
        
        public PrismFightStateUpdateMessage(sbyte state)
        {
            this.state = state;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(state);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            state = reader.ReadSByte();
            if (state < 0)
                throw new Exception("Forbidden value on state = " + state + ", it doesn't respect the following condition : state < 0");
        }
        
    }
    
}