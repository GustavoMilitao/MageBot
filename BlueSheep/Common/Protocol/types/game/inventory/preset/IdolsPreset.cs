using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;
using System;
using System.Collections.Generic;

namespace BlueSheep.Common.Protocol.Types
{
    public class IdolsPreset
    {
        public const int ID = 491;
        public uint presetId = 0;
        public uint symbolId = 0;
        public List<uint> idolId;

        public IdolsPreset()
        {

        }
        public virtual void Serialize(BigEndianWriter writer)
        {
            if (presetId < 0)
            {
                throw new Exception("Forbidden value (" + presetId + ") on element presetId.");
            }
            writer.WriteByte((byte)presetId);
            if (symbolId < 0)
            {
                throw new Exception("Forbidden value (" + symbolId + ") on element symbolId.");
            }
            writer.WriteByte((byte)symbolId);
            writer.WriteShort((short)(int)idolId.Count);
            uint _loc2_ = 0;
            while (_loc2_ < idolId.Count)
            {
                if (idolId[(int)_loc2_] < 0)
                {
                    throw new Exception("Forbidden value (" + idolId[(int)_loc2_] + ") on element 3 (starting at 1) of idolId.");
                }
                writer.WriteVarShort((short)(int)idolId[(int)_loc2_]);
                _loc2_++;
            }
        }
        public virtual void Deserialize(BigEndianReader reader)
        {
            uint _loc4_ = 0;
            _presetIdFunc(reader);
            _symbolIdFunc(reader);
            uint _loc2_ = reader.ReadUShort();
            uint _loc3_  = 0;
            while (_loc3_ < _loc2_)
            {
                _loc4_ = reader.ReadVarUhShort();
                if (_loc4_ < 0)
                {
                    throw new Exception("Forbidden value (" + _loc4_ + ") on elements of idolId.");
                }
                idolId.Add(_loc4_);
                _loc3_++;
            }
        }

        private void _symbolIdFunc(BigEndianReader reader)
        {
            symbolId = reader.ReadByte();
            if (symbolId < 0)
            {
                throw new Exception("Forbidden value (" + symbolId + ") on element of IdolsPreset.symbolId.");
            }
        }

        private void _idolIdFunc(BigEndianReader reader)
        {
            uint _loc2_ = reader.ReadVarUhShort();
            if (_loc2_ < 0)
            {
                throw new Exception("Forbidden value (" + _loc2_ + ") on elements of idolId.");
            }
            idolId.Add(_loc2_);
        }

        private void _presetIdFunc(BigEndianReader reader)
        {
            presetId = reader.ReadByte();
            if (presetId < 0)
            {
                throw new Exception("Forbidden value (" + presetId + ") on element of IdolsPreset.presetId.");
            }
        }

    }
}