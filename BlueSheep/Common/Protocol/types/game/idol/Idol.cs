using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class Idol : Message
    {
        public const uint ID = 489;
        public uint IdolId = 0;
        public uint xpBonusPercent = 0;
        public uint dropBonusPercent = 0;

        public override uint ProtocolID => ID;

        public override void Serialize(BigEndianWriter writer)
        {
            if (IdolId < 0)
            {
                throw new Exception("Forbidden value (" + IdolId + ") on element id.");
            }
            writer.WriteVarShort((short)IdolId);
            if (xpBonusPercent < 0)
            {
                throw new Exception("Forbidden value (" + xpBonusPercent + ") on element xpBonusPercent.");
            }
            writer.WriteVarShort((short)xpBonusPercent);
            if (dropBonusPercent < 0)
            {
                throw new Exception("Forbidden value (" + dropBonusPercent + ") on element dropBonusPercent.");
            }
            writer.WriteVarShort((short)dropBonusPercent);
        }

        public override void Deserialize(BigEndianReader reader)
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
            if (this.IdolId < 0)
            {
                throw new Exception("Forbidden value (" + this.IdolId + ") on element of Idol.id.");
            }
        }

        private void _dropBonusPercentFunc(BigEndianReader reader)
        {
            this.dropBonusPercent = reader.ReadVarUhShort();
            if (this.dropBonusPercent < 0)
            {
                throw new Exception("Forbidden value (" + dropBonusPercent + ") on element of Idol.dropBonusPercent.");
            }
        }

    }
}