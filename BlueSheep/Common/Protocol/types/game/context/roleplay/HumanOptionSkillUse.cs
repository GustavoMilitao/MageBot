using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class HumanOptionSkillUse : HumanOption
    {

        public uint elementId = 0;

        public uint skillId = 0;

        public double skillEndTime = 0;

        public void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            if (this.elementId < 0)
            {
                throw new Exception("Forbidden value (" + this.elementId + ") on element elementId.");
            }
            writer.WriteVarInt(this.elementId);
            if (this.skillId < 0)
            {
                throw new Exception("Forbidden value (" + this.skillId + ") on element skillId.");
            }
            writer.WriteVarShort((short)skillId);
            if (this.skillEndTime < -9007199254740990 || this.skillEndTime > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + this.skillEndTime + ") on element skillEndTime.");
            }
            writer.WriteDouble(this.skillEndTime);
        }

        public virtual void Serialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            this._elementIdFunc(reader);
            this._skillIdFunc(reader);
            this._skillEndTimeFunc(reader);
        }

        private void _elementIdFunc(BigEndianReader reader)
        {
            this.elementId = reader.ReadVarUhInt();
            if (this.elementId < 0)
            {
                throw new Exception("Forbidden value (" + this.elementId + ") on element of HumanOptionSkillUse.elementId.");
            }
        }

        private void _skillIdFunc(BigEndianReader reader)
        {
            this.skillId = reader.ReadVarUhShort();
            if (this.skillId < 0)
            {
                throw new Exception("Forbidden value (" + this.skillId + ") on element of HumanOptionSkillUse.skillId.");
            }
        }

        private void _skillEndTimeFunc(BigEndianReader reader)
        {
            this.skillEndTime = reader.ReadDouble();
            if (this.skillEndTime < -9007199254740990 || this.skillEndTime > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + this.skillEndTime + ") on element of HumanOptionSkillUse.skillEndTime.");
            }
        }


    }
}