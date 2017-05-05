


















// Generated on 12/11/2014 19:02:12
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class AlliancedGuildFactSheetInformations : GuildInformations
    {

        public new const int ID = 422;
        public override int TypeId
        {
            get { return ID; }
        }

        public Types.BasicNamedAllianceInformations allianceInfos;


        public AlliancedGuildFactSheetInformations()
        {
        }

        public AlliancedGuildFactSheetInformations(uint guildId, string guildName, byte guildLevel, Types.GuildEmblem guildEmblem, Types.BasicNamedAllianceInformations allianceInfos)
                 : base(guildId, guildName, guildLevel, guildEmblem)
        {
            this.allianceInfos = allianceInfos;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            allianceInfos.Serialize(writer);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            allianceInfos = new Types.BasicNamedAllianceInformations();
            allianceInfos.Deserialize(reader);


        }


    }


}