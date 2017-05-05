


















// Generated on 12/11/2014 19:02:08
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class TreasureHuntStepFollowDirectionToHint : TreasureHuntStep
{

public new const int ID = 472;
public override int TypeId
{
    get { return ID; }
}

public sbyte direction;
        public int npcId;
        

public TreasureHuntStepFollowDirectionToHint()
{
}

public TreasureHuntStepFollowDirectionToHint(sbyte direction, int npcId)
        {
            this.direction = direction;
            this.npcId = npcId;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteSByte(direction);
            writer.WriteVarShort((short)npcId);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            direction = reader.ReadSByte();
            if (direction < 0)
                throw new Exception("Forbidden value on direction = " + direction + ", it doesn't respect the following condition : direction < 0");
            npcId = reader.ReadVarUhShort();
            if (npcId < 0)
                throw new Exception("Forbidden value on npcId = " + npcId + ", it doesn't respect the following condition : npcId < 0");
            

}


}


}