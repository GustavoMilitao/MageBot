









// Generated on 12/11/2014 19:01:18
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameActionFightModifyEffectsDurationMessage : AbstractGameActionMessage
    {
        public new const uint ID =6304;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public ulong targetId;
        public int delta;
        
        public GameActionFightModifyEffectsDurationMessage()
        {
        }
        
        public GameActionFightModifyEffectsDurationMessage(int actionId, ulong sourceId, ulong targetId, int delta)
         : base(actionId, sourceId)
        {
            this.targetId = targetId;
            this.delta = delta;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteULong(targetId);
            writer.WriteShort((short)delta);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            targetId = reader.ReadULong();
            delta = reader.ReadShort();
        }
        
    }
    
}