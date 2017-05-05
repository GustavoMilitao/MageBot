


















// Generated on 12/11/2014 19:02:11
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class IndexedEntityLook
    {

        public new const int ID = 405;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public Types.EntityLook look;
        public byte index;


        public IndexedEntityLook()
        {
        }

        public IndexedEntityLook(Types.EntityLook look, byte index)
        {
            this.look = look;
            this.index = index;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            look.Serialize(writer);
            writer.WriteByte(index);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            look = new Types.EntityLook();
            look.Deserialize(reader);
            index = reader.ReadByte();
            if (index < 0)
                throw new Exception("Forbidden value on index = " + index + ", it doesn't respect the following condition : index < 0");


        }


    }


}