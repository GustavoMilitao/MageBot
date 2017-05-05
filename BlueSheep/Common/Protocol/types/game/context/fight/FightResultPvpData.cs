


















// Generated on 12/11/2014 19:02:04
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class FightResultPvpData : FightResultAdditionalData
{

public new const int ID = 190;
public override int TypeId
{
    get { return ID; }
}

public byte grade;
        public int minHonorForGrade;
        public int maxHonorForGrade;
        public int honor;
        public int honorDelta;
        

public FightResultPvpData()
{
}

public FightResultPvpData(byte grade, int minHonorForGrade, int maxHonorForGrade, int honor, int honorDelta)
        {
            this.grade = grade;
            this.minHonorForGrade = minHonorForGrade;
            this.maxHonorForGrade = maxHonorForGrade;
            this.honor = honor;
            this.honorDelta = honorDelta;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteByte(grade);
            writer.WriteVarShort((short)minHonorForGrade);
            writer.WriteVarShort((short)maxHonorForGrade);
            writer.WriteVarShort((short)honor);
            writer.WriteVarShort((short)honorDelta);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            grade = reader.ReadByte();
            if (grade < 0 || grade > 255)
                throw new Exception("Forbidden value on grade = " + grade + ", it doesn't respect the following condition : grade < 0 || grade > 255");
            minHonorForGrade = reader.ReadVarUhShort();
            if (minHonorForGrade < 0 || minHonorForGrade > 20000)
                throw new Exception("Forbidden value on minHonorForGrade = " + minHonorForGrade + ", it doesn't respect the following condition : minHonorForGrade < 0 || minHonorForGrade > 20000");
            maxHonorForGrade = reader.ReadVarUhShort();
            if (maxHonorForGrade < 0 || maxHonorForGrade > 20000)
                throw new Exception("Forbidden value on maxHonorForGrade = " + maxHonorForGrade + ", it doesn't respect the following condition : maxHonorForGrade < 0 || maxHonorForGrade > 20000");
            honor = reader.ReadVarUhShort();
            if (honor < 0 || honor > 20000)
                throw new Exception("Forbidden value on honor = " + honor + ", it doesn't respect the following condition : honor < 0 || honor > 20000");
            honorDelta = reader.ReadVarUhShort();
            

}


}


}