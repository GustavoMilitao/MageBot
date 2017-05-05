


















// Generated on 12/11/2014 19:02:07
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class NamedPartyTeamWithOutcome
{

public new const int ID = 470;
public virtual int TypeId
{
    get { return ID; }
}

public Types.NamedPartyTeam team;
        public int outcome;
        

public NamedPartyTeamWithOutcome()
{
}

public NamedPartyTeamWithOutcome(Types.NamedPartyTeam team, int outcome)
        {
            this.team = team;
            this.outcome = outcome;
        }
        

public virtual void Serialize(BigEndianWriter writer)
{

team.Serialize(writer);
            writer.WriteVarShort((short)outcome);
            

}

public virtual void Deserialize(BigEndianReader reader)
{

team = new Types.NamedPartyTeam();
            team.Deserialize(reader);
            outcome = reader.ReadVarUhShort();
            if (outcome < 0)
                throw new Exception("Forbidden value on outcome = " + outcome + ", it doesn't respect the following condition : outcome < 0");
            

}


}


}