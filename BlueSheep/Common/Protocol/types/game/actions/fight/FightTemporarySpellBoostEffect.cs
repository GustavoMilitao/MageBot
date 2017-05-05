


















// Generated on 12/11/2014 19:02:02
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class FightTemporarySpellBoostEffect : FightTemporaryBoostEffect
{

public new const int ID = 207;
public override int TypeId
{
    get { return ID; }
}

public int boostedSpellId;
        

public FightTemporarySpellBoostEffect()
{
}

public FightTemporarySpellBoostEffect(int uid, ulong targetId, int turnDuration, sbyte dispelable, int spellId, int effectId, int parentBoostUid, int delta, int boostedSpellId)
         : base(uid, targetId, turnDuration, dispelable, spellId, effectId, parentBoostUid, delta)
        {
            this.boostedSpellId = boostedSpellId;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteVarShort((short)boostedSpellId);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            boostedSpellId = reader.ReadVarUhShort();
            if (boostedSpellId < 0)
                throw new Exception("Forbidden value on boostedSpellId = " + boostedSpellId + ", it doesn't respect the following condition : boostedSpellId < 0");
            

}


}


}