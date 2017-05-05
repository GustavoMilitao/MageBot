


















// Generated on 12/11/2014 19:02:09
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class SpellItem : Item
    {

        public new const short ID = 49;
        public override short TypeId
        {
            get { return ID; }
        }


        public int spellId;
        public short spellLevel;


        public SpellItem()
        {
        }

        public SpellItem(int spellId, short spellLevel)
        {
            this.spellId = spellId;
            this.spellLevel = spellLevel;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteInt(spellId);
            writer.WriteShort(spellLevel);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            spellId = reader.ReadInt();
            spellLevel = reader.ReadShort();
            if (spellLevel < 1 || spellLevel > 200)
                throw new Exception("Forbidden value on spellLevel = " + spellLevel + ", it doesn't respect the following condition : spellLevel < 1 || spellLevel > 6");
        }


    }


}