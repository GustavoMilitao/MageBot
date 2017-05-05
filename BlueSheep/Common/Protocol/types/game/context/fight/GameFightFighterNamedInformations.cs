


















// Generated on 12/11/2014 19:02:05
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class GameFightFighterNamedInformations : GameFightFighterInformations
{

public new const int ID = 158;
public override int TypeId
{
    get { return ID; }
}

public string name;
        public Types.PlayerStatus status;
        

public GameFightFighterNamedInformations()
{
}

public GameFightFighterNamedInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, Types.GameFightMinimalStats stats, int[] previousPositions, string name, Types.PlayerStatus status)
         : base(contextualId, look, disposition, teamId, wave, alive, stats, previousPositions)
        {
            this.name = name;
            this.status = status;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteUTF(name);
            status.Serialize(writer);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            name = reader.ReadUTF();
            status = new Types.PlayerStatus();
            status.Deserialize(reader);
            

}


}


}