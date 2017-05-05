


















// Generated on 12/11/2014 19:02:02
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class AbstractFightDispellableEffect
{

public new const int ID = 206;
public virtual int TypeId
{
    get { return ID; }
}

public int uid;
        public ulong targetId;
        public int turnDuration;
        public sbyte dispelable;
        public int spellId;
        public int effectId;
        public int parentBoostUid;
        

public AbstractFightDispellableEffect()
{
}

public AbstractFightDispellableEffect(int uid, ulong targetId, int turnDuration, sbyte dispelable, int spellId, int effectId, int parentBoostUid)
        {
            this.uid = uid;
            this.targetId = targetId;
            this.turnDuration = turnDuration;
            this.dispelable = dispelable;
            this.spellId = spellId;
            this.effectId = effectId;
            this.parentBoostUid = parentBoostUid;
        }
        

public virtual void Serialize(BigEndianWriter writer)
{

writer.WriteVarInt(uid);
            writer.WriteULong(targetId);
            writer.WriteShort((short)turnDuration);
            writer.WriteSByte(dispelable);
            writer.WriteVarShort((short)spellId);
            writer.WriteVarInt(effectId);
            writer.WriteVarInt(parentBoostUid);
            

}

public virtual void Deserialize(BigEndianReader reader)
{

uid = reader.ReadVarInt();
            if (uid < 0)
                throw new Exception("Forbidden value on uid = " + uid + ", it doesn't respect the following condition : uid < 0");
            targetId = reader.ReadULong();
            turnDuration = reader.ReadShort();
            dispelable = reader.ReadSByte();
            if (dispelable < 0)
                throw new Exception("Forbidden value on dispelable = " + dispelable + ", it doesn't respect the following condition : dispelable < 0");
            spellId = reader.ReadVarUhShort();
            if (spellId < 0)
                throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
            effectId = reader.ReadVarInt();
            if (effectId < 0)
                throw new Exception("Forbidden value on effectId = " + effectId + ", it doesn't respect the following condition : effectId < 0");
            parentBoostUid = reader.ReadVarInt();
            if (parentBoostUid < 0)
                throw new Exception("Forbidden value on parentBoostUid = " + parentBoostUid + ", it doesn't respect the following condition : parentBoostUid < 0");
            

}


}


}