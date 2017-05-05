using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class HumanOptionSkillUse : HumanOption
    {
        public new const int ID = 495;

        public uint elementId = 0;

        public uint skillId = 0;

        public double skillEndTime = 0;

        public HumanOptionSkillUse()
        {

        }

        public void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            if (elementId < 0)
            {
                throw new Exception("Forbidden value (" + elementId + ") on element elementId.");
            }
            writer.WriteVarInt(elementId);
            if (skillId < 0)
            {
                throw new Exception("Forbidden value (" + skillId + ") on element skillId.");
            }
            writer.WriteVarShort((short)(int)skillId);
            if (skillEndTime < -9007199254740990 || skillEndTime > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + skillEndTime + ") on element skillEndTime.");
            }
            writer.WriteDouble(skillEndTime);
        }

        public virtual void Serialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            _elementIdFunc(reader);
            _skillIdFunc(reader);
            _skillEndTimeFunc(reader);
        }

        private void _elementIdFunc(BigEndianReader reader)
        {
            elementId = reader.ReadVarUhInt();
            if (elementId < 0)
            {
                throw new Exception("Forbidden value (" + elementId + ") on element of HumanOptionSkillUse.elementId.");
            }
        }

        private void _skillIdFunc(BigEndianReader reader)
        {
            skillId = reader.ReadVarUhShort();
            if (skillId < 0)
            {
                throw new Exception("Forbidden value (" + skillId + ") on element of HumanOptionSkillUse.skillId.");
            }
        }

        private void _skillEndTimeFunc(BigEndianReader reader)
        {
            skillEndTime = reader.ReadDouble();
            if (skillEndTime < -9007199254740990 || skillEndTime > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + skillEndTime + ") on element of HumanOptionSkillUse.skillEndTime.");
            }
        }


    }
}