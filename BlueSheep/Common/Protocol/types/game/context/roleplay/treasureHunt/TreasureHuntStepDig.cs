


















// Generated on 12/11/2014 19:02:08
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class TreasureHuntStepDig : TreasureHuntStep
{

public new const int ID = 465;
public override int TypeId
{
    get { return ID; }
}



public TreasureHuntStepDig()
{
}



public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            

}


}


}