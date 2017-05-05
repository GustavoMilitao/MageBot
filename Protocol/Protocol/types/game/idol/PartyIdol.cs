using BlueSheep.Common.IO;
using System;
using System.Collections.Generic;

namespace BlueSheep.Common.Protocol.Types
{
    public class PartyIdol : Idol
    {
        public new const short ID = 490;
        public List<ulong> ownersIds;

        public PartyIdol()
        {
        }

        public virtual void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)ownersIds.Count);
            uint _loc2_ = 0;
            while (_loc2_ < ownersIds.Count)
            {
                if (ownersIds[(int)_loc2_] < 0 || ownersIds[(int)_loc2_] > 9007199254740990)
                {
                    throw new Exception("Forbidden value (" + ownersIds[(int)_loc2_] + ") on element 1 (starting at 1) of ownersIds.");
                }
                writer.WriteVarLong(ownersIds[(int)_loc2_]);
                _loc2_++;
            }
        }
        public void Deserialize(BigEndianReader reader)
        {
            ulong _loc4_;
            base.Deserialize(reader);
            uint _loc2_ = reader.ReadUShort();
            uint _loc3_ = 0;
            while (_loc3_ < _loc2_)
            {
                _loc4_ = reader.ReadVarUhLong();
                if (_loc4_ < 0 || _loc4_ > 9007199254740990)
                {
                    throw new Exception("Forbidden value (" + _loc4_ + ") on elements of ownersIds.");
                }
                ownersIds.Add(_loc4_);
                _loc3_++;
            }
        }

    }
}