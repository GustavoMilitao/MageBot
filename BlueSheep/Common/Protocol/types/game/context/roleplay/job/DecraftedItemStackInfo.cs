using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.types.game.context.roleplay.job
{
    public class DecraftedItemStackInfo
    {
        public new const int ID = 481;
        public virtual int TypeId
        {
            get { return ID; }
        }
        public int objectUID;
        public float bonusMin;
        public float bonusMax;
        public List<int> runesId;
        public List<int> runesQty;

        public DecraftedItemStackInfo()
        {
            runesId = new List<int>();
            runesQty = new List<int>();
        }
        public DecraftedItemStackInfo(int objectUID, float bonusMin, float bonusMax, List<int> runesId, List<int> runesQty)
        {
            this.objectUID = objectUID;
            this.bonusMin = bonusMin;
            this.bonusMax = bonusMax;
            this.runesId = runesId;
            this.runesQty = runesQty;
        }

        public virtual void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarInt(objectUID);
            writer.WriteFloat(bonusMin);
            writer.WriteFloat(bonusMax);
            writer.WriteShort((short)(int)runesId.Count);
            for(int i=0;i<runesId.Count;i++)
            {
                writer.WriteVarShort((short)runesId[i]);
            }
            writer.WriteShort((short)(int)runesQty.Count);
            for(int i=0;i<runesQty.Count;i++)
            {
                writer.WriteVarInt(runesQty[i]);
            }
        }
        public virtual void Deserialize(BigEndianReader reader)
        {
            objectUID = reader.ReadVarInt();
            bonusMin = reader.ReadFloat();
            bonusMax = reader.ReadFloat();
            var limit = reader.ReadUShort();
            for(int i=0;i<limit;i++)
            {
                runesId.Add(reader.ReadVarUhShort());
            }
            var newLimit = reader.ReadUShort();
            for(int i=0;i<newLimit;i++)
            {
                runesQty.Add(reader.ReadVarInt());
            }
        }
    }
}
