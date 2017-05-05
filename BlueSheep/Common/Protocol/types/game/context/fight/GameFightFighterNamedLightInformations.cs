


















// Generated on 12/11/2014 19:02:05
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class GameFightFighterNamedLightInformations : GameFightFighterLightInformations
{

public new const int ID = 456;
public override int TypeId
{
    get { return ID; }
}

public string name;
        

public GameFightFighterNamedLightInformations()
{
}

public GameFightFighterNamedLightInformations(ulong id, sbyte wave, int level, sbyte breed, string name)
         : base(id, wave, level, breed)
        {
            this.name = name;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteUTF(name);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            name = reader.ReadUTF();
            

}


}


}