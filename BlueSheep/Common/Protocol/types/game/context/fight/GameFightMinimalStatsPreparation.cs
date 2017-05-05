


















// Generated on 12/11/2014 19:02:05
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class GameFightMinimalStatsPreparation : GameFightMinimalStats
{

public new const int ID = 360;
public override int TypeId
{
    get { return ID; }
}

public int initiative;
        

public GameFightMinimalStatsPreparation()
{
}

public GameFightMinimalStatsPreparation(int lifePoints, int maxLifePoints, int baseMaxLifePoints, int permanentDamagePercent, int shieldPoints, int actionPoints, int maxActionPoints, int movementPoints, int maxMovementPoints, int summoner, bool summoned, int neutralElementResistPercent, int earthElementResistPercent, int waterElementResistPercent, int airElementResistPercent, int fireElementResistPercent, int neutralElementReduction, int earthElementReduction, int waterElementReduction, int airElementReduction, int fireElementReduction, int criticalDamageFixedResist, int pushDamageFixedResist, int dodgePALostProbability, int dodgePMLostProbability, int tackleBlock, int tackleEvade, sbyte invisibilityState, int initiative)
         : base(lifePoints, maxLifePoints, baseMaxLifePoints, permanentDamagePercent, shieldPoints, actionPoints, maxActionPoints, movementPoints, maxMovementPoints, summoner, summoned, neutralElementResistPercent, earthElementResistPercent, waterElementResistPercent, airElementResistPercent, fireElementResistPercent, neutralElementReduction, earthElementReduction, waterElementReduction, airElementReduction, fireElementReduction, criticalDamageFixedResist, pushDamageFixedResist, dodgePALostProbability, dodgePMLostProbability, tackleBlock, tackleEvade, invisibilityState)
        {
            this.initiative = initiative;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteVarInt(initiative);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            initiative = reader.ReadVarInt();
            if (initiative < 0)
                throw new Exception("Forbidden value on initiative = " + initiative + ", it doesn't respect the following condition : initiative < 0");
            

}


}


}