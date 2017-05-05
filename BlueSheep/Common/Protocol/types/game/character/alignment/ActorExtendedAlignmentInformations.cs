


















// Generated on 12/11/2014 19:02:03
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class ActorExtendedAlignmentInformations : ActorAlignmentInformations
    {

        public new const short ID = 202;
        public override short TypeId
        {
            get { return ID; }
        }

        public ushort honor;
        public ushort honorGradeFloor;
        public ushort honorNextGradeFloor;
        public byte aggressable;


        public ActorExtendedAlignmentInformations()
        {
        }

        public ActorExtendedAlignmentInformations(byte alignmentSide, byte alignmentValue, byte alignmentGrade, double characterPower, ushort honor, ushort honorGradeFloor, ushort honorNextGradeFloor, byte aggressable)
                 : base(alignmentSide, alignmentValue, alignmentGrade, characterPower)
        {
            this.honor = honor;
            this.honorGradeFloor = honorGradeFloor;
            this.honorNextGradeFloor = honorNextGradeFloor;
            this.aggressable = aggressable;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteVarShort(honor);
            writer.WriteVarShort(honorGradeFloor);
            writer.WriteVarShort(honorNextGradeFloor);
            writer.WriteByte(aggressable);
        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            honor = reader.ReadVarUhShort();
            if (honor < 0 || honor > 20000)
                throw new Exception("Forbidden value on honor = " + honor + ", it doesn't respect the following condition : honor < 0 || honor > 20000");
            honorGradeFloor = reader.ReadVarUhShort();
            if (honorGradeFloor < 0 || honorGradeFloor > 20000)
                throw new Exception("Forbidden value on honorGradeFloor = " + honorGradeFloor + ", it doesn't respect the following condition : honorGradeFloor < 0 || honorGradeFloor > 20000");
            honorNextGradeFloor = reader.ReadVarUhShort();
            if (honorNextGradeFloor < 0 || honorNextGradeFloor > 20000)
                throw new Exception("Forbidden value on honorNextGradeFloor = " + honorNextGradeFloor + ", it doesn't respect the following condition : honorNextGradeFloor < 0 || honorNextGradeFloor > 20000");
            aggressable = reader.ReadByte();
            if (aggressable < 0)
                throw new Exception("Forbidden value on aggressable = " + aggressable + ", it doesn't respect the following condition : aggressable < 0");


        }


    }


}