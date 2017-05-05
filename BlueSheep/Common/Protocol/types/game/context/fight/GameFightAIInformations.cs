


















// Generated on 12/11/2014 19:02:05
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class GameFightAIInformations : GameFightFighterInformations
{

public new const int ID = 151;
public override int TypeId
{
    get { return ID; }
}



public GameFightAIInformations()
{
}

public GameFightAIInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, Types.GameFightMinimalStats stats, int[] previousPositions)
         : base(contextualId, look, disposition, teamId, wave, alive, stats, previousPositions)
        {
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            

}


}


}