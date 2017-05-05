


















// Generated on 12/11/2014 19:02:11
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class SubEntity
    {

        public new const int ID = 54;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public byte bindingPointCategory;
        public byte bindingPointIndex;
        public Types.EntityLook subEntityLook;


        public SubEntity()
        {
        }

        public SubEntity(byte bindingPointCategory, byte bindingPointIndex, Types.EntityLook subEntityLook)
        {
            this.bindingPointCategory = bindingPointCategory;
            this.bindingPointIndex = bindingPointIndex;
            this.subEntityLook = subEntityLook;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteByte(bindingPointCategory);
            writer.WriteByte(bindingPointIndex);
            subEntityLook.Serialize(writer);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            bindingPointCategory = reader.ReadByte();
            if (bindingPointCategory < 0)
                throw new Exception("Forbidden value on bindingPointCategory = " + bindingPointCategory + ", it doesn't respect the following condition : bindingPointCategory < 0");
            bindingPointIndex = reader.ReadByte();
            if (bindingPointIndex < 0)
                throw new Exception("Forbidden value on bindingPointIndex = " + bindingPointIndex + ", it doesn't respect the following condition : bindingPointIndex < 0");
            subEntityLook = new Types.EntityLook();
            subEntityLook.Deserialize(reader);


        }


    }


}