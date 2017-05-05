


















// Generated on 12/11/2014 19:02:03
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;
using BlueSheep.Util.Enums.Char;

namespace BlueSheep.Common.Protocol.Types
{

    public class CharacterBaseInformations : CharacterMinimalPlusLookInformations
    {

        public new const int ID = 45;
        public override int TypeId
        {
            get { return ID; }
        }

        public Breed breed;
        public Sex sex;


        public CharacterBaseInformations()
        {
        }

        public CharacterBaseInformations(uint id, byte level, string name, Types.EntityLook entityLook, sbyte breed, bool sex)
                 : base(id, level, name, entityLook)
        {
            this.breed = (Breed)breed;
            this.sex = (Sex)Convert.ToInt32(sex);
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteSByte((sbyte)breed);
            writer.WriteBoolean(Convert.ToBoolean(sex));


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            breed = (Breed)reader.ReadSByte();
            sex = (Sex)Convert.ToInt32(reader.ReadBoolean());


        }


    }


}