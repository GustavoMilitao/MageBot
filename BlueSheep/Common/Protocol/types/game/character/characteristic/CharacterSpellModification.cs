


















// Generated on 12/11/2014 19:02:03
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class CharacterSpellModification
    {

        public new const int ID = 215;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public byte modificationType;
        public int spellId;
        public Types.CharacterBaseCharacteristic value;


        public CharacterSpellModification()
        {
        }

        public CharacterSpellModification(byte modificationType, int spellId, Types.CharacterBaseCharacteristic value)
        {
            this.modificationType = modificationType;
            this.spellId = spellId;
            this.value = value;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteByte(modificationType);
            writer.WriteVarShort((short)spellId);
            value.Serialize(writer);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            modificationType = reader.ReadByte();
            if (modificationType < 0)
                throw new Exception("Forbidden value on modificationType = " + modificationType + ", it doesn't respect the following condition : modificationType < 0");
            spellId = reader.ReadVarUhShort();
            if (spellId < 0)
                throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
            value = new Types.CharacterBaseCharacteristic();
            value.Deserialize(reader);


        }


    }


}