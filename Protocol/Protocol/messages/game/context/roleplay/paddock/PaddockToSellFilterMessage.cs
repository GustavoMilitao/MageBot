









// Generated on 12/11/2014 19:01:36
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PaddockToSellFilterMessage : Message
    {
        public new const uint ID =6161;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int areaId;
        public sbyte atLeastNbMount;
        public sbyte atLeastNbMachine;
        public int maxPrice;
        
        public PaddockToSellFilterMessage()
        {
        }
        
        public PaddockToSellFilterMessage(int areaId, sbyte atLeastNbMount, sbyte atLeastNbMachine, int maxPrice)
        {
            this.areaId = areaId;
            this.atLeastNbMount = atLeastNbMount;
            this.atLeastNbMachine = atLeastNbMachine;
            this.maxPrice = maxPrice;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(areaId);
            writer.WriteSByte(atLeastNbMount);
            writer.WriteSByte(atLeastNbMachine);
            writer.WriteVarInt(maxPrice);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            areaId = reader.ReadInt();
            atLeastNbMount = reader.ReadSByte();
            atLeastNbMachine = reader.ReadSByte();
            maxPrice = reader.ReadVarInt();
            if (maxPrice < 0)
                throw new Exception("Forbidden value on maxPrice = " + maxPrice + ", it doesn't respect the following condition : maxPrice < 0");
        }
        
    }
    
}