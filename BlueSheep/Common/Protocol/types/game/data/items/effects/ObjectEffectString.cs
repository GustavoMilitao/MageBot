


















// Generated on 12/11/2014 19:02:09
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class ObjectEffectString : ObjectEffect
{

public new const int ID = 74;
public override int TypeId
{
    get { return ID; }
}

public string value;
        

public ObjectEffectString()
{
}

public ObjectEffectString(int actionId, string value)
         : base(actionId)
        {
            this.value = value;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteUTF(value);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            value = reader.ReadUTF();
            

}


}


}