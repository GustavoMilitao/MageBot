


















// Generated on 12/11/2014 19:02:05
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameFightFighterMonsterLightInformations : GameFightFighterLightInformations
    {

        public new const int ID = 455;
        public override int TypeId
        {
            get { return ID; }
        }

        public int creatureGenericId;


        public GameFightFighterMonsterLightInformations()
        {
        }

        public GameFightFighterMonsterLightInformations(ulong id, sbyte wave, int level, sbyte breed, int creatureGenericId)
                 : base(id, wave, level, breed)
        {
            this.creatureGenericId = creatureGenericId;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteVarShort((short)creatureGenericId);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            creatureGenericId = reader.ReadVarUhShort();
            if (creatureGenericId < 0)
                throw new Exception("Forbidden value on creatureGenericId = " + creatureGenericId + ", it doesn't respect the following condition : creatureGenericId < 0");


        }


    }


}