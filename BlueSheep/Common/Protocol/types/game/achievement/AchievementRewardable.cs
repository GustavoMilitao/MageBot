


















// Generated on 12/11/2014 19:02:02
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class AchievementRewardable
    {

        public new const int ID = 412;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public ulong id;
        public byte finishedlevel;


        public AchievementRewardable()
        {
        }

        public AchievementRewardable(ulong id, byte finishedlevel)
        {
            this.id = id;
            this.finishedlevel = finishedlevel;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteVarShort((short)id);
            writer.WriteByte(finishedlevel);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            id = reader.ReadVarUhShort();
            if (id < 0)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0");
            finishedlevel = reader.ReadByte();
            if (finishedlevel < 0 || finishedlevel > 200)
                throw new Exception("Forbidden value on finishedlevel = " + finishedlevel + ", it doesn't respect the following condition : finishedlevel < 0 || finishedlevel > 200");


        }


    }


}