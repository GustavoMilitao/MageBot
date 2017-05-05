









// Generated on 12/11/2014 19:01:41
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class QuestStepStartedMessage : Message
    {
        public new const uint ID =6096;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int questId;
        public int stepId;
        
        public QuestStepStartedMessage()
        {
        }
        
        public QuestStepStartedMessage(int questId, int stepId)
        {
            this.questId = questId;
            this.stepId = stepId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)questId);
            writer.WriteVarShort((short)stepId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            questId = reader.ReadVarUhShort();
            if (questId < 0)
                throw new Exception("Forbidden value on questId = " + questId + ", it doesn't respect the following condition : questId < 0");
            stepId = reader.ReadVarUhShort();
            if (stepId < 0)
                throw new Exception("Forbidden value on stepId = " + stepId + ", it doesn't respect the following condition : stepId < 0");
        }
        
    }
    
}