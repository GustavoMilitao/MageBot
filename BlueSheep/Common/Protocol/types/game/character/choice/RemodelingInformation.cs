


















// Generated on 12/11/2014 19:02:03
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class RemodelingInformation
{

public new const int ID = 480;
public virtual int TypeId
{
    get { return ID; }
}

public string name;
        public sbyte breed;
        public bool sex;
        public int cosmeticId;
        public int[] colors;
        

public RemodelingInformation()
{
}

public RemodelingInformation(string name, sbyte breed, bool sex, int cosmeticId, int[] colors)
        {
            this.name = name;
            this.breed = breed;
            this.sex = sex;
            this.cosmeticId = cosmeticId;
            this.colors = colors;
        }
        

public virtual void Serialize(BigEndianWriter writer)
{

writer.WriteUTF(name);
            writer.WriteSByte(breed);
            writer.WriteBoolean(sex);
            writer.WriteVarShort((short)cosmeticId);
            writer.WriteUShort((ushort)colors.Length);
            foreach (var entry in colors)
            {
                 writer.WriteInt(entry);
            }
            

}

public virtual void Deserialize(BigEndianReader reader)
{

name = reader.ReadUTF();
            breed = reader.ReadSByte();
            sex = reader.ReadBoolean();
            cosmeticId = reader.ReadVarUhShort();
            if (cosmeticId < 0)
                throw new Exception("Forbidden value on cosmeticId = " + cosmeticId + ", it doesn't respect the following condition : cosmeticId < 0");
            var limit = reader.ReadUShort();
            colors = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 colors[i] = reader.ReadInt();
            }
            

}


}


}