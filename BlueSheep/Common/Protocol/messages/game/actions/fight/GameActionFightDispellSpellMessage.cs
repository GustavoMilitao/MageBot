









// Generated on 12/11/2014 19:01:17
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameActionFightDispellSpellMessage : GameActionFightDispellMessage
    {
        public new const uint ID =6176;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int spellId;
        
        public GameActionFightDispellSpellMessage()
        {
        }
        
        public GameActionFightDispellSpellMessage(int actionId, ulong sourceId, ulong targetId, int spellId)
         : base(actionId, sourceId, targetId)
        {
            this.spellId = spellId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarShort((short)spellId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            spellId = reader.ReadVarUhShort();
            if (spellId < 0)
                throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
        }
        
    }
    
}