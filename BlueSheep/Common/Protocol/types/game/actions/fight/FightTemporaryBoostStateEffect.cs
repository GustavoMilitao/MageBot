


















// Generated on 12/11/2014 19:02:02
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class FightTemporaryBoostStateEffect : FightTemporaryBoostEffect
{

public new const int ID = 214;
public override int TypeId
{
    get { return ID; }
}

public int stateId;
        

public FightTemporaryBoostStateEffect()
{
}

public FightTemporaryBoostStateEffect(int uid, ulong targetId, int turnDuration, sbyte dispelable, int spellId, int effectId, int parentBoostUid, int delta, int stateId)
         : base(uid, targetId, turnDuration, dispelable, spellId, effectId, parentBoostUid, delta)
        {
            this.stateId = stateId;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteShort((short)stateId);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            stateId = reader.ReadShort();
            

}


}


}