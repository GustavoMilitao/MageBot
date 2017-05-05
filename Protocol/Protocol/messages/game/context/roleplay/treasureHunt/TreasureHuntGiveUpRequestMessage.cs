









// Generated on 12/11/2014 19:01:41
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class TreasureHuntGiveUpRequestMessage : Message
    {
        public new const uint ID =6487;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte questType;
        
        public TreasureHuntGiveUpRequestMessage()
        {
        }
        
        public TreasureHuntGiveUpRequestMessage(sbyte questType)
        {
            this.questType = questType;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(questType);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            questType = reader.ReadSByte();
            if (questType < 0)
                throw new Exception("Forbidden value on questType = " + questType + ", it doesn't respect the following condition : questType < 0");
        }
        
    }
    
}