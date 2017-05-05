


















// Generated on 12/11/2014 19:02:08
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class PartyCompanionMemberInformations : PartyCompanionBaseInformations
{

public new const int ID = 452;
public override int TypeId
{
    get { return ID; }
}

public int initiative;
        public int lifePoints;
        public int maxLifePoints;
        public int prospecting;
        public byte regenRate;
        

public PartyCompanionMemberInformations()
{
}

public PartyCompanionMemberInformations(sbyte indexId, sbyte companionGenericId, Types.EntityLook entityLook, int initiative, int lifePoints, int maxLifePoints, int prospecting, byte regenRate)
         : base(indexId, companionGenericId, entityLook)
        {
            this.initiative = initiative;
            this.lifePoints = lifePoints;
            this.maxLifePoints = maxLifePoints;
            this.prospecting = prospecting;
            this.regenRate = regenRate;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteVarShort((short)initiative);
            writer.WriteVarInt(lifePoints);
            writer.WriteVarInt(maxLifePoints);
            writer.WriteVarShort((short)prospecting);
            writer.WriteByte(regenRate);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            initiative = reader.ReadVarUhShort();
            if (initiative < 0)
                throw new Exception("Forbidden value on initiative = " + initiative + ", it doesn't respect the following condition : initiative < 0");
            lifePoints = reader.ReadVarInt();
            if (lifePoints < 0)
                throw new Exception("Forbidden value on lifePoints = " + lifePoints + ", it doesn't respect the following condition : lifePoints < 0");
            maxLifePoints = reader.ReadVarInt();
            if (maxLifePoints < 0)
                throw new Exception("Forbidden value on maxLifePoints = " + maxLifePoints + ", it doesn't respect the following condition : maxLifePoints < 0");
            prospecting = reader.ReadVarUhShort();
            if (prospecting < 0)
                throw new Exception("Forbidden value on prospecting = " + prospecting + ", it doesn't respect the following condition : prospecting < 0");
            regenRate = reader.ReadByte();
            if (regenRate < 0 || regenRate > 255)
                throw new Exception("Forbidden value on regenRate = " + regenRate + ", it doesn't respect the following condition : regenRate < 0 || regenRate > 255");
            

}


}


}