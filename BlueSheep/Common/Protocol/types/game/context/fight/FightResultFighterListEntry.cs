


















// Generated on 12/11/2014 19:02:04
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class FightResultFighterListEntry : FightResultListEntry
    {

        public new const int ID = 189;
        public override int TypeId
        {
            get { return ID; }
        }

        public ulong id;
        public bool alive;


        public FightResultFighterListEntry()
        {
        }

        public FightResultFighterListEntry(int outcome, sbyte wave, Types.FightLoot rewards, ulong id, bool alive)
                 : base(outcome, wave, rewards)
        {
            this.id = id;
            this.alive = alive;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteULong(id);
            writer.WriteBoolean(alive);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            id = reader.ReadULong();
            alive = reader.ReadBoolean();


        }


    }


}