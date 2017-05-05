









// Generated on 12/11/2014 19:01:42
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class TreasureHuntLegendaryRequestMessage : Message
    {
        public new const uint ID =6499;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public short legendaryId;
        
        public TreasureHuntLegendaryRequestMessage()
        {
        }
        
        public TreasureHuntLegendaryRequestMessage(short legendaryId)
        {
            this.legendaryId = legendaryId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort(legendaryId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            legendaryId = reader.ReadVarShort();
            if (legendaryId < 0)
                throw new Exception("Forbidden value on legendaryId = " + legendaryId + ", it doesn't respect the following condition : legendaryId < 0");
        }
        
    }
    
}