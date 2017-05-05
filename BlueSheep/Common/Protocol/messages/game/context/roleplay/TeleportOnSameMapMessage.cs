









// Generated on 12/11/2014 19:01:32
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class TeleportOnSameMapMessage : Message
    {
        public new const uint ID =6048;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public ulong targetId;
        public int cellId;
        
        public TeleportOnSameMapMessage()
        {
        }
        
        public TeleportOnSameMapMessage(ulong targetId, int cellId)
        {
            this.targetId = targetId;
            this.cellId = cellId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteULong(targetId);
            writer.WriteVarShort((short)cellId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            targetId = reader.ReadULong();
            cellId = reader.ReadVarUhShort();
            if (cellId < 0 || cellId > 559)
                throw new Exception("Forbidden value on cellId = " + cellId + ", it doesn't respect the following condition : cellId < 0 || cellId > 559");
        }
        
    }
    
}