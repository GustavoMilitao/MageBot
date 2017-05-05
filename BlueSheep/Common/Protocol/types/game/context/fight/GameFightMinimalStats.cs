


















// Generated on 12/11/2014 19:02:05
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class GameFightMinimalStats
{

public new const int ID = 31;
public virtual int TypeId
{
    get { return ID; }
}

public int lifePoints;
        public int maxLifePoints;
        public int baseMaxLifePoints;
        public int permanentDamagePercent;
        public int shieldPoints;
        public int actionPoints;
        public int maxActionPoints;
        public int movementPoints;
        public int maxMovementPoints;
        public int summoner;
        public bool summoned;
        public int neutralElementResistPercent;
        public int earthElementResistPercent;
        public int waterElementResistPercent;
        public int airElementResistPercent;
        public int fireElementResistPercent;
        public int neutralElementReduction;
        public int earthElementReduction;
        public int waterElementReduction;
        public int airElementReduction;
        public int fireElementReduction;
        public int criticalDamageFixedResist;
        public int pushDamageFixedResist;
        public int dodgePALostProbability;
        public int dodgePMLostProbability;
        public int tackleBlock;
        public int tackleEvade;
        public sbyte invisibilityState;
        

public GameFightMinimalStats()
{
}

public GameFightMinimalStats(int lifePoints, int maxLifePoints, int baseMaxLifePoints, int permanentDamagePercent, int shieldPoints, int actionPoints, int maxActionPoints, int movementPoints, int maxMovementPoints, int summoner, bool summoned, int neutralElementResistPercent, int earthElementResistPercent, int waterElementResistPercent, int airElementResistPercent, int fireElementResistPercent, int neutralElementReduction, int earthElementReduction, int waterElementReduction, int airElementReduction, int fireElementReduction, int criticalDamageFixedResist, int pushDamageFixedResist, int dodgePALostProbability, int dodgePMLostProbability, int tackleBlock, int tackleEvade, sbyte invisibilityState)
        {
            this.lifePoints = lifePoints;
            this.maxLifePoints = maxLifePoints;
            this.baseMaxLifePoints = baseMaxLifePoints;
            this.permanentDamagePercent = permanentDamagePercent;
            this.shieldPoints = shieldPoints;
            this.actionPoints = actionPoints;
            this.maxActionPoints = maxActionPoints;
            this.movementPoints = movementPoints;
            this.maxMovementPoints = maxMovementPoints;
            this.summoner = summoner;
            this.summoned = summoned;
            this.neutralElementResistPercent = neutralElementResistPercent;
            this.earthElementResistPercent = earthElementResistPercent;
            this.waterElementResistPercent = waterElementResistPercent;
            this.airElementResistPercent = airElementResistPercent;
            this.fireElementResistPercent = fireElementResistPercent;
            this.neutralElementReduction = neutralElementReduction;
            this.earthElementReduction = earthElementReduction;
            this.waterElementReduction = waterElementReduction;
            this.airElementReduction = airElementReduction;
            this.fireElementReduction = fireElementReduction;
            this.criticalDamageFixedResist = criticalDamageFixedResist;
            this.pushDamageFixedResist = pushDamageFixedResist;
            this.dodgePALostProbability = dodgePALostProbability;
            this.dodgePMLostProbability = dodgePMLostProbability;
            this.tackleBlock = tackleBlock;
            this.tackleEvade = tackleEvade;
            this.invisibilityState = invisibilityState;
        }
        

public virtual void Serialize(BigEndianWriter writer)
{

writer.WriteVarInt(lifePoints);
            writer.WriteVarInt(maxLifePoints);
            writer.WriteVarInt(baseMaxLifePoints);
            writer.WriteVarInt(permanentDamagePercent);
            writer.WriteVarInt(shieldPoints);
            writer.WriteVarShort((short)actionPoints);
            writer.WriteVarShort((short)maxActionPoints);
            writer.WriteVarShort((short)movementPoints);
            writer.WriteVarShort((short)maxMovementPoints);
            writer.WriteInt(summoner);
            writer.WriteBoolean(summoned);
            writer.WriteVarShort((short)neutralElementResistPercent);
            writer.WriteVarShort((short)earthElementResistPercent);
            writer.WriteVarShort((short)waterElementResistPercent);
            writer.WriteVarShort((short)airElementResistPercent);
            writer.WriteVarShort((short)fireElementResistPercent);
            writer.WriteVarShort((short)neutralElementReduction);
            writer.WriteVarShort((short)earthElementReduction);
            writer.WriteVarShort((short)waterElementReduction);
            writer.WriteVarShort((short)airElementReduction);
            writer.WriteVarShort((short)fireElementReduction);
            writer.WriteVarShort((short)criticalDamageFixedResist);
            writer.WriteVarShort((short)pushDamageFixedResist);
            writer.WriteVarShort((short)dodgePALostProbability);
            writer.WriteVarShort((short)dodgePMLostProbability);
            writer.WriteVarShort((short)tackleBlock);
            writer.WriteVarShort((short)tackleEvade);
            writer.WriteSByte(invisibilityState);
            

}

public virtual void Deserialize(BigEndianReader reader)
{

lifePoints = reader.ReadVarInt();
            if (lifePoints < 0)
                throw new Exception("Forbidden value on lifePoints = " + lifePoints + ", it doesn't respect the following condition : lifePoints < 0");
            maxLifePoints = reader.ReadVarInt();
            if (maxLifePoints < 0)
                throw new Exception("Forbidden value on maxLifePoints = " + maxLifePoints + ", it doesn't respect the following condition : maxLifePoints < 0");
            baseMaxLifePoints = reader.ReadVarInt();
            if (baseMaxLifePoints < 0)
                throw new Exception("Forbidden value on baseMaxLifePoints = " + baseMaxLifePoints + ", it doesn't respect the following condition : baseMaxLifePoints < 0");
            permanentDamagePercent = reader.ReadVarInt();
            if (permanentDamagePercent < 0)
                throw new Exception("Forbidden value on permanentDamagePercent = " + permanentDamagePercent + ", it doesn't respect the following condition : permanentDamagePercent < 0");
            shieldPoints = reader.ReadVarInt();
            if (shieldPoints < 0)
                throw new Exception("Forbidden value on shieldPoints = " + shieldPoints + ", it doesn't respect the following condition : shieldPoints < 0");
            actionPoints = reader.ReadVarUhShort();
            maxActionPoints = reader.ReadVarUhShort();
            movementPoints = reader.ReadVarUhShort();
            maxMovementPoints = reader.ReadVarUhShort();
            summoner = reader.ReadInt();
            summoned = reader.ReadBoolean();
            neutralElementResistPercent = reader.ReadVarUhShort();
            earthElementResistPercent = reader.ReadVarUhShort();
            waterElementResistPercent = reader.ReadVarUhShort();
            airElementResistPercent = reader.ReadVarUhShort();
            fireElementResistPercent = reader.ReadVarUhShort();
            neutralElementReduction = reader.ReadVarUhShort();
            earthElementReduction = reader.ReadVarUhShort();
            waterElementReduction = reader.ReadVarUhShort();
            airElementReduction = reader.ReadVarUhShort();
            fireElementReduction = reader.ReadVarUhShort();
            criticalDamageFixedResist = reader.ReadVarUhShort();
            pushDamageFixedResist = reader.ReadVarUhShort();
            dodgePALostProbability = reader.ReadVarUhShort();
            if (dodgePALostProbability < 0)
                throw new Exception("Forbidden value on dodgePALostProbability = " + dodgePALostProbability + ", it doesn't respect the following condition : dodgePALostProbability < 0");
            dodgePMLostProbability = reader.ReadVarUhShort();
            if (dodgePMLostProbability < 0)
                throw new Exception("Forbidden value on dodgePMLostProbability = " + dodgePMLostProbability + ", it doesn't respect the following condition : dodgePMLostProbability < 0");
            tackleBlock = reader.ReadVarUhShort();
            tackleEvade = reader.ReadVarUhShort();
            invisibilityState = reader.ReadSByte();
            if (invisibilityState < 0)
                throw new Exception("Forbidden value on invisibilityState = " + invisibilityState + ", it doesn't respect the following condition : invisibilityState < 0");
            

}


}


}