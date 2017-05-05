









// Generated on 12/11/2014 19:01:42
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class TreasureHuntLegendaryRequestMessage : Message
    {
        public new const uint ID =6499;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int legendaryId;
        
        public TreasureHuntLegendaryRequestMessage()
        {
        }
        
        public TreasureHuntLegendaryRequestMessage(int legendaryId)
        {
            this.legendaryId = legendaryId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)legendaryId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            legendaryId = reader.ReadVarUhShort();
            if (legendaryId < 0)
                throw new Exception("Forbidden value on legendaryId = " + legendaryId + ", it doesn't respect the following condition : legendaryId < 0");
        }
        
    }
    
}