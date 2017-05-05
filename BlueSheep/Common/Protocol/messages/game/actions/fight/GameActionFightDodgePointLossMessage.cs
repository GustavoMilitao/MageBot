









// Generated on 12/11/2014 19:01:17
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameActionFightDodgePointLossMessage : AbstractGameActionMessage
    {
        public new const uint ID =5828;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public ulong targetId;
        public int amount;
        
        public GameActionFightDodgePointLossMessage()
        {
        }
        
        public GameActionFightDodgePointLossMessage(int actionId, ulong sourceId, ulong targetId, int amount)
         : base(actionId, sourceId)
        {
            this.targetId = targetId;
            this.amount = amount;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteULong(targetId);
            writer.WriteVarShort((short)amount);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            targetId = reader.ReadULong();
            amount = reader.ReadVarUhShort();
            if (amount < 0)
                throw new Exception("Forbidden value on amount = " + amount + ", it doesn't respect the following condition : amount < 0");
        }
        
    }
    
}