using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class Idol
    {
        public const int ID = 489;
        public uint IdolId = 0;
        public uint xpBonusPercent = 0;
        public uint dropBonusPercent = 0;

        public Idol()
        {
        }

        public virtual void Serialize(BigEndianWriter writer)
        {
            if (IdolId < 0)
            {
                throw new Exception("Forbidden value (" + IdolId + ") on element id.");
            }
            writer.WriteVarShort((short)(int)IdolId);
            if (xpBonusPercent < 0)
            {
                throw new Exception("Forbidden value (" + xpBonusPercent + ") on element xpBonusPercent.");
            }
            writer.WriteVarShort((short)(int)xpBonusPercent);
            if (dropBonusPercent < 0)
            {
                throw new Exception("Forbidden value (" + dropBonusPercent + ") on element dropBonusPercent.");
            }
            writer.WriteVarShort((short)(int)dropBonusPercent);
        }

        public virtual void Deserialize(BigEndianReader reader)
        {
            _idFunc(reader);
            _xpBonusPercentFunc(reader);
            _dropBonusPercentFunc(reader);
        }

        private void _xpBonusPercentFunc(BigEndianReader reader)
        {
            xpBonusPercent = reader.ReadVarUhShort();
            if (xpBonusPercent < 0)
            {
                throw new Exception("Forbidden value (" + xpBonusPercent + ") on element of Idol.xpBonusPercent.");
            }
        }

        private void _idFunc(BigEndianReader reader)
        {
            IdolId = reader.ReadVarUhShort();
            if (IdolId < 0)
            {
                throw new Exception("Forbidden value (" + IdolId + ") on element of Idol.id.");
            }
        }

        private void _dropBonusPercentFunc(BigEndianReader reader)
        {
            dropBonusPercent = reader.ReadVarUhShort();
            if (dropBonusPercent < 0)
            {
                throw new Exception("Forbidden value (" + dropBonusPercent + ") on element of Idol.dropBonusPercent.");
            }
        }

    }
}