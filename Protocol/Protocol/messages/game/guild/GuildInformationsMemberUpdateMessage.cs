









// Generated on 12/11/2014 19:01:44
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GuildInformationsMemberUpdateMessage : Message
    {
        public new const uint ID =5597;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.GuildMember member;
        
        public GuildInformationsMemberUpdateMessage()
        {
        }
        
        public GuildInformationsMemberUpdateMessage(Types.GuildMember member)
        {
            this.member = member;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            member.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            member = new Types.GuildMember();
            member.Deserialize(reader);
        }
        
    }
    
}