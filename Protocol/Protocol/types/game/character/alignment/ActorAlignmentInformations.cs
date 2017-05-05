


















// Generated on 12/11/2014 19:02:03
using System;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class ActorAlignmentInformations
    {

        public new const short ID = 201;
        public virtual short TypeId
        {
            get { return ID; }
        }

        public byte alignmentSide;
        public byte alignmentValue;
        public byte alignmentGrade;
        public double characterPower;


        public ActorAlignmentInformations()
        {
        }

        public ActorAlignmentInformations(byte alignmentSide, byte alignmentValue, byte alignmentGrade, double characterPower)
        {
            this.alignmentSide = alignmentSide;
            this.alignmentValue = alignmentValue;
            this.alignmentGrade = alignmentGrade;
            this.characterPower = characterPower;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteByte(alignmentSide);
            writer.WriteByte(alignmentValue);
            writer.WriteByte(alignmentGrade);
            writer.WriteDouble(characterPower);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            alignmentSide = reader.ReadByte();
            alignmentValue = reader.ReadByte();
            if (alignmentValue < 0)
                throw new Exception("Forbidden value on alignmentValue = " + alignmentValue + ", it doesn't respect the following condition : alignmentValue < 0");
            alignmentGrade = reader.ReadByte();
            if (alignmentGrade < 0)
                throw new Exception("Forbidden value on alignmentGrade = " + alignmentGrade + ", it doesn't respect the following condition : alignmentGrade < 0");
            characterPower = reader.ReadDouble();
        }


    }


}