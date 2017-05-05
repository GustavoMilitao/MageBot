using BlueSheep.Common.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueSheep.Common.Protocol.Types
{
    public class StatisticDataShort : StatisticData
    {
        public new const int ID = 488;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public int value;


        public StatisticDataShort()
        {
        }

        public StatisticDataShort(int value)
        {
            this.value = value;

        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteShort((short)value);



        }

        public virtual void Deserialize(BigEndianReader reader)
        {
            value = reader.ReadShort();
        }
    }
}
