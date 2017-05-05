


















// Generated on 12/11/2014 19:02:12
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GuildFactSheetInformations : GuildInformations
    {

        public new const int ID = 424;
        public override int TypeId
        {
            get { return ID; }
        }

        public int leaderId;
        public int nbMembers;


        public GuildFactSheetInformations()
        {
        }

        public GuildFactSheetInformations(uint guildId, string guildName, byte guildLevel, Types.GuildEmblem guildEmblem, int leaderId, int nbMembers)
                 : base(guildId, guildName, guildLevel, guildEmblem)
        {
            this.leaderId = leaderId;
            this.nbMembers = nbMembers;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteVarInt(leaderId);
            writer.WriteByte(guildLevel);
            writer.WriteVarShort((short)nbMembers);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            leaderId = reader.ReadVarInt();
            if (leaderId < 0)
                throw new Exception("Forbidden value on leaderId = " + leaderId + ", it doesn't respect the following condition : leaderId < 0");
            guildLevel = reader.ReadByte();
            if (guildLevel < 0 || guildLevel > 255)
                throw new Exception("Forbidden value on guildLevel = " + guildLevel + ", it doesn't respect the following condition : guildLevel < 0 || guildLevel > 255");
            nbMembers = reader.ReadVarUhShort();
            if (nbMembers < 0)
                throw new Exception("Forbidden value on nbMembers = " + nbMembers + ", it doesn't respect the following condition : nbMembers < 0");


        }


    }


}