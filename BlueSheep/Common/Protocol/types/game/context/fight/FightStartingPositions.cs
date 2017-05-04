using BlueSheep.Common.IO;
using System;
using System.Collections.Generic;

namespace BlueSheep.Common.Protocol.Types
{
    public class FightStartingPositions
    {

        public new const short ID = 513;


        public List<uint> positionsForChallengers;

        public List<uint> positionsForDefenders;

        public FightStartingPositions()
        {
        }

        public void Serialize(BigEndianWriter writer)
        {
            writer.WriteShort((short)positionsForChallengers.Count);
            uint _loc2_ = 0;
            while (_loc2_ < positionsForChallengers.Count)
            {
                if (positionsForChallengers[(int)_loc2_] < 0 || positionsForChallengers[(int)_loc2_] > 559)
                {
                    throw new Exception("Forbidden value (" + positionsForChallengers[(int)_loc2_] + ") on element 1 (starting at 1) of positionsForChallengers.");
                }
                writer.WriteVarShort((short)positionsForChallengers[(int)_loc2_]);
                _loc2_++;
            }
            writer.WriteShort((short)positionsForDefenders.Count);
            uint _loc3_ = 0;
            while (_loc3_ < positionsForDefenders.Count)
            {
                if (positionsForDefenders[(int)_loc3_] < 0 || positionsForDefenders[(int)_loc3_] > 559)
                {
                    throw new Exception("Forbidden value (" + positionsForDefenders[(int)_loc3_] + ") on element 2 (starting at 1) of positionsForDefenders.");
                }
                writer.WriteVarShort((short)positionsForDefenders[(int)_loc3_]);
                _loc3_++;
            }
        }

        public void Deserialize(BigEndianReader reader)
        {
            uint _loc6_ = 0;
            uint _loc7_ = 0;
            uint _loc2_ = reader.ReadUShort();
            uint _loc3_ = 0;
            while (_loc3_ < _loc2_)
            {
                _loc6_ = reader.ReadVarUhShort();
                if (_loc6_ < 0 || _loc6_ > 559)
                {
                    throw new Exception("Forbidden value (" + _loc6_ + ") on elements of positionsForChallengers.");
                }
                positionsForChallengers.Add(_loc6_);
                _loc3_++;
            }
            uint _loc4_ = reader.ReadUShort();
            uint _loc5_ = 0;
            while (_loc5_ < _loc4_)
            {
                _loc7_ = reader.ReadVarUhShort();
                if (_loc7_ < 0 || _loc7_ > 559)
                {
                    throw new Exception("Forbidden value (" + _loc7_ + ") on elements of positionsForDefenders.");
                }
                positionsForDefenders.Add(_loc7_);
                _loc5_++;
            }
        }

    }
}