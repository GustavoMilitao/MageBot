









// Generated on 12/11/2014 19:01:43
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GuildCharacsUpgradeRequestMessage : Message
    {
        public new const uint ID =5706;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte charaTypeTarget;
        
        public GuildCharacsUpgradeRequestMessage()
        {
        }
        
        public GuildCharacsUpgradeRequestMessage(sbyte charaTypeTarget)
        {
            this.charaTypeTarget = charaTypeTarget;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(charaTypeTarget);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            charaTypeTarget = reader.ReadSByte();
            if (charaTypeTarget < 0)
                throw new Exception("Forbidden value on charaTypeTarget = " + charaTypeTarget + ", it doesn't respect the following condition : charaTypeTarget < 0");
        }
        
    }
    
}