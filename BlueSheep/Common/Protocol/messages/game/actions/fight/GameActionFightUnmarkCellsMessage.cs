









// Generated on 12/11/2014 19:01:19
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameActionFightUnmarkCellsMessage : AbstractGameActionMessage
    {
        public new const uint ID =5570;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int markId;
        
        public GameActionFightUnmarkCellsMessage()
        {
        }
        
        public GameActionFightUnmarkCellsMessage(int actionId, ulong sourceId, int markId)
         : base(actionId, sourceId)
        {
            this.markId = markId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)markId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            markId = reader.ReadShort();
        }
        
    }
    
}