using BlueSheep.Common.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueSheep.Common.Protocol.Types
{
    public class HouseOnMapInformations : HouseInformations
    {
        public new const uint ID = 510;

        public List<HouseInstanceInformations> houseInstances;

        public virtual void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)doorsOnMap.Length);
            uint _loc2_ = 0;
            while (_loc2_ < doorsOnMap.Length)
            {
                if (this.doorsOnMap[_loc2_] < 0)
                {
                    throw new Exception("Forbidden value (" + this.doorsOnMap[_loc2_] + ") on element 1 (starting at 1) of doorsOnMap.");
                }
                writer.WriteInt(doorsOnMap[_loc2_]);
                _loc2_++;
            }
            writer.WriteShort((short)houseInstances.Count);
            uint _loc3_ = 0;
            while (_loc3_ < this.houseInstances.Count)
            {
                (this.houseInstances[(int)_loc3_] as HouseInstanceInformations).Serialize(writer);
                _loc3_++;
            }
        }
        public virtual void Deserialize(BigEndianReader reader)
        {
            uint _loc6_ = 0;
            HouseInstanceInformations _loc7_ = new HouseInstanceInformations(); ;
            base.Deserialize(reader);
            uint _loc2_ = reader.ReadUShort();
            uint _loc3_ = 0;
            while (_loc3_ < _loc2_)
            {
                _loc6_ = reader.ReadUInt();
                if (_loc6_ < 0)
                {
                    throw new Exception("Forbidden value (" + _loc6_ + ") on elements of doorsOnMap.");
                }
                var aux = doorsOnMap.ToList();
                aux.Add((int)_loc6_);
                doorsOnMap = aux.ToArray();
                _loc3_++;
            }
            uint _loc4_  = reader.ReadUShort();
            uint _loc5_ = 0;
            while (_loc5_ < _loc4_)
            {
                _loc7_.Deserialize(reader);
                houseInstances.Add(_loc7_);
                _loc5_++;
            }
        }
    }
}