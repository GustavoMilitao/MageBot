


















// Generated on 12/11/2014 19:02:04
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class AbstractFightTeamInformations
    {

        public new const int ID = 116;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public byte teamId;
        public double leaderId;
        public byte teamSide;
        public byte teamTypeId;
        public byte nbWaves;


        public AbstractFightTeamInformations()
        {
        }

        public AbstractFightTeamInformations(byte teamId, double leaderId, byte teamSide, byte teamTypeId, byte nbWaves)
        {
            this.teamId = teamId;
            this.leaderId = leaderId;
            this.teamSide = teamSide;
            this.teamTypeId = teamTypeId;
            this.nbWaves = nbWaves;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteByte(teamId);
            writer.WriteDouble(leaderId);
            writer.WriteByte(teamSide);
            writer.WriteByte(teamTypeId);
            writer.WriteByte(nbWaves);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            teamId = reader.ReadByte();
            if (teamId < 0)
                throw new Exception("Forbidden value on teamId = " + teamId + ", it doesn't respect the following condition : teamId < 0");
            leaderId = reader.ReadDouble();
            teamSide = reader.ReadByte();
            teamTypeId = reader.ReadByte();
            if (teamTypeId < 0)
                throw new Exception("Forbidden value on teamTypeId = " + teamTypeId + ", it doesn't respect the following condition : teamTypeId < 0");
            nbWaves = reader.ReadByte();
            if (nbWaves < 0)
                throw new Exception("Forbidden value on nbWaves = " + nbWaves + ", it doesn't respect the following condition : nbWaves < 0");


        }


    }


}