









// Generated on 12/11/2014 19:01:58
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PrismSetSabotagedRequestMessage : Message
    {
        public new const uint ID =6468;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public short subAreaId;
        
        public PrismSetSabotagedRequestMessage()
        {
        }
        
        public PrismSetSabotagedRequestMessage(short subAreaId)
        {
            this.subAreaId = subAreaId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort(subAreaId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            subAreaId = reader.ReadVarShort();
            if (subAreaId < 0)
                throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
        }
        
    }
    
}