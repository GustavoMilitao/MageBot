









// Generated on 12/11/2014 19:01:47
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class TeleportRequestMessage : Message
    {
        public new const uint ID =5961;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte teleporterType;
        public int mapId;
        
        public TeleportRequestMessage()
        {
        }
        
        public TeleportRequestMessage(sbyte teleporterType, int mapId)
        {
            this.teleporterType = teleporterType;
            this.mapId = mapId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(teleporterType);
            writer.WriteInt(mapId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            teleporterType = reader.ReadSByte();
            if (teleporterType < 0)
                throw new Exception("Forbidden value on teleporterType = " + teleporterType + ", it doesn't respect the following condition : teleporterType < 0");
            mapId = reader.ReadInt();
            if (mapId < 0)
                throw new Exception("Forbidden value on mapId = " + mapId + ", it doesn't respect the following condition : mapId < 0");
        }
        
    }
    
}