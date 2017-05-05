


















// Generated on 12/11/2014 19:02:12
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class AllianceVersatileInformations
    {

        public new const int ID = 432;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public int allianceId;
        public int nbGuilds;
        public int nbMembers;
        public int nbSubarea;


        public AllianceVersatileInformations()
        {
        }

        public AllianceVersatileInformations(int allianceId, int nbGuilds, int nbMembers, int nbSubarea)
        {
            this.allianceId = allianceId;
            this.nbGuilds = nbGuilds;
            this.nbMembers = nbMembers;
            this.nbSubarea = nbSubarea;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteVarInt(allianceId);
            writer.WriteVarShort((short)nbGuilds);
            writer.WriteVarShort((short)nbMembers);
            writer.WriteVarShort((short)nbSubarea);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            allianceId = reader.ReadVarInt();
            if (allianceId < 0)
                throw new Exception("Forbidden value on allianceId = " + allianceId + ", it doesn't respect the following condition : allianceId < 0");
            nbGuilds = reader.ReadVarUhShort();
            if (nbGuilds < 0)
                throw new Exception("Forbidden value on nbGuilds = " + nbGuilds + ", it doesn't respect the following condition : nbGuilds < 0");
            nbMembers = reader.ReadVarUhShort();
            if (nbMembers < 0)
                throw new Exception("Forbidden value on nbMembers = " + nbMembers + ", it doesn't respect the following condition : nbMembers < 0");
            nbSubarea = reader.ReadVarUhShort();
            if (nbSubarea < 0)
                throw new Exception("Forbidden value on nbSubarea = " + nbSubarea + ", it doesn't respect the following condition : nbSubarea < 0");


        }


    }


}