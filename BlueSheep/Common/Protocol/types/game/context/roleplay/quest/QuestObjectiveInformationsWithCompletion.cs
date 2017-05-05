


















// Generated on 12/11/2014 19:02:08
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class QuestObjectiveInformationsWithCompletion : QuestObjectiveInformations
{

public new const int ID = 386;
public override int TypeId
{
    get { return ID; }
}

public int curCompletion;
        public int maxCompletion;
        

public QuestObjectiveInformationsWithCompletion()
{
}

public QuestObjectiveInformationsWithCompletion(int objectiveId, bool objectiveStatus, string[] dialogParams, int curCompletion, int maxCompletion)
         : base(objectiveId, objectiveStatus, dialogParams)
        {
            this.curCompletion = curCompletion;
            this.maxCompletion = maxCompletion;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteVarShort((short)curCompletion);
            writer.WriteVarShort((short)maxCompletion);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            curCompletion = reader.ReadVarUhShort();
            if (curCompletion < 0)
                throw new Exception("Forbidden value on curCompletion = " + curCompletion + ", it doesn't respect the following condition : curCompletion < 0");
            maxCompletion = reader.ReadVarUhShort();
            if (maxCompletion < 0)
                throw new Exception("Forbidden value on maxCompletion = " + maxCompletion + ", it doesn't respect the following condition : maxCompletion < 0");
            

}


}


}