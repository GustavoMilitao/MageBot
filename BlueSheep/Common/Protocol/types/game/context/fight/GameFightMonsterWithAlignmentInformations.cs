


















// Generated on 12/11/2014 19:02:05
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class GameFightMonsterWithAlignmentInformations : GameFightMonsterInformations
{

public new const int ID = 203;
public override int TypeId
{
    get { return ID; }
}

public Types.ActorAlignmentInformations alignmentInfos;
        

public GameFightMonsterWithAlignmentInformations()
{
}

public GameFightMonsterWithAlignmentInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, Types.GameFightMinimalStats stats, int[] previousPositions, int creatureGenericId, sbyte creatureGrade, Types.ActorAlignmentInformations alignmentInfos)
         : base(contextualId, look, disposition, teamId, wave, alive, stats, previousPositions, creatureGenericId, creatureGrade)
        {
            this.alignmentInfos = alignmentInfos;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            alignmentInfos.Serialize(writer);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            alignmentInfos = new Types.ActorAlignmentInformations();
            alignmentInfos.Deserialize(reader);
            

}


}


}