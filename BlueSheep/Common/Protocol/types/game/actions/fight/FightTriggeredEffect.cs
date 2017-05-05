


















// Generated on 12/11/2014 19:02:02
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class FightTriggeredEffect : AbstractFightDispellableEffect
{

public new const int ID = 210;
public override int TypeId
{
    get { return ID; }
}

public int arg1;
        public int arg2;
        public int arg3;
        public int delay;
        

public FightTriggeredEffect()
{
}

public FightTriggeredEffect(int uid, ulong targetId, int turnDuration, sbyte dispelable, int spellId, int effectId, int parentBoostUid, int arg1, int arg2, int arg3, int delay)
         : base(uid, targetId, turnDuration, dispelable, spellId, effectId, parentBoostUid)
        {
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
            this.delay = delay;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteInt(arg1);
            writer.WriteInt(arg2);
            writer.WriteInt(arg3);
            writer.WriteShort((short)delay);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            arg1 = reader.ReadInt();
            arg2 = reader.ReadInt();
            arg3 = reader.ReadInt();
            delay = reader.ReadShort();
            

}


}


}