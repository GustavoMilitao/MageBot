


















// Generated on 12/11/2014 19:02:04
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class FightTeamInformations : AbstractFightTeamInformations
    {

        public new const int ID = 33;
        public override int TypeId
        {
            get { return ID; }
        }

        public Types.FightTeamMemberInformations[] teamMembers;


        public FightTeamInformations()
        {
        }

        public FightTeamInformations(byte teamId, int leaderId, byte teamSide, byte teamTypeId, byte nbWaves, Types.FightTeamMemberInformations[] teamMembers)
                 : base(teamId, leaderId, teamSide, teamTypeId, nbWaves)
        {
            this.teamMembers = teamMembers;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteUShort((ushort)teamMembers.Length);
            foreach (var entry in teamMembers)
            {
                writer.WriteShort((short)entry.TypeId);
                entry.Serialize(writer);
            }


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            var limit = reader.ReadUShort();
            teamMembers = new Types.FightTeamMemberInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                teamMembers[i] = Types.ProtocolTypeManager.GetInstance<FightTeamMemberInformations>(reader.ReadUShort());
                teamMembers[i].Deserialize(reader);
            }


        }


    }


}