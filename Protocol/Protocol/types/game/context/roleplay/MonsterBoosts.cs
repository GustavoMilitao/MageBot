using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class MonsterBoosts
    {
        public MonsterBoosts()
        {
        }

        public new const short ID = 497;


        public uint id = 0;

        public uint xpBoost = 0;

        public uint dropBoost = 0;

        public void Serialize(BigEndianWriter writer)
        {
            if (id < 0)
            {
                throw new Exception("Forbidden value (" + id + ") on element id.");
            }
            writer.WriteVarInt(id);
            if (xpBoost < 0)
            {
                throw new Exception("Forbidden value (" + xpBoost + ") on element xpBoost.");
            }
            writer.WriteVarShort((short)xpBoost);
            if (dropBoost < 0)
            {
                throw new Exception("Forbidden value (" + dropBoost + ") on element dropBoost.");
            }
            writer.WriteVarShort((short)dropBoost);
        }

        public void deserialize(BigEndianReader reader)
        {
            deserializeAs_MonsterBoosts(reader);
        }

        public void deserializeAs_MonsterBoosts(BigEndianReader reader)
        {
            _idFunc(reader);
            _xpBoostFunc(reader);
            _dropBoostFunc(reader);
        }

        private void _idFunc(BigEndianReader reader)
        {
            id = reader.ReadVarUhInt();
            if (id < 0)
            {
                throw new Exception("Forbidden value (" + id + ") on element of MonsterBoosts.id.");
            }
        }

        private void _xpBoostFunc(BigEndianReader reader)
        {
            xpBoost = reader.ReadVarUhShort();
            if (xpBoost < 0)
            {
                throw new Exception("Forbidden value (" + xpBoost + ") on element of MonsterBoosts.xpBoost.");
            }
        }

        private void _dropBoostFunc(BigEndianReader reader)
        {
            dropBoost = reader.ReadVarUhShort();
            if (dropBoost < 0)
            {
                throw new Exception("Forbidden value (" + dropBoost + ") on element of MonsterBoosts.dropBoost.");
            }
        }

    }
}