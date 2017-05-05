









// Generated on 12/11/2014 19:01:31
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ChangeMapMessage : Message
    {
        public new const uint ID =221;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int mapId;
        
        public ChangeMapMessage()
        {
        }
        
        public ChangeMapMessage(int mapId)
        {
            this.mapId = mapId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(mapId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            mapId = reader.ReadInt();
            if (mapId < 0)
                throw new Exception("Forbidden value on mapId = " + mapId + ", it doesn't respect the following condition : mapId < 0");
        }
        
    }
    
}