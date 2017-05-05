









// Generated on 12/11/2014 19:01:40
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class QuestObjectiveValidatedMessage : Message
    {
        public new const uint ID =6098;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int questId;
        public int objectiveId;
        
        public QuestObjectiveValidatedMessage()
        {
        }
        
        public QuestObjectiveValidatedMessage(int questId, int objectiveId)
        {
            this.questId = questId;
            this.objectiveId = objectiveId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)questId);
            writer.WriteVarShort((short)objectiveId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            questId = reader.ReadVarUhShort();
            if (questId < 0)
                throw new Exception("Forbidden value on questId = " + questId + ", it doesn't respect the following condition : questId < 0");
            objectiveId = reader.ReadVarUhShort();
            if (objectiveId < 0)
                throw new Exception("Forbidden value on objectiveId = " + objectiveId + ", it doesn't respect the following condition : objectiveId < 0");
        }
        
    }
    
}