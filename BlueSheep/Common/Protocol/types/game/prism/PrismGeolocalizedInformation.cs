


















// Generated on 12/11/2014 19:02:11
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class PrismGeolocalizedInformation : PrismSubareaEmptyInfo
{

public new const int ID = 434;
public override int TypeId
{
    get { return ID; }
}

public int worldX;
        public int worldY;
        public int mapId;
        public Types.PrismInformation prism;
        

public PrismGeolocalizedInformation()
{
}

public PrismGeolocalizedInformation(int subAreaId, int allianceId, int worldX, int worldY, int mapId, Types.PrismInformation prism)
         : base(subAreaId, allianceId)
        {
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.prism = prism;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteShort((short)worldX);
            writer.WriteShort((short)worldY);
            writer.WriteInt(mapId);
            writer.WriteShort((short)prism.TypeId);
            prism.Serialize(writer);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            worldX = reader.ReadShort();
            if (worldX < -255 || worldX > 255)
                throw new Exception("Forbidden value on worldX = " + worldX + ", it doesn't respect the following condition : worldX < -255 || worldX > 255");
            worldY = reader.ReadShort();
            if (worldY < -255 || worldY > 255)
                throw new Exception("Forbidden value on worldY = " + worldY + ", it doesn't respect the following condition : worldY < -255 || worldY > 255");
            mapId = reader.ReadInt();
            prism = Types.ProtocolTypeManager.GetInstance<Types.PrismInformation>(reader.ReadUShort());
            prism.Deserialize(reader);
            

}


}


}