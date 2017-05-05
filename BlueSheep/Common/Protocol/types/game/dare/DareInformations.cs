using BlueSheep.Common.IO;
using System;
using System.Collections.Generic;

namespace BlueSheep.Common.Protocol.Types
{
    public class DareInformations : CharacterBasicMinimalInformations
    {
        public const int ID = 502;

        public double dareId = 0;
        public CharacterBasicMinimalInformations creator;
        public double subscriptionFee = 0;
        public double jackpot = 0;
        public uint maxCountWinners = 0;
        public double endDate = 0;
        public bool isPrivate = false;
        public uint guildId = 0;
        public uint allianceId = 0;
        public double startDate = 0;
        uint _loc2_ = 0;
        DareCriteria _loc4_ = null;
        uint _loc3_ = 0;
        List<DareCriteria> criterions;

        public virtual void Serialize(BigEndianWriter writer)
        {
            if (dareId < 0 || dareId > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + dareId + ") on element dareId.");
            }
            writer.WriteDouble(dareId);
            creator.Serialize(writer);
            if (subscriptionFee < 0 || subscriptionFee > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + subscriptionFee + ") on element subscriptionFee.");
            }
            writer.WriteVarLong((long)subscriptionFee);
            if (jackpot < 0 || jackpot > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + jackpot + ") on element jackpot.");
            }
            writer.WriteVarLong((long)jackpot);
            if (maxCountWinners < 0 || maxCountWinners > 65535)
            {
                throw new Exception("Forbidden value (" + maxCountWinners + ") on element maxCountWinners.");
            }
            writer.WriteShort((short)(int)maxCountWinners);
            if (endDate < 0 || endDate > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + endDate + ") on element endDate.");
            }
            writer.WriteDouble(endDate);
            writer.WriteBoolean(isPrivate);
            if (guildId < 0)
            {
                throw new Exception("Forbidden value (" + guildId + ") on element guildId.");
            }
            writer.WriteVarInt(guildId);
            if (allianceId < 0)
            {
                throw new Exception("Forbidden value (" + allianceId + ") on element allianceId.");
            }
            writer.WriteVarInt(allianceId);
            writer.WriteShort((short)(int)criterions.Count);
            uint _loc2_ = 0;
            while (_loc2_ < criterions.Count)
            {
                (criterions[(int)_loc2_] as DareCriteria).Serialize(writer);
                _loc2_++;
            }
            if (startDate < 0 || startDate > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + startDate + ") on element startDate.");
            }
            writer.WriteDouble(startDate);
        }
        public virtual void Deserialize(BigEndianReader reader)
        {
            DareCriteria _loc4_ = new DareCriteria(); ;
            _dareIdFunc(reader);
            creator = new CharacterBasicMinimalInformations();
            creator.Deserialize(reader);
            _subscriptionFeeFunc(reader);
            _jackpotFunc(reader);
            _maxCountWinnersFunc(reader);
            _endDateFunc(reader);
            _isPrivateFunc(reader);
            _guildIdFunc(reader);
            _allianceIdFunc(reader);
            uint _loc2_ = reader.ReadUShort();
            uint _loc3_ = 0;
            while (_loc3_ < _loc2_)
            {
                _loc4_.Deserialize(reader);
                criterions.Add(_loc4_);
                _loc3_++;
            }
            _startDateFunc(reader);
        }

        private void _jackpotFunc(BigEndianReader param1)
        {
            jackpot = param1.ReadVarUhLong();
            if (jackpot < 0 || jackpot > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + jackpot + ") on element of DareInformations.jackpot.");
            }
        }
        private void _endDateFunc(BigEndianReader param1)
        {
            endDate = param1.ReadDouble();
            if (endDate < 0 || endDate > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + endDate + ") on element of DareInformations.endDate.");
            }
        }
        private void _guildIdFunc(BigEndianReader param1)
        {
            guildId = param1.ReadVarUhInt();
            if (guildId < 0)
            {
                throw new Exception("Forbidden value (" + guildId + ") on element of DareInformations.guildId.");
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

        private void _startDateFunc(BigEndianReader reader)
        {
            startDate = reader.ReadDouble();
            if (startDate < 0 || startDate > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + startDate + ") on element of DareInformations.startDate.");
            }
        }

        private void _subscriptionFeeFunc(BigEndianReader reader)
        {
            subscriptionFee = reader.ReadVarUhLong();
            if (subscriptionFee < 0 || subscriptionFee > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + subscriptionFee + ") on element of DareInformations.subscriptionFee.");
            }
        }

        private void _maxCountWinnersFunc(BigEndianReader reader)
        {
            maxCountWinners = reader.ReadUShort();
            if (maxCountWinners < 0 || maxCountWinners > 65535)
            {
                throw new Exception("Forbidden value (" + maxCountWinners + ") on element of DareInformations.maxCountWinners.");
            }
        }

        private void _isPrivateFunc(BigEndianReader reader)
        {
            isPrivate = reader.ReadBoolean();
        }

        private void _allianceIdFunc(BigEndianReader reader)
        {
            allianceId = reader.ReadVarUhInt();
            if (allianceId < 0)
            {
                throw new Exception("Forbidden value (" + allianceId + ") on element of DareInformations.allianceId.");
            }
        }

    }
}