


















// Generated on 12/11/2014 19:02:11
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class PrismInformation
    {

        public new const int ID = 428;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public byte typeId;
        public byte state;
        public int nextVulnerabilityDate;
        public int placementDate;
        public uint rewardTokenCount;


        public PrismInformation()
        {
        }

        public PrismInformation(byte typeId, byte state, int nextVulnerabilityDate, int placementDate, uint rewardTokenCount)
        {
            this.typeId = typeId;
            this.state = state;
            this.nextVulnerabilityDate = nextVulnerabilityDate;
            this.placementDate = placementDate;
            this.rewardTokenCount = rewardTokenCount;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteByte(typeId);
            writer.WriteByte(state);
            writer.WriteInt(nextVulnerabilityDate);
            writer.WriteInt(placementDate);
            writer.WriteVarInt(rewardTokenCount);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            typeId = reader.ReadByte();
            if (typeId < 0)
                throw new Exception("Forbidden value on typeId = " + typeId + ", it doesn't respect the following condition : typeId < 0");
            state = reader.ReadByte();
            if (state < 0)
                throw new Exception("Forbidden value on state = " + state + ", it doesn't respect the following condition : state < 0");
            nextVulnerabilityDate = reader.ReadInt();
            if (nextVulnerabilityDate < 0)
                throw new Exception("Forbidden value on nextVulnerabilityDate = " + nextVulnerabilityDate + ", it doesn't respect the following condition : nextVulnerabilityDate < 0");
            placementDate = reader.ReadInt();
            if (placementDate < 0)
                throw new Exception("Forbidden value on placementDate = " + placementDate + ", it doesn't respect the following condition : placementDate < 0");
            rewardTokenCount = reader.ReadVarUhInt();
            if (rewardTokenCount < 0)
                throw new Exception("Forbidden value on rewardTokenCount = " + rewardTokenCount + ", it doesn't respect the following condition : rewardTokenCount < 0");


        }


    }


}