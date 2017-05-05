


















// Generated on 12/11/2014 19:02:07
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class HumanOptionAlliance : HumanOption
    {

        public new const int ID = 425;
        public override int TypeId
        {
            get { return ID; }
        }

        public Types.AllianceInformations allianceInformations;
        public byte aggressable;


        public HumanOptionAlliance()
        {
        }

        public HumanOptionAlliance(Types.AllianceInformations allianceInformations, byte aggressable)
        {
            this.allianceInformations = allianceInformations;
            this.aggressable = aggressable;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            allianceInformations.Serialize(writer);
            writer.WriteByte(aggressable);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            allianceInformations = new Types.AllianceInformations();
            allianceInformations.Deserialize(reader);
            aggressable = reader.ReadByte();
            if (aggressable < 0)
                throw new Exception("Forbidden value on aggressable = " + aggressable + ", it doesn't respect the following condition : aggressable < 0");


        }


    }


}