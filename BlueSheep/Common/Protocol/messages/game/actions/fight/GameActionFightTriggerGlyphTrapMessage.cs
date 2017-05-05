









// Generated on 12/11/2014 19:01:19
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameActionFightTriggerGlyphTrapMessage : AbstractGameActionMessage
    {
        public new const uint ID =5741;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int markId;
        public int triggeringCharacterId;
        public int triggeredSpellId;
        
        public GameActionFightTriggerGlyphTrapMessage()
        {
        }
        
        public GameActionFightTriggerGlyphTrapMessage(int actionId, ulong sourceId, int markId, int triggeringCharacterId, int triggeredSpellId)
         : base(actionId, sourceId)
        {
            this.markId = markId;
            this.triggeringCharacterId = triggeringCharacterId;
            this.triggeredSpellId = triggeredSpellId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)markId);
            writer.WriteInt(triggeringCharacterId);
            writer.WriteVarShort((short)triggeredSpellId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            markId = reader.ReadShort();
            triggeringCharacterId = reader.ReadInt();
            triggeredSpellId = reader.ReadVarUhShort();
            if (triggeredSpellId < 0)
                throw new Exception("Forbidden value on triggeredSpellId = " + triggeredSpellId + ", it doesn't respect the following condition : triggeredSpellId < 0");
        }
        
    }
    
}