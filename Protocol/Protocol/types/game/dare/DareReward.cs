using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class DareReward
    {
        public new const short ID = 505;
        public uint type = 0;
        public uint monsterId = 0;
        public double kamas = 0;
        public double dareId = 0;

        public DareReward()
        {
        }

        public virtual void Serialize(BigEndianWriter writer)
        {
            writer.WriteByte((byte)type);
            if (monsterId < 0)
            {
                throw new Exception("Forbidden value (" + monsterId + ") on element monsterId.");
            }
            writer.WriteVarShort((short)monsterId);
            if (kamas < 0 || kamas > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + kamas + ") on element kamas.");
            }
            writer.WriteVarLong((long)kamas);
            if (dareId < 0 || dareId > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + dareId + ") on element dareId.");
            }
            writer.WriteDouble(dareId);
        }

        public virtual void Deserialize(BigEndianReader reader)
        {
            _typeFunc(reader);
            _monsterIdFunc(reader);
            _kamasFunc(reader);
            _dareIdFunc(reader);
        }
        
        private void _monsterIdFunc(BigEndianReader reader)
        {
            monsterId = reader.ReadVarUhShort();
            if (monsterId < 0)
            {
                throw new Exception("Forbidden value (" + monsterId + ") on element of DareReward.monsterId.");
            }
        }
        private void _dareIdFunc(BigEndianReader reader)
        {
            dareId = reader.ReadDouble();
            if (dareId < 0 || dareId > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + dareId + ") on element of DareReward.dareId.");
            }
        }

        private void _typeFunc(BigEndianReader reader)
        {
            type = reader.ReadByte();
            if (type < 0)
            {
                throw new Exception("Forbidden value (" + type + ") on element of DareReward.type.");
            }
        }

        private void _kamasFunc(BigEndianReader reader)
        {
            kamas = reader.ReadVarUhLong();
            if (kamas < 0 || kamas > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + kamas + ") on element of DareReward.kamas.");
            }
        }
    }
}