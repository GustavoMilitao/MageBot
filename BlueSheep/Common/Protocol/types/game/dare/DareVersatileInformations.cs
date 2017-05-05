using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class DareVersatileInformations
    {
        public new const int ID = 504;
        public double dareId = 0;
        public uint countEntrants = 0;
        public uint countWinners = 0;

        public DareVersatileInformations()
        {
        }

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
            _dareIdFunc(reader);
            _countEntrantsFunc(reader);
            _countWinnersFunc(reader);
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
            dareId = reader.ReadDouble();
            if (dareId < 0 || dareId > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + dareId + ") on element of DareInformations.dareId.");
            }
        }

        private void _countWinnersFunc(BigEndianReader reader)
        {
            countWinners = reader.ReadUInt();
            if (countWinners < 0)
            {
                throw new Exception("Forbidden value (" + countWinners + ") on element of DareVersatileInformations.countWinners.");
            }
        }

    }
}