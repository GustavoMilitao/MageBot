


















// Generated on 12/11/2014 19:02:10
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class SkillActionDescriptionCraft : SkillActionDescription
    {

        public new const int ID = 100;
        public override int TypeId
        {
            get { return ID; }
        }

        public byte probability;


        public SkillActionDescriptionCraft()
        {
        }

        public SkillActionDescriptionCraft(int skillId, byte probability)
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