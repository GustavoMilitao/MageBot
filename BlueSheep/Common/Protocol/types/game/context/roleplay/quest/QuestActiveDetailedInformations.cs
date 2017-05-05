


















// Generated on 12/11/2014 19:02:08
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class QuestActiveDetailedInformations : QuestActiveInformations
{

public new const int ID = 382;
public override int TypeId
{
    get { return ID; }
}

public int stepId;
        public Types.QuestObjectiveInformations[] objectives;
        

public QuestActiveDetailedInformations()
{
}

public QuestActiveDetailedInformations(int questId, int stepId, Types.QuestObjectiveInformations[] objectives)
         : base(questId)
        {
            this.stepId = stepId;
            this.objectives = objectives;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteVarShort((short)stepId);
            writer.WriteUShort((ushort)objectives.Length);
            foreach (var entry in objectives)
            {
                 writer.WriteShort((short)entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            stepId = reader.ReadVarUhShort();
            if (stepId < 0)
                throw new Exception("Forbidden value on stepId = " + stepId + ", it doesn't respect the following condition : stepId < 0");
            var limit = reader.ReadUShort();
            objectives = new Types.QuestObjectiveInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 objectives[i] = Types.ProtocolTypeManager.GetInstance<Types.QuestObjectiveInformations>(reader.ReadUShort());
                 objectives[i].Deserialize(reader);
            }
            

}


}


}