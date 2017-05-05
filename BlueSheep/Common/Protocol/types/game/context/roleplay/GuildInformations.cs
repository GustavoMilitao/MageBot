


















// Generated on 12/11/2014 19:02:07
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GuildInformations : BasicGuildInformations
    {

        public new const int ID = 127;
        public override int TypeId
        {
            get { return ID; }
        }

        public Types.GuildEmblem guildEmblem;


        public GuildInformations()
        {
        }

        public GuildInformations(uint guildId, string guildName, byte guildLevel, Types.GuildEmblem guildEmblem)
                 : base(guildId, guildName, guildLevel )
        {
            this.guildEmblem = guildEmblem;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            guildEmblem.Serialize(writer);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            guildEmblem = new Types.GuildEmblem();
            guildEmblem.Deserialize(reader);


        }


    }


}