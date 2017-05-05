









// Generated on 12/11/2014 19:01:45
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GuildModificationValidMessage : Message
    {
        public new const uint ID =6323;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public string guildName;
        public Types.GuildEmblem guildEmblem;
        
        public GuildModificationValidMessage()
        {
        }
        
        public GuildModificationValidMessage(string guildName, Types.GuildEmblem guildEmblem)
        {
            this.guildName = guildName;
            this.guildEmblem = guildEmblem;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUTF(guildName);
            guildEmblem.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            guildName = reader.ReadUTF();
            guildEmblem = new Types.GuildEmblem();
            guildEmblem.Deserialize(reader);
        }
        
    }
    
}