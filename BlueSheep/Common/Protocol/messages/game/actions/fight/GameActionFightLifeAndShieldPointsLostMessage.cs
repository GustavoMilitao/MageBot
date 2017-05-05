









// Generated on 12/11/2014 19:01:17
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameActionFightLifeAndShieldPointsLostMessage : GameActionFightLifePointsLostMessage
    {
        public new const uint ID =6310;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int shieldLoss;
        
        public GameActionFightLifeAndShieldPointsLostMessage()
        {
        }
        
        public GameActionFightLifeAndShieldPointsLostMessage(int actionId, ulong sourceId, ulong targetId, int loss, int permanentDamages, int shieldLoss)
         : base(actionId, sourceId, targetId, loss, permanentDamages)
        {
            this.shieldLoss = shieldLoss;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarShort((short)shieldLoss);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            shieldLoss = reader.ReadVarUhShort();
            if (shieldLoss < 0)
                throw new Exception("Forbidden value on shieldLoss = " + shieldLoss + ", it doesn't respect the following condition : shieldLoss < 0");
        }
        
    }
    
}