









// Generated on 12/11/2014 19:01:42
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class TreasureHuntShowLegendaryUIMessage : Message
    {
        public new const uint ID =6498;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int[] availableLegendaryIds;
        
        public TreasureHuntShowLegendaryUIMessage()
        {
        }
        
        public TreasureHuntShowLegendaryUIMessage(int[] availableLegendaryIds)
        {
            this.availableLegendaryIds = availableLegendaryIds;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUShort((ushort)availableLegendaryIds.Length);
            foreach (var entry in availableLegendaryIds)
            {
                 writer.WriteVarShort((short)entry);
            }
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            var limit = reader.ReadUShort();
            availableLegendaryIds = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 availableLegendaryIds[i] = reader.ReadVarUhShort();
            }
        }
        
    }
    
}