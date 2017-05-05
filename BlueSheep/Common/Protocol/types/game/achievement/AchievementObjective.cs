


















// Generated on 12/11/2014 19:02:02
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class AchievementObjective
    {

        public new const int ID = 404;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public ulong id;
        public int maxValue;


        public AchievementObjective()
        {
        }

        public AchievementObjective(ulong id, int maxValue)
        {
            this.id = id;
            this.maxValue = maxValue;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteVarLong(id);
            writer.WriteVarShort((short)maxValue);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            id = reader.ReadVarUhLong();
            if (id < 0)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0");
            maxValue = reader.ReadVarUhShort();
            if (maxValue < 0)
                throw new Exception("Forbidden value on maxValue = " + maxValue + ", it doesn't respect the following condition : maxValue < 0");


        }


    }


}