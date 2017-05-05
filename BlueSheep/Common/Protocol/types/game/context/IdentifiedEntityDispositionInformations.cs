


















// Generated on 12/11/2014 19:02:04
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class IdentifiedEntityDispositionInformations : EntityDispositionInformations
    {

        public new const int ID = 107;
        public override int TypeId
        {
            get { return ID; }
        }

        public ulong id;


        public IdentifiedEntityDispositionInformations()
        {
        }

        public IdentifiedEntityDispositionInformations(int cellId, byte direction, ulong id)
                 : base(cellId, direction)
        {
            this.id = id;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteULong(id);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            id = reader.ReadULong();


        }


    }


}