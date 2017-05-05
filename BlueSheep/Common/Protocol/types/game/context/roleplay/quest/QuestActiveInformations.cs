


















// Generated on 12/11/2014 19:02:08
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class QuestActiveInformations
{

public new const int ID = 381;
public virtual int TypeId
{
    get { return ID; }
}

public int questId;
        

public QuestActiveInformations()
{
}

public QuestActiveInformations(int questId)
        {
            this.questId = questId;
        }
        

public virtual void Serialize(BigEndianWriter writer)
{

writer.WriteVarShort((short)questId);
            

}

public virtual void Deserialize(BigEndianReader reader)
{

questId = reader.ReadVarUhShort();
            if (questId < 0)
                throw new Exception("Forbidden value on questId = " + questId + ", it doesn't respect the following condition : questId < 0");
            

}


}


}