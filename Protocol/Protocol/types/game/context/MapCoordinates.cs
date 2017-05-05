


















// Generated on 12/11/2014 19:02:04
using System;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class MapCoordinates
{

public new const short ID = 174;
public virtual short TypeId
{
    get { return ID; }
}

public short worldX;
        public short worldY;
        

public MapCoordinates()
{
}

public MapCoordinates(short worldX, short worldY)
        {
            this.worldX = worldX;
            this.worldY = worldY;
        }
        

public virtual void Serialize(BigEndianWriter writer)
{

writer.WriteShort(worldX);
            writer.WriteShort(worldY);
            

}

public virtual void Deserialize(BigEndianReader reader)
{

worldX = reader.ReadShort();
            if (worldX < -255 || worldX > 255)
                throw new Exception("Forbidden value on worldX = " + worldX + ", it doesn't respect the following condition : worldX < -255 || worldX > 255");
            worldY = reader.ReadShort();
            if (worldY < -255 || worldY > 255)
                throw new Exception("Forbidden value on worldY = " + worldY + ", it doesn't respect the following condition : worldY < -255 || worldY > 255");
            

}


}


}