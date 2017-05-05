


















// Generated on 12/11/2014 19:02:09
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class ObjectEffect
    {

        public new const int ID = 76;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public int actionId;


        public ObjectEffect()
        {
        }

        public ObjectEffect(int actionId)
        {
            this.actionId = actionId;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteVarShort((short)actionId);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            actionId = reader.ReadVarUhShort();
            if (actionId < 0)
                throw new Exception("Forbidden value on actionId = " + actionId + ", it doesn't respect the following condition : actionId < 0");
        }


    }


}