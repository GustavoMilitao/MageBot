


















// Generated on 12/11/2014 19:02:03
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class EntityDispositionInformations
    {

        public new const int ID = 60;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public int cellId;
        public byte direction;


        public EntityDispositionInformations()
        {
        }

        public EntityDispositionInformations(int cellId, byte direction)
        {
            this.cellId = cellId;
            this.direction = direction;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteShort((short)cellId);
            writer.WriteByte(direction);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            cellId = reader.ReadShort();
            if (cellId < -1 || cellId > 559)
                throw new Exception("Forbidden value on cellId = " + cellId + ", it doesn't respect the following condition : cellId < -1 || cellId > 559");
            direction = reader.ReadByte();
            if (direction < 0)
                throw new Exception("Forbidden value on direction = " + direction + ", it doesn't respect the following condition : direction < 0");


        }


    }


}