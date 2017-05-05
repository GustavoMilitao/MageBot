









// Generated on 12/11/2014 19:01:44
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GuildInvitedMessage : Message
    {
        public new const uint ID =5552;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int recruterId;
        public string recruterName;
        public Types.BasicGuildInformations guildInfo;
        
        public GuildInvitedMessage()
        {
        }
        
        public GuildInvitedMessage(int recruterId, string recruterName, Types.BasicGuildInformations guildInfo)
        {
            this.recruterId = recruterId;
            this.recruterName = recruterName;
            this.guildInfo = guildInfo;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarInt(recruterId);
            writer.WriteUTF(recruterName);
            guildInfo.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            recruterId = reader.ReadVarInt();
            if (recruterId < 0)
                throw new Exception("Forbidden value on recruterId = " + recruterId + ", it doesn't respect the following condition : recruterId < 0");
            recruterName = reader.ReadUTF();
            guildInfo = new Types.BasicGuildInformations();
            guildInfo.Deserialize(reader);
        }
        
    }
    
}