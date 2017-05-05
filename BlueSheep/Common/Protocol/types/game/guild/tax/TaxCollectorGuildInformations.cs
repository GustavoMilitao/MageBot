


















// Generated on 12/11/2014 19:02:10
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class TaxCollectorGuildInformations : TaxCollectorComplementaryInformations
{

public new const int ID = 446;
public override int TypeId
{
    get { return ID; }
}

public Types.BasicGuildInformations guild;
        

public TaxCollectorGuildInformations()
{
}

public TaxCollectorGuildInformations(Types.BasicGuildInformations guild)
        {
            this.guild = guild;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            guild.Serialize(writer);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            guild = new Types.BasicGuildInformations();
            guild.Deserialize(reader);
            

}


}


}