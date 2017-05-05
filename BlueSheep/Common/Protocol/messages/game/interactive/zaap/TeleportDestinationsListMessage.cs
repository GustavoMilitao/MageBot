









// Generated on 12/11/2014 19:01:47
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class TeleportDestinationsListMessage : Message
    {
        public new const uint ID =5960;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte teleporterType;
        public int[] mapIds;
        public int[] subAreaIds;
        public int[] costs;
        public sbyte[] destTeleporterType;
        
        public TeleportDestinationsListMessage()
        {
        }
        
        public TeleportDestinationsListMessage(sbyte teleporterType, int[] mapIds, int[] subAreaIds, int[] costs, sbyte[] destTeleporterType)
        {
            this.teleporterType = teleporterType;
            this.mapIds = mapIds;
            this.subAreaIds = subAreaIds;
            this.costs = costs;
            this.destTeleporterType = destTeleporterType;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(teleporterType);
            writer.WriteUShort((ushort)mapIds.Length);
            foreach (var entry in mapIds)
            {
                 writer.WriteInt(entry);
            }
            writer.WriteUShort((ushort)subAreaIds.Length);
            foreach (var entry in subAreaIds)
            {
                 writer.WriteVarShort((short)entry);
            }
            writer.WriteUShort((ushort)costs.Length);
            foreach (var entry in costs)
            {
                 writer.WriteVarShort((short)entry);
            }
            writer.WriteUShort((ushort)destTeleporterType.Length);
            foreach (var entry in destTeleporterType)
            {
                 writer.WriteSByte(entry);
            }
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            teleporterType = reader.ReadSByte();
            if (teleporterType < 0)
                throw new Exception("Forbidden value on teleporterType = " + teleporterType + ", it doesn't respect the following condition : teleporterType < 0");
            var limit = reader.ReadUShort();
            mapIds = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 mapIds[i] = reader.ReadInt();
            }
            limit = reader.ReadUShort();
            subAreaIds = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 subAreaIds[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            costs = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 costs[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            destTeleporterType = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                 destTeleporterType[i] = reader.ReadSByte();
            }
        }
        
    }
    
}