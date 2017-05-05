


















// Generated on 12/11/2014 19:02:11
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class ShortcutSpell : Shortcut
    {

        public new const int ID = 368;
        public override int TypeId
        {
            get { return ID; }
        }

        public int spellId;


        public ShortcutSpell()
        {
        }

        public ShortcutSpell(sbyte slot, int spellId)
                 : base(slot)
        {
            this.spellId = spellId;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteVarShort((short)spellId);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            spellId = reader.ReadVarUhShort();
            if (spellId < 0)
                throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");


        }


    }


}