









// Generated on 12/11/2014 19:01:17
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameActionFightExchangePositionsMessage : AbstractGameActionMessage
    {
        public new const uint ID =5527;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public ulong targetId;
        public int casterCellId;
        public int targetCellId;
        
        public GameActionFightExchangePositionsMessage()
        {
        }
        
        public GameActionFightExchangePositionsMessage(int actionId, ulong sourceId, ulong targetId, int casterCellId, int targetCellId)
         : base(actionId, sourceId)
        {
            this.targetId = targetId;
            this.casterCellId = casterCellId;
            this.targetCellId = targetCellId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteULong(targetId);
            writer.WriteShort((short)casterCellId);
            writer.WriteShort((short)targetCellId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            targetId = reader.ReadULong();
            casterCellId = reader.ReadShort();
            if (casterCellId < -1 || casterCellId > 559)
                throw new Exception("Forbidden value on casterCellId = " + casterCellId + ", it doesn't respect the following condition : casterCellId < -1 || casterCellId > 559");
            targetCellId = reader.ReadShort();
            if (targetCellId < -1 || targetCellId > 559)
                throw new Exception("Forbidden value on targetCellId = " + targetCellId + ", it doesn't respect the following condition : targetCellId < -1 || targetCellId > 559");
        }
        
    }
    
}