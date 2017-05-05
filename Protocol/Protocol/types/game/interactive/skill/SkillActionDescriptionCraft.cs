


















// Generated on 12/11/2014 19:02:10
using System;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class SkillActionDescriptionCraft : SkillActionDescription
    {

        public new const short ID = 100;
        public override short TypeId
        {
            get { return ID; }
        }

        public byte probability;


        public SkillActionDescriptionCraft()
        {
        }

        public SkillActionDescriptionCraft(ushort skillId, byte probability)
                 : base(skillId)
        {
            this.probability = probability;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteByte(probability);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            probability = reader.ReadByte();
            if (probability < 0)
                throw new Exception("Forbidden value on probability = " + probability + ", it doesn't respect the following condition : probability < 0");


        }


    }


}