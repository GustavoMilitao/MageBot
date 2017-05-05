









// Generated on 12/11/2014 19:01:46
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class CharacterCapabilitiesMessage : Message
    {
        public new const uint ID =6339;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int guildEmblemSymbolCategories;
        
        public CharacterCapabilitiesMessage()
        {
        }
        
        public CharacterCapabilitiesMessage(int guildEmblemSymbolCategories)
        {
            this.guildEmblemSymbolCategories = guildEmblemSymbolCategories;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarInt(guildEmblemSymbolCategories);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            guildEmblemSymbolCategories = reader.ReadVarInt();
            if (guildEmblemSymbolCategories < 0)
                throw new Exception("Forbidden value on guildEmblemSymbolCategories = " + guildEmblemSymbolCategories + ", it doesn't respect the following condition : guildEmblemSymbolCategories < 0");
        }
        
    }
    
}