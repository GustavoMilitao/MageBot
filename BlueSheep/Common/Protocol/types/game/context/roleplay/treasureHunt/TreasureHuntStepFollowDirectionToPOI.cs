


















// Generated on 12/11/2014 19:02:08
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class TreasureHuntStepFollowDirectionToPOI : TreasureHuntStep
{

public new const int ID = 461;
public override int TypeId
{
    get { return ID; }
}

public sbyte direction;
        public int poiLabelId;
        

public TreasureHuntStepFollowDirectionToPOI()
{
}

public TreasureHuntStepFollowDirectionToPOI(sbyte direction, int poiLabelId)
        {
            this.direction = direction;
            this.poiLabelId = poiLabelId;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteSByte(direction);
            writer.WriteVarShort((short)poiLabelId);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            direction = reader.ReadSByte();
            if (direction < 0)
                throw new Exception("Forbidden value on direction = " + direction + ", it doesn't respect the following condition : direction < 0");
            poiLabelId = reader.ReadVarUhShort();
            if (poiLabelId < 0)
                throw new Exception("Forbidden value on poiLabelId = " + poiLabelId + ", it doesn't respect the following condition : poiLabelId < 0");
            

}


}


}