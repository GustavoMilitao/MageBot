using BlueSheep.Common.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueSheep.Common.Protocol.Types
{
    public class StatisticDataBoolean : StatisticData
    {
        public new const int ID = 482;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public bool value;


        public StatisticDataBoolean()
        {
        }

        public StatisticDataBoolean(bool value)
        {
            this.value = value;

        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteBoolean(value);



        }

        public virtual void Deserialize(BigEndianReader reader)
        {
            value = reader.ReadBoolean();
        }
    }
}
