


















// Generated on 12/11/2014 19:02:02
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class FightDispellableEffectExtendedInformations
{

public new const int ID = 208;
public virtual int TypeId
{
    get { return ID; }
}

public int actionId;
        public ulong sourceId;
        public Types.AbstractFightDispellableEffect effect;
        

public FightDispellableEffectExtendedInformations()
{
}

public FightDispellableEffectExtendedInformations(int actionId, ulong sourceId, Types.AbstractFightDispellableEffect effect)
        {
            this.actionId = actionId;
            this.sourceId = sourceId;
            this.effect = effect;
        }
        

public virtual void Serialize(BigEndianWriter writer)
{

writer.WriteVarShort((short)actionId);
            writer.WriteULong(sourceId);
            writer.WriteShort((short)effect.TypeId);
            effect.Serialize(writer);
            

}

public virtual void Deserialize(BigEndianReader reader)
{

actionId = reader.ReadVarUhShort();
            if (actionId < 0)
                throw new Exception("Forbidden value on actionId = " + actionId + ", it doesn't respect the following condition : actionId < 0");
            sourceId = reader.ReadULong();
            effect = Types.ProtocolTypeManager.GetInstance<Types.AbstractFightDispellableEffect>(reader.ReadUShort());
            effect.Deserialize(reader);
            

}


}


}