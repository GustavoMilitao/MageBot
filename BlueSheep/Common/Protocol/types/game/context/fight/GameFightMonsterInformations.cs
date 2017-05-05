


















// Generated on 12/11/2014 19:02:05
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class GameFightMonsterInformations : GameFightAIInformations
{

public new const int ID = 29;
public override int TypeId
{
    get { return ID; }
}

public int creatureGenericId;
        public sbyte creatureGrade;
        

public GameFightMonsterInformations()
{
}

public GameFightMonsterInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, Types.GameFightMinimalStats stats, int[] previousPositions, int creatureGenericId, sbyte creatureGrade)
         : base(contextualId, look, disposition, teamId, wave, alive, stats, previousPositions)
        {
            this.creatureGenericId = creatureGenericId;
            this.creatureGrade = creatureGrade;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteVarShort((short)creatureGenericId);
            writer.WriteSByte(creatureGrade);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            creatureGenericId = reader.ReadVarUhShort();
            if (creatureGenericId < 0)
                throw new Exception("Forbidden value on creatureGenericId = " + creatureGenericId + ", it doesn't respect the following condition : creatureGenericId < 0");
            creatureGrade = reader.ReadSByte();
            if (creatureGrade < 0)
                throw new Exception("Forbidden value on creatureGrade = " + creatureGrade + ", it doesn't respect the following condition : creatureGrade < 0");
            

}


}


}