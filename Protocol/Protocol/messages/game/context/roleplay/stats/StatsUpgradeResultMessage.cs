









// Generated on 12/11/2014 19:01:41
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class StatsUpgradeResultMessage : Message
    {
        public new const uint ID =5609;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte result;
        public short nbCharacBoost;
        
        public StatsUpgradeResultMessage()
        {
        }
        
        public StatsUpgradeResultMessage(sbyte result, short nbCharacBoost)
        {
            this.result = result;
            this.nbCharacBoost = nbCharacBoost;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(result);
            writer.WriteVarShort(nbCharacBoost);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            result = reader.ReadSByte();
            nbCharacBoost = reader.ReadVarShort();
            if (nbCharacBoost < 0)
                throw new Exception("Forbidden value on nbCharacBoost = " + nbCharacBoost + ", it doesn't respect the following condition : nbCharacBoost < 0");
        }
        
    }
    
}