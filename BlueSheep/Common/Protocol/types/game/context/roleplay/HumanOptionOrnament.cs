


















// Generated on 12/11/2014 19:02:07
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class HumanOptionOrnament : HumanOption
    {

        public new const int ID = 411;
        public override int TypeId
        {
            get { return ID; }
        }

        public int ornamentId;


        public HumanOptionOrnament()
        {
        }

        public HumanOptionOrnament(int ornamentId)
        {
            this.ornamentId = ornamentId;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteVarShort((short)ornamentId);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            ornamentId = reader.ReadVarUhShort();
            if (ornamentId < 0)
                throw new Exception("Forbidden value on ornamentId = " + ornamentId + ", it doesn't respect the following condition : ornamentId < 0");


        }


    }


}