









// Generated on 12/11/2014 19:01:18
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameActionFightReflectSpellMessage : AbstractGameActionMessage
    {
        public new const uint ID =5531;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public ulong targetId;
        
        public GameActionFightReflectSpellMessage()
        {
        }
        
        public GameActionFightReflectSpellMessage(int actionId, ulong sourceId, ulong targetId)
         : base(actionId, sourceId)
        {
            this.targetId = targetId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteULong(targetId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            targetId = reader.ReadULong();
        }
        
    }
    
}