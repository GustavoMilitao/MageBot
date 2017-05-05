


















// Generated on 12/11/2014 19:02:11
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class PaddockContentInformations : PaddockInformations
    {

        public new const int ID = 183;
        public override int TypeId
        {
            get { return ID; }
        }

        public int paddockId;
        public int worldX;
        public int worldY;
        public int mapId;
        public int subAreaId;
        public bool abandonned;
        public Types.MountInformationsForPaddock[] mountsInformations;


        public PaddockContentInformations()
        {
        }

        public PaddockContentInformations(int maxOutdoorMount, int maxItems, int paddockId, int worldX, int worldY, int mapId, int subAreaId, bool abandonned, Types.MountInformationsForPaddock[] mountsInformations)
                 : base(maxOutdoorMount, maxItems)
        {
            this.paddockId = paddockId;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
            this.abandonned = abandonned;
            this.mountsInformations = mountsInformations;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteInt(paddockId);
            writer.WriteShort((short)worldX);
            writer.WriteShort((short)worldY);
            writer.WriteInt(mapId);
            writer.WriteVarShort((short)subAreaId);
            writer.WriteBoolean(abandonned);
            writer.WriteUShort((ushort)mountsInformations.Length);
            foreach (var entry in mountsInformations)
            {
                entry.Serialize(writer);
            }


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            paddockId = reader.ReadInt();
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
            abandonned = reader.ReadBoolean();
            var limit = reader.ReadUShort();
            mountsInformations = new Types.MountInformationsForPaddock[limit];
            for (int i = 0; i < limit; i++)
            {
                mountsInformations[i] = new Types.MountInformationsForPaddock();
                mountsInformations[i].Deserialize(reader);
            }


        }


    }


}