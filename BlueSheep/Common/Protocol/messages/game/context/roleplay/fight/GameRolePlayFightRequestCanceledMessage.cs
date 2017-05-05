









// Generated on 12/11/2014 19:01:32
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameRolePlayFightRequestCanceledMessage : Message
    {
        public new const uint ID =5822;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int fightId;
        public ulong sourceId;
        public ulong targetId;
        
        public GameRolePlayFightRequestCanceledMessage()
        {
        }
        
        public GameRolePlayFightRequestCanceledMessage(int fightId, ulong sourceId, ulong targetId)
        {
            this.fightId = fightId;
            this.sourceId = sourceId;
            this.targetId = targetId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(fightId);
            writer.WriteVarLong(sourceId);
            writer.WriteULong(targetId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            fightId = reader.ReadInt();
            sourceId = reader.ReadVarUhLong();
            if (sourceId < 0)
                throw new Exception("Forbidden value on sourceId = " + sourceId + ", it doesn't respect the following condition : sourceId < 0");
            targetId = reader.ReadULong();
        }
        
    }
    
}