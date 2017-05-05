


















// Generated on 12/11/2014 19:02:09
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class ObjectEffectDice : ObjectEffect
{

public new const int ID = 73;
public override int TypeId
{
    get { return ID; }
}

public int diceNum;
        public int diceSide;
        public int diceConst;
        

public ObjectEffectDice()
{
}

public ObjectEffectDice(int actionId, int diceNum, int diceSide, int diceConst)
         : base(actionId)
        {
            this.diceNum = diceNum;
            this.diceSide = diceSide;
            this.diceConst = diceConst;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteVarShort((short)diceNum);
            writer.WriteVarShort((short)diceSide);
            writer.WriteVarShort((short)diceConst);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            diceNum = reader.ReadVarUhShort();
            if (diceNum < 0)
                throw new Exception("Forbidden value on diceNum = " + diceNum + ", it doesn't respect the following condition : diceNum < 0");
            diceSide = reader.ReadVarUhShort();
            if (diceSide < 0)
                throw new Exception("Forbidden value on diceSide = " + diceSide + ", it doesn't respect the following condition : diceSide < 0");
            diceConst = reader.ReadVarUhShort();
            if (diceConst < 0)
                throw new Exception("Forbidden value on diceConst = " + diceConst + ", it doesn't respect the following condition : diceConst < 0");
            

}


}


}