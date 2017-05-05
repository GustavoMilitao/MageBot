


















// Generated on 12/11/2014 19:02:10
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class SkillActionDescription
    {

        public const int ID = 102;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public int skillId;


        public SkillActionDescription()
        {
        }

        public SkillActionDescription(int skillId)
        {
            this.skillId = skillId;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteVarShort((short)skillId);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            skillId = reader.ReadVarUhShort();
            if (skillId < 0)
                throw new Exception("Forbidden value on skillId = " + skillId + ", it doesn't respect the following condition : skillId < 0");


        }


    }


}