


















// Generated on 12/11/2014 19:02:04
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class FightResultMutantListEntry : FightResultFighterListEntry
    {

        public new const int ID = 216;
        public override int TypeId
        {
            get { return ID; }
        }

        public int level;


        public FightResultMutantListEntry()
        {
        }

        public FightResultMutantListEntry(int outcome, sbyte wave, Types.FightLoot rewards, ulong id, bool alive, int level)
                 : base(outcome, wave, rewards, id, alive)
        {
            this.level = level;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteVarShort((short)level);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            level = reader.ReadVarUhShort();
            if (level < 0)
                throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 0");


        }


    }


}