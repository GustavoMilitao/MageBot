









// Generated on 12/11/2014 19:01:24
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class CharacterLevelUpMessage : Message
    {
        public new const uint ID =5670;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public byte newLevel;
        
        public CharacterLevelUpMessage()
        {
        }
        
        public CharacterLevelUpMessage(byte newLevel)
        {
            this.newLevel = newLevel;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteByte(newLevel);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            newLevel = reader.ReadByte();
            if (newLevel < 2 || newLevel > 200)
                throw new Exception("Forbidden value on newLevel = " + newLevel + ", it doesn't respect the following condition : newLevel < 2 || newLevel > 200");
        }
        
    }
    
}