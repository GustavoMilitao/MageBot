


















// Generated on 12/11/2014 19:02:07
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class HumanOptionObjectUse : HumanOption
    {

        public new const int ID = 449;
        public override int TypeId
        {
            get { return ID; }
        }

        public byte delayTypeId;
        public double delayEndTime;
        public int objectGID;


        public HumanOptionObjectUse()
        {
        }

        public HumanOptionObjectUse(byte delayTypeId, double delayEndTime, int objectGID)
        {
            this.delayTypeId = delayTypeId;
            this.delayEndTime = delayEndTime;
            this.objectGID = objectGID;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteByte(delayTypeId);
            writer.WriteDouble(delayEndTime);
            writer.WriteVarShort((short)objectGID);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            delayTypeId = reader.ReadByte();
            if (delayTypeId < 0)
                throw new Exception("Forbidden value on delayTypeId = " + delayTypeId + ", it doesn't respect the following condition : delayTypeId < 0");
            delayEndTime = reader.ReadDouble();
            if (delayEndTime < 0 || delayEndTime > 9.007199254740992E15)
                throw new Exception("Forbidden value on delayEndTime = " + delayEndTime + ", it doesn't respect the following condition : delayEndTime < 0 || delayEndTime > 9.007199254740992E15");
            objectGID = reader.ReadVarUhShort();
            if (objectGID < 0)
                throw new Exception("Forbidden value on objectGID = " + objectGID + ", it doesn't respect the following condition : objectGID < 0");


        }


    }


}