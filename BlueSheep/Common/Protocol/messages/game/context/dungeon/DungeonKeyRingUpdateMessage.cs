









// Generated on 12/11/2014 19:01:27
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class DungeonKeyRingUpdateMessage : Message
    {
        public new const uint ID =6296;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int dungeonId;
        public bool available;
        
        public DungeonKeyRingUpdateMessage()
        {
        }
        
        public DungeonKeyRingUpdateMessage(int dungeonId, bool available)
        {
            this.dungeonId = dungeonId;
            this.available = available;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)dungeonId);
            writer.WriteBoolean(available);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            dungeonId = reader.ReadVarUhShort();
            if (dungeonId < 0)
                throw new Exception("Forbidden value on dungeonId = " + dungeonId + ", it doesn't respect the following condition : dungeonId < 0");
            available = reader.ReadBoolean();
        }
        
    }
    
}