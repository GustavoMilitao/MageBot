


















// Generated on 12/11/2014 19:02:11
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class AlliancePrismInformation : PrismInformation
    {

        public new const int ID = 427;
        public override int TypeId
        {
            get { return ID; }
        }

        public Types.AllianceInformations alliance;


        public AlliancePrismInformation()
        {
        }

        public AlliancePrismInformation(byte typeId, byte state, int nextVulnerabilityDate, int placementDate, uint rewardTokenCount, Types.AllianceInformations alliance)
                 : base(typeId, state, nextVulnerabilityDate, placementDate, rewardTokenCount)
        {
            this.alliance = alliance;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            alliance.Serialize(writer);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            alliance = new Types.AllianceInformations();
            alliance.Deserialize(reader);


        }


    }


}