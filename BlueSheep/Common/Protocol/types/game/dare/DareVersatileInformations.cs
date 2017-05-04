using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class DareVersatileInformations
    {
        public new const uint ID = 504;
        public double dareId = 0;
        public uint countEntrants = 0;
        public uint countWinners = 0;

        public void Serializer(BigEndianWriter writer)
        {
            if (dareId < 0 || dareId > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + dareId + ") on element dareId.");
            }
            writer.WriteDouble(dareId);
            if (countEntrants < 0)
            {
                throw new Exception("Forbidden value (" + countEntrants + ") on element countEntrants.");
            }
            writer.WriteInt((int)countEntrants);
            if (countWinners < 0)
            {
                throw new Exception("Forbidden value (" + countWinners + ") on element countWinners.");
            }
            writer.WriteInt((int)countWinners);
        }
        public void deserializeAs_DareVersatileInformations(BigEndianReader reader)
        {
            this._dareIdFunc(reader);
            _countEntrantsFunc(reader);
            this._countWinnersFunc(reader);
        }

        private void _countEntrantsFunc(BigEndianReader reader)
        {
            countEntrants = reader.ReadUInt();
            if (countEntrants < 0)
            {
                throw new Exception("Forbidden value (" + countEntrants + ") on element of DareVersatileInformations.countEntrants.");
            }
        }

        private void _dareIdFunc(BigEndianReader reader)
        {
            this.dareId = reader.ReadDouble();
            if (this.dareId < 0 || this.dareId > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + this.dareId + ") on element of DareInformations.dareId.");
            }
        }

        private void _countWinnersFunc(BigEndianReader reader)
        {
            countWinners = reader.ReadUInt();
            if (this.countWinners < 0)
            {
                throw new Exception("Forbidden value (" + this.countWinners + ") on element of DareVersatileInformations.countWinners.");
            }
        }

    }
}