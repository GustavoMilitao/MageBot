


















// Generated on 12/11/2014 19:02:04
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class FightTeamMemberInformations
    {

        public new const int ID = 44;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public double id;


        public FightTeamMemberInformations()
        {
        }

        public FightTeamMemberInformations(double id)
        {
            this.id = id;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteDouble(id);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            id = reader.ReadDouble();


        }


    }


}