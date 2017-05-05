


















// Generated on 12/11/2014 19:02:10
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class EntityLook
    {

        public new const int ID = 55;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public int bonesId;
        public int[] skins;
        public int[] indexedColors;
        public int[] scales;
        public Types.SubEntity[] subentities;


        public EntityLook()
        {
        }

        public EntityLook(int bonesId, int[] skins, int[] indexedColors, int[] scales, Types.SubEntity[] subentities)
        {
            this.bonesId = bonesId;
            this.skins = skins;
            this.indexedColors = indexedColors;
            this.scales = scales;
            this.subentities = subentities;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteVarShort((short)bonesId);
            writer.WriteUShort((ushort)skins.Length);
            foreach (var entry in skins)
            {
                writer.WriteVarShort((short)entry);
            }
            writer.WriteUShort((ushort)indexedColors.Length);
            foreach (var entry in indexedColors)
            {
                writer.WriteInt(entry);
            }
            writer.WriteUShort((ushort)scales.Length);
            foreach (var entry in scales)
            {
                writer.WriteVarShort((short)entry);
            }
            writer.WriteUShort((ushort)subentities.Length);
            foreach (var entry in subentities)
            {
                entry.Serialize(writer);
            }


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            bonesId = reader.ReadVarUhShort();
            if (bonesId < 0)
                throw new Exception("Forbidden value on bonesId = " + bonesId + ", it doesn't respect the following condition : bonesId < 0");
            var limit = reader.ReadUShort();
            skins = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                skins[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            indexedColors = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                indexedColors[i] = reader.ReadInt();
            }
            limit = reader.ReadUShort();
            scales = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                scales[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            subentities = new Types.SubEntity[limit];
            for (int i = 0; i < limit; i++)
            {
                subentities[i] = new Types.SubEntity();
                subentities[i].Deserialize(reader);
            }


        }


    }


}