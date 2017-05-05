


















// Generated on 12/11/2014 19:02:09
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class ObjectEffectCreature : ObjectEffect
{

public new const int ID = 71;
public override int TypeId
{
    get { return ID; }
}

public int monsterFamilyId;
        

public ObjectEffectCreature()
{
}

public ObjectEffectCreature(int actionId, int monsterFamilyId)
         : base(actionId)
        {
            this.monsterFamilyId = monsterFamilyId;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteVarShort((short)monsterFamilyId);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            monsterFamilyId = reader.ReadVarUhShort();
            if (monsterFamilyId < 0)
                throw new Exception("Forbidden value on monsterFamilyId = " + monsterFamilyId + ", it doesn't respect the following condition : monsterFamilyId < 0");
            

}


}


}