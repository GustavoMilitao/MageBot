


















// Generated on 12/11/2014 19:02:04
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class FightCommonInformations
    {

        public new const int ID = 43;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public int fightId;
        public byte fightType;
        public Types.FightTeamInformations[] fightTeams;
        public int[] fightTeamsPositions;
        public Types.FightOptionsInformations[] fightTeamsOptions;


        public FightCommonInformations()
        {
        }

        public FightCommonInformations(int fightId, byte fightType, Types.FightTeamInformations[] fightTeams, int[] fightTeamsPositions, Types.FightOptionsInformations[] fightTeamsOptions)
        {
            this.fightId = fightId;
            this.fightType = fightType;
            this.fightTeams = fightTeams;
            this.fightTeamsPositions = fightTeamsPositions;
            this.fightTeamsOptions = fightTeamsOptions;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteInt(fightId);
            writer.WriteByte(fightType);
            writer.WriteUShort((ushort)fightTeams.Length);
            foreach (var entry in fightTeams)
            {
                writer.WriteShort((short)entry.TypeId);
                entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)fightTeamsPositions.Length);
            foreach (var entry in fightTeamsPositions)
            {
                writer.WriteVarShort((short)entry);
            }
            writer.WriteUShort((ushort)fightTeamsOptions.Length);
            foreach (var entry in fightTeamsOptions)
            {
                entry.Serialize(writer);
            }


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            fightId = reader.ReadInt();
            fightType = reader.ReadByte();
            if (fightType < 0)
                throw new Exception("Forbidden value on fightType = " + fightType + ", it doesn't respect the following condition : fightType < 0");
            var limit = reader.ReadUShort();
            fightTeams = new Types.FightTeamInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                fightTeams[i] = Types.ProtocolTypeManager.GetInstance<FightTeamInformations>(reader.ReadUShort());
                fightTeams[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            fightTeamsPositions = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                fightTeamsPositions[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            fightTeamsOptions = new FightOptionsInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                fightTeamsOptions[i] = new FightOptionsInformations();
                fightTeamsOptions[i].Deserialize(reader);
            }


        }


    }


}