


















// Generated on 12/11/2014 19:02:08
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class PartyMemberGeoPosition
{

public new const int ID = 378;
public virtual int TypeId
{
    get { return ID; }
}

public int memberId;
        public int worldX;
        public int worldY;
        public int mapId;
        public int subAreaId;
        

public PartyMemberGeoPosition()
{
}

public PartyMemberGeoPosition(int memberId, int worldX, int worldY, int mapId, int subAreaId)
        {
            this.memberId = memberId;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
        }
        

public virtual void Serialize(BigEndianWriter writer)
{

writer.WriteInt(memberId);
            writer.WriteShort((short)worldX);
            writer.WriteShort((short)worldY);
            writer.WriteInt(mapId);
            writer.WriteVarShort((short)subAreaId);
            

}

public virtual void Deserialize(BigEndianReader reader)
{

memberId = reader.ReadInt();
            if (memberId < 0)
                throw new Exception("Forbidden value on memberId = " + memberId + ", it doesn't respect the following condition : memberId < 0");
            worldX = reader.ReadShort();
            if (worldX < -255 || worldX > 255)
                throw new Exception("Forbidden value on worldX = " + worldX + ", it doesn't respect the following condition : worldX < -255 || worldX > 255");
            worldY = reader.ReadShort();
            if (worldY < -255 || worldY > 255)
                throw new Exception("Forbidden value on worldY = " + worldY + ", it doesn't respect the following condition : worldY < -255 || worldY > 255");
            mapId = reader.ReadInt();
            subAreaId = reader.ReadVarUhShort();
            if (subAreaId < 0)
                throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
            

}


}


}