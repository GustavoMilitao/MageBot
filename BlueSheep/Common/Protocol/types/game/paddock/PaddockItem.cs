


















// Generated on 12/11/2014 19:02:11
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class PaddockItem : ObjectItemInRolePlay
{

public new const int ID = 185;
public override int TypeId
{
    get { return ID; }
}

public Types.ItemDurability durability;
        

public PaddockItem()
{
}

public PaddockItem(int cellId, int objectGID, Types.ItemDurability durability)
         : base(cellId, objectGID)
        {
            this.durability = durability;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            durability.Serialize(writer);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            durability = new Types.ItemDurability();
            durability.Deserialize(reader);
            

}


}


}