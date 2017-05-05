


















// Generated on 12/11/2014 19:02:06
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class BasicGuildInformations : AbstractSocialGroupInfos
    {

        public new const int ID = 365;
        public override int TypeId
        {
            get { return ID; }
        }

        public uint guildId;
        public string guildName;
        public byte guildLevel { get; set; }


        public BasicGuildInformations()
        {
        }

        public BasicGuildInformations(uint guildId, string guildName, byte guildLevel)
        {
            this.guildId = guildId;
            this.guildName = guildName;
            this.guildLevel = guildLevel;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteVarInt(guildId);
            writer.WriteUTF(guildName);
            writer.WriteByte(guildLevel);

        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            guildId = reader.ReadVarUhInt();
            if (guildId < 0)
                throw new Exception("Forbidden value on guildId = " + guildId + ", it doesn't respect the following condition : guildId < 0");
            guildName = reader.ReadUTF();
            guildLevel = reader.ReadByte();
        }


    }


}