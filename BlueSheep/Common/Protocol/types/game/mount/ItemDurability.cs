


















// Generated on 12/11/2014 19:02:11
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class ItemDurability
{

public new const int ID = 168;
public virtual int TypeId
{
    get { return ID; }
}

public int durability;
        public int durabilityMax;
        

public ItemDurability()
{
}

public ItemDurability(int durability, int durabilityMax)
        {
            this.durability = durability;
            this.durabilityMax = durabilityMax;
        }
        

public virtual void Serialize(BigEndianWriter writer)
{

writer.WriteShort((short)durability);
            writer.WriteShort((short)durabilityMax);
            

}

public virtual void Deserialize(BigEndianReader reader)
{

durability = reader.ReadShort();
            durabilityMax = reader.ReadShort();
            

}


}


}