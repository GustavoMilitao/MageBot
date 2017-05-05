


















// Generated on 12/11/2014 19:02:10
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class HouseInformations
    {

        public new const int ID = 111;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public uint houseId;
        public int modelId;


        public HouseInformations()
        {
        }

        public HouseInformations(uint houseId, int modelId)
        {
            this.houseId = houseId;
            this.modelId = modelId;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarInt(houseId);
            writer.WriteVarShort((short)modelId);
        }

        public virtual void Deserialize(BigEndianReader reader)
        {
            houseId = reader.ReadVarUhInt();
            if (houseId < 0)
                throw new Exception("Forbidden value on houseId = " + houseId + ", it doesn't respect the following condition : houseId < 0");
            modelId = reader.ReadVarUhShort();
            if (modelId < 0)
                throw new Exception("Forbidden value on modelId = " + modelId + ", it doesn't respect the following condition : modelId < 0");
        }


    }


}