


















// Generated on 12/11/2014 19:02:11
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class PrismSubareaEmptyInfo
    {

        public new const int ID = 438;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public int subAreaId;
        public int allianceId;


        public PrismSubareaEmptyInfo()
        {
        }

        public PrismSubareaEmptyInfo(int subAreaId, int allianceId)
        {
            this.subAreaId = subAreaId;
            this.allianceId = allianceId;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteVarShort((short)subAreaId);
            writer.WriteVarInt(allianceId);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            subAreaId = reader.ReadVarUhShort();
            if (subAreaId < 0)
                throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
            allianceId = reader.ReadVarInt();
            if (allianceId < 0)
                throw new Exception("Forbidden value on allianceId = " + allianceId + ", it doesn't respect the following condition : allianceId < 0");


        }


    }


}