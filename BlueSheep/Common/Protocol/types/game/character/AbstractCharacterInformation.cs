


















// Generated on 12/11/2014 19:02:02
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class AbstractCharacterInformation
    {

        public new const short ID = 400;
        public virtual short TypeId
        {
            get { return ID; }
        }

        public double Id { get; set; }

        public AbstractCharacterInformation()
        {
        }

        public AbstractCharacterInformation(uint id)
        {
            this.Id = id;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteVarLong((long)Id);
        }

        public virtual void Deserialize(BigEndianReader reader)
        {
            Id = reader.ReadVarUhLong();
            if (Id < 0)
                throw new Exception("Forbidden value on id = " + Id + ", it doesn't respect the following condition : id < 0");
        }
    }


}