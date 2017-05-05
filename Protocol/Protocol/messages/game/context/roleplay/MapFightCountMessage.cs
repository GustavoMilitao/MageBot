









// Generated on 12/11/2014 19:01:31
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class MapFightCountMessage : Message
    {
        public new const uint ID =210;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public short fightCount;
        
        public MapFightCountMessage()
        {
        }
        
        public MapFightCountMessage(short fightCount)
        {
            this.fightCount = fightCount;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort(fightCount);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            fightCount = reader.ReadVarShort();
            if (fightCount < 0)
                throw new Exception("Forbidden value on fightCount = " + fightCount + ", it doesn't respect the following condition : fightCount < 0");
        }
        
    }
    
}