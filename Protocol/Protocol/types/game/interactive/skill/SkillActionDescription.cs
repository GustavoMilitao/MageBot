


















// Generated on 12/11/2014 19:02:10
using System;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class SkillActionDescription
    {

        public const short ID = 102;
        public virtual short TypeId
        {
            get { return ID; }
        }

        public ushort skillId;


        public SkillActionDescription()
        {
        }

        public SkillActionDescription(ushort skillId)
        {
            this.skillId = skillId;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteVarShort(skillId);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            skillId = reader.ReadVarUhShort();
            if (skillId < 0)
                throw new Exception("Forbidden value on skillId = " + skillId + ", it doesn't respect the following condition : skillId < 0");


        }


    }


}