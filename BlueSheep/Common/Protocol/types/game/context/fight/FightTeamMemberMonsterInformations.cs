


















// Generated on 12/11/2014 19:02:04
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class FightTeamMemberMonsterInformations : FightTeamMemberInformations
    {

        public new const int ID = 6;
        public override int TypeId
        {
            get { return ID; }
        }

        public int monsterId;
        public byte grade;


        public FightTeamMemberMonsterInformations()
        {
        }

        public FightTeamMemberMonsterInformations(int id, int monsterId, byte grade)
                 : base(id)
        {
            this.monsterId = monsterId;
            this.grade = grade;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteInt(monsterId);
            writer.WriteByte(grade);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            monsterId = reader.ReadInt();
            grade = reader.ReadByte();
            if (grade < 0)
                throw new Exception("Forbidden value on grade = " + grade + ", it doesn't respect the following condition : grade < 0");


        }


    }


}