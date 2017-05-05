


















// Generated on 12/11/2014 19:02:03
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class ActorOrientation
    {

        public new const int ID = 353;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public ulong id;
        public sbyte direction;


        public ActorOrientation()
        {
        }

        public ActorOrientation(ulong id, sbyte direction)
        {
            this.id = id;
            this.direction = direction;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteULong(id);
            writer.WriteSByte(direction);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            id = reader.ReadULong();
            direction = reader.ReadSByte();
            if (direction < 0)
                throw new Exception("Forbidden value on direction = " + direction + ", it doesn't respect the following condition : direction < 0");


        }


    }


}