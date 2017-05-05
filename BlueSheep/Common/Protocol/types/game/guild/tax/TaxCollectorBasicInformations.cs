


















// Generated on 12/11/2014 19:02:10
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class TaxCollectorBasicInformations
{

public new const int ID = 96;
public virtual int TypeId
{
    get { return ID; }
}

public int firstNameId;
        public int lastNameId;
        public int worldX;
        public int worldY;
        public int mapId;
        public int subAreaId;
        

public TaxCollectorBasicInformations()
{
}

public TaxCollectorBasicInformations(int firstNameId, int lastNameId, int worldX, int worldY, int mapId, int subAreaId)
        {
            this.firstNameId = firstNameId;
            this.lastNameId = lastNameId;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
        }
        

public virtual void Serialize(BigEndianWriter writer)
{

writer.WriteVarShort((short)firstNameId);
            writer.WriteVarShort((short)lastNameId);
            writer.WriteShort((short)worldX);
            writer.WriteShort((short)worldY);
            writer.WriteInt(mapId);
            writer.WriteVarShort((short)subAreaId);
            

}

public virtual void Deserialize(BigEndianReader reader)
{

firstNameId = reader.ReadVarUhShort();
            if (firstNameId < 0)
                throw new Exception("Forbidden value on firstNameId = " + firstNameId + ", it doesn't respect the following condition : firstNameId < 0");
            lastNameId = reader.ReadVarUhShort();
            if (lastNameId < 0)
                throw new Exception("Forbidden value on lastNameId = " + lastNameId + ", it doesn't respect the following condition : lastNameId < 0");
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