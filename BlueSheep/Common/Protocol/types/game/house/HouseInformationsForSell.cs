


















// Generated on 12/11/2014 19:02:10
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class HouseInformationsForSell
{

public new const int ID = 221;
public virtual int TypeId
{
    get { return ID; }
}

public int modelId;
        public string ownerName;
        public bool ownerConnected;
        public int worldX;
        public int worldY;
        public int subAreaId;
        public sbyte nbRoom;
        public sbyte nbChest;
        public int[] skillListIds;
        public bool isLocked;
        public int price;
        

public HouseInformationsForSell()
{
}

public HouseInformationsForSell(int modelId, string ownerName, bool ownerConnected, int worldX, int worldY, int subAreaId, sbyte nbRoom, sbyte nbChest, int[] skillListIds, bool isLocked, int price)
        {
            this.modelId = modelId;
            this.ownerName = ownerName;
            this.ownerConnected = ownerConnected;
            this.worldX = worldX;
            this.worldY = worldY;
            this.subAreaId = subAreaId;
            this.nbRoom = nbRoom;
            this.nbChest = nbChest;
            this.skillListIds = skillListIds;
            this.isLocked = isLocked;
            this.price = price;
        }
        

public virtual void Serialize(BigEndianWriter writer)
{

writer.WriteVarInt(modelId);
            writer.WriteUTF(ownerName);
            writer.WriteBoolean(ownerConnected);
            writer.WriteShort((short)worldX);
            writer.WriteShort((short)worldY);
            writer.WriteVarShort((short)subAreaId);
            writer.WriteSByte(nbRoom);
            writer.WriteSByte(nbChest);
            writer.WriteUShort((ushort)skillListIds.Length);
            foreach (var entry in skillListIds)
            {
                 writer.WriteInt(entry);
            }
            writer.WriteBoolean(isLocked);
            writer.WriteVarInt(price);
            

}

public virtual void Deserialize(BigEndianReader reader)
{

modelId = reader.ReadVarInt();
            if (modelId < 0)
                throw new Exception("Forbidden value on modelId = " + modelId + ", it doesn't respect the following condition : modelId < 0");
            ownerName = reader.ReadUTF();
            ownerConnected = reader.ReadBoolean();
            worldX = reader.ReadShort();
            if (worldX < -255 || worldX > 255)
                throw new Exception("Forbidden value on worldX = " + worldX + ", it doesn't respect the following condition : worldX < -255 || worldX > 255");
            worldY = reader.ReadShort();
            if (worldY < -255 || worldY > 255)
                throw new Exception("Forbidden value on worldY = " + worldY + ", it doesn't respect the following condition : worldY < -255 || worldY > 255");
            subAreaId = reader.ReadVarUhShort();
            if (subAreaId < 0)
                throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
            nbRoom = reader.ReadSByte();
            nbChest = reader.ReadSByte();
            var limit = reader.ReadUShort();
            skillListIds = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 skillListIds[i] = reader.ReadInt();
            }
            isLocked = reader.ReadBoolean();
            price = reader.ReadVarInt();
            if (price < 0)
                throw new Exception("Forbidden value on price = " + price + ", it doesn't respect the following condition : price < 0");
            

}


}


}