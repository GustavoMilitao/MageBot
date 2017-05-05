


















// Generated on 12/11/2014 19:02:07
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class PartyMemberArenaInformations : PartyMemberInformations
{

public new const int ID = 391;
public override int TypeId
{
    get { return ID; }
}

public int rank;
        

public PartyMemberArenaInformations()
{
}

public PartyMemberArenaInformations(uint id, byte level, string name, Types.EntityLook entityLook, sbyte breed, bool sex, int lifePoints, int maxLifePoints, int prospecting, byte regenRate, int initiative, sbyte alignmentSide, int worldX, int worldY, int mapId, int subAreaId, Types.PlayerStatus status, Types.PartyCompanionMemberInformations[] companions, int rank)
         : base(id, level, name, entityLook, breed, sex, lifePoints, maxLifePoints, prospecting, regenRate, initiative, alignmentSide, worldX, worldY, mapId, subAreaId, status, companions)
        {
            this.rank = rank;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteVarShort((short)rank);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            rank = reader.ReadVarUhShort();
            if (rank < 0 || rank > 2300)
                throw new Exception("Forbidden value on rank = " + rank + ", it doesn't respect the following condition : rank < 0 || rank > 2300");
            

}


}


}