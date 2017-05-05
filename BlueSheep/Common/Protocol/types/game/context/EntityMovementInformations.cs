


















// Generated on 12/11/2014 19:02:03
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class EntityMovementInformations
    {

        public new const int ID = 63;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public ulong id;
        public sbyte[] steps;


        public EntityMovementInformations()
        {
        }

        public EntityMovementInformations(ulong id, sbyte[] steps)
        {
            this.id = id;
            this.steps = steps;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteULong(id);
            writer.WriteUShort((ushort)steps.Length);
            foreach (var entry in steps)
            {
                writer.WriteSByte(entry);
            }


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            id = reader.ReadULong();
            var limit = reader.ReadUShort();
            steps = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                steps[i] = reader.ReadSByte();
            }


        }


    }


}