


















// Generated on 12/11/2014 19:02:04
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class FightTeamLightInformations : AbstractFightTeamInformations
    {

        public new const int ID = 115;
        public override int TypeId
        {
            get { return ID; }
        }

        public byte teamMembersCount;
        public int meanLevel;
        public bool hasFriend;

        public bool hasGuildMember;

        public bool hasAllianceMember;

        public bool hasGroupMember;

        public bool hasMyTaxCollector;


        public FightTeamLightInformations()
        {
        }

        public FightTeamLightInformations(byte teamId, int leaderId, byte teamSide, byte teamTypeId, byte nbWaves, byte teamMembersCount, int meanLevel)
                 : base(teamId, leaderId, teamSide, teamTypeId, nbWaves)
        {
            this.teamMembersCount = teamMembersCount;
            this.meanLevel = meanLevel;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            byte _loc2_ = 0;
            _loc2_ = BooleanByteWrapper.SetFlag(_loc2_, 0, hasFriend);
            _loc2_ = BooleanByteWrapper.SetFlag(_loc2_, 1, hasGuildMember);
            _loc2_ = BooleanByteWrapper.SetFlag(_loc2_, 2, hasAllianceMember);
            _loc2_ = BooleanByteWrapper.SetFlag(_loc2_, 3, hasGroupMember);
            _loc2_ = BooleanByteWrapper.SetFlag(_loc2_, 4, hasMyTaxCollector);
            writer.WriteByte(_loc2_);
            writer.WriteByte(teamMembersCount);
            writer.WriteVarInt(meanLevel);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            byte _loc2_ = reader.ReadByte();
            hasFriend = BooleanByteWrapper.GetFlag(_loc2_, 0);
            hasGuildMember = BooleanByteWrapper.GetFlag(_loc2_, 1);
            hasAllianceMember = BooleanByteWrapper.GetFlag(_loc2_, 2);
            hasGroupMember = BooleanByteWrapper.GetFlag(_loc2_, 3);
            hasMyTaxCollector = BooleanByteWrapper.GetFlag(_loc2_, 4);
            teamMembersCount = reader.ReadByte();
            if (teamMembersCount < 0)
                throw new Exception("Forbidden value on teamMembersCount = " + teamMembersCount + ", it doesn't respect the following condition : teamMembersCount < 0");
            meanLevel = reader.ReadVarInt();
            if (meanLevel < 0)
                throw new Exception("Forbidden value on meanLevel = " + meanLevel + ", it doesn't respect the following condition : meanLevel < 0");


        }


    }


}