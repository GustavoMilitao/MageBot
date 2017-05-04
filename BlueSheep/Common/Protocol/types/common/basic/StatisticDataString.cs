using BlueSheep.Common.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueSheep.Common.Protocol.Types
{
    public class StatisticDataString : StatisticData
    {
        public new const short ID = 487;
public virtual short TypeId
{
    get { return ID; }
}

public string value;
        

public StatisticDataString()
{
}

public StatisticDataString(string s)
        {
            value = s;

        }
        

public virtual void Serialize(BigEndianWriter writer)
{

    writer.WriteUTF(value);

            

}

public virtual void Deserialize(BigEndianReader reader)
{
            value = reader.ReadUTF();
}

    }
}
