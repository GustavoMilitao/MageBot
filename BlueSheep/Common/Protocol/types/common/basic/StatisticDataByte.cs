using BlueSheep.Common.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueSheep.Common.Protocol.Types
{
    public class StatisticDataByte : StatisticData
    {
        public new const int ID = 486;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public byte value;


        public StatisticDataByte()
        {
        }

        public StatisticDataByte(byte value)
        {
            this.value = value;

        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteByte(value);



        }

        public virtual void Deserialize(BigEndianReader reader)
        {
            value = reader.ReadByte();
        }
    }
}
