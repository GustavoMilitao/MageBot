using BlueSheep.Common.IO;
using System.Collections.Generic;

namespace BlueSheep.Common.Protocol.Types
{
    public class PaddockInstancesInformations : PaddockInformations
    {
        public new const short ID = 509;

        public List<PaddockBuyableInformations> paddocks;
        public PaddockInstancesInformations()
        {

        }
        public virtual void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)paddocks.Count);
            uint _loc2_ = 0;
            while (_loc2_ < paddocks.Count)
            {
                writer.WriteShort((paddocks[(int)_loc2_] as PaddockBuyableInformations).TypeId);
                (paddocks[(int)_loc2_] as PaddockBuyableInformations).Serialize(writer);
                _loc2_++;
            }
        }

        public virtual void Deserialize(BigEndianReader reader)
        {
            ushort _loc4_ = 0;
            PaddockBuyableInformations _loc5_ = new PaddockBuyableInformations();
            base.Deserialize(reader);
            uint _loc2_ = reader.ReadUShort();
            uint _loc3_ = 0;
            while (_loc3_ < _loc2_)
            {
                _loc4_ = reader.ReadUShort();
                _loc5_ = ProtocolTypeManager.GetInstance<PaddockBuyableInformations>((short)_loc4_);
                _loc5_.Deserialize(reader);
                paddocks.Add(_loc5_);
                _loc3_++;
            }
        }
    }
}