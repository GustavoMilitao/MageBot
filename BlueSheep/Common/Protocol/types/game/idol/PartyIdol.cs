using BlueSheep.Common.IO;
using System;
using System.Collections.Generic;

namespace BlueSheep.Common.Protocol.Types
{
    public class PartyIdol : Idol
    {
        public new const uint ID = 490;
        public List<ulong> ownersIds;

        public virtual void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)this.ownersIds.Count);
            uint _loc2_ = 0;
            while (_loc2_ < this.ownersIds.Count)
            {
                if (this.ownersIds[(int)_loc2_] < 0 || this.ownersIds[(int)_loc2_] > 9007199254740990)
                {
                    throw new Exception("Forbidden value (" + this.ownersIds[(int)_loc2_] + ") on element 1 (starting at 1) of ownersIds.");
                }
                writer.WriteVarLong(this.ownersIds[(int)_loc2_]);
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