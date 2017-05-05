


















// Generated on 12/11/2014 19:02:07
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class ObjectItemInRolePlay
{

public new const int ID = 198;
public virtual int TypeId
{
    get { return ID; }
}

public int cellId;
        public int objectGID;
        

public ObjectItemInRolePlay()
{
}

public ObjectItemInRolePlay(int cellId, int objectGID)
        {
            this.cellId = cellId;
            this.objectGID = objectGID;
        }
        

public virtual void Serialize(BigEndianWriter writer)
{

writer.WriteVarShort((short)cellId);
            writer.WriteVarShort((short)objectGID);
            

}

public virtual void Deserialize(BigEndianReader reader)
{

cellId = reader.ReadVarUhShort();
            if (cellId < 0 || cellId > 559)
                throw new Exception("Forbidden value on cellId = " + cellId + ", it doesn't respect the following condition : cellId < 0 || cellId > 559");
            objectGID = reader.ReadVarUhShort();
            if (objectGID < 0)
                throw new Exception("Forbidden value on objectGID = " + objectGID + ", it doesn't respect the following condition : objectGID < 0");
            

}


}


}