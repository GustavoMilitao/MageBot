









// Generated on 12/11/2014 19:01:44
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GuildInvitationByNameMessage : Message
    {
        public new const uint ID =6115;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public string name;
        
        public GuildInvitationByNameMessage()
        {
        }
        
        public GuildInvitationByNameMessage(string name)
        {
            this.name = name;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUTF(name);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            name = reader.ReadUTF();
        }
        
    }
    
}