


















// Generated on 12/11/2014 19:02:02
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class FightTemporaryBoostWeaponDamagesEffect : FightTemporaryBoostEffect
{

public new const int ID = 211;
public override int TypeId
{
    get { return ID; }
}

public int weaponTypeId;
        

public FightTemporaryBoostWeaponDamagesEffect()
{
}

public FightTemporaryBoostWeaponDamagesEffect(int uid, ulong targetId, int turnDuration, sbyte dispelable, int spellId, int effectId, int parentBoostUid, int delta, int weaponTypeId)
         : base(uid, targetId, turnDuration, dispelable, spellId, effectId, parentBoostUid, delta)
        {
            this.weaponTypeId = weaponTypeId;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteShort((short)weaponTypeId);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            weaponTypeId = reader.ReadShort();
            

}


}


}