


















// Generated on 12/11/2014 19:02:02
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class FightTemporaryBoostEffect : AbstractFightDispellableEffect
{

public new const int ID = 209;
public override int TypeId
{
    get { return ID; }
}

public int delta;
        

public FightTemporaryBoostEffect()
{
}

public FightTemporaryBoostEffect(int uid, ulong targetId, int turnDuration, sbyte dispelable, int spellId, int effectId, int parentBoostUid, int delta)
         : base(uid, targetId, turnDuration, dispelable, spellId, effectId, parentBoostUid)
        {
            this.delta = delta;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteShort((short)delta);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            delta = reader.ReadShort();
            

}


}


}