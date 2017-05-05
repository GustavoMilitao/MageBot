


















// Generated on 12/11/2014 19:02:05
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameFightFighterTaxCollectorLightInformations : GameFightFighterLightInformations
    {

        public new const int ID = 457;
        public override int TypeId
        {
            get { return ID; }
        }

        public int firstNameId;
        public int lastNameId;


        public GameFightFighterTaxCollectorLightInformations()
        {
        }

        public GameFightFighterTaxCollectorLightInformations(ulong id, sbyte wave, int level, sbyte breed, int firstNameId, int lastNameId)
                 : base(id, wave, level, breed)
        {
            this.firstNameId = firstNameId;
            this.lastNameId = lastNameId;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteVarShort((short)firstNameId);
            writer.WriteVarShort((short)lastNameId);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            firstNameId = reader.ReadVarUhShort();
            if (firstNameId < 0)
                throw new Exception("Forbidden value on firstNameId = " + firstNameId + ", it doesn't respect the following condition : firstNameId < 0");
            lastNameId = reader.ReadVarUhShort();
            if (lastNameId < 0)
                throw new Exception("Forbidden value on lastNameId = " + lastNameId + ", it doesn't respect the following condition : lastNameId < 0");


        }


    }


}