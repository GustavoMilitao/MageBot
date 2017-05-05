


















// Generated on 12/11/2014 19:02:02
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class ServerSessionConstant
    {

        public new const int ID = 430;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public ulong id;


        public ServerSessionConstant()
        {
        }

        public ServerSessionConstant(ulong id)
        {
            this.id = id;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteVarShort((short)id);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            id = reader.ReadVarUhShort();
            if (id < 0)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0");


        }


    }


}