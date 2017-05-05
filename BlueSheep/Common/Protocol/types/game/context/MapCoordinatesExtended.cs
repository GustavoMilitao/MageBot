


















// Generated on 12/11/2014 19:02:04
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class MapCoordinatesExtended : MapCoordinatesAndId
{

public new const int ID = 176;
public override int TypeId
{
    get { return ID; }
}

public int subAreaId;
        

public MapCoordinatesExtended()
{
}

public MapCoordinatesExtended(int worldX, int worldY, int mapId, int subAreaId)
         : base(worldX, worldY, mapId)
        {
            this.subAreaId = subAreaId;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteVarShort((short)subAreaId);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            subAreaId = reader.ReadVarUhShort();
            if (subAreaId < 0)
                throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
            

}


}


}