









// Generated on 12/11/2014 19:01:18
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameActionFightLifePointsLostMessage : AbstractGameActionMessage
    {
        public new const uint ID =6312;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public ulong targetId;
        public int loss;
        public int permanentDamages;
        
        public GameActionFightLifePointsLostMessage()
        {
        }
        
        public GameActionFightLifePointsLostMessage(int actionId, ulong sourceId, ulong targetId, int loss, int permanentDamages)
         : base(actionId, sourceId)
        {
            this.targetId = targetId;
            this.loss = loss;
            this.permanentDamages = permanentDamages;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteULong(targetId);
            writer.WriteVarShort((short)loss);
            writer.WriteVarShort((short)permanentDamages);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            targetId = reader.ReadULong();
            loss = reader.ReadVarUhShort();
            if (loss < 0)
                throw new Exception("Forbidden value on loss = " + loss + ", it doesn't respect the following condition : loss < 0");
            permanentDamages = reader.ReadVarUhShort();
            if (permanentDamages < 0)
                throw new Exception("Forbidden value on permanentDamages = " + permanentDamages + ", it doesn't respect the following condition : permanentDamages < 0");
        }
        
    }
    
}