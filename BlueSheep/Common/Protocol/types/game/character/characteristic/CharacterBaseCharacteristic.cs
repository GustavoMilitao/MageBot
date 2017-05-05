


















// Generated on 12/11/2014 19:02:03
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class CharacterBaseCharacteristic
    {

        public new const int ID = 4;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public int @base;
        public int additionnal;
        public int objectsAndMountBonus;
        public int alignGiftBonus;
        public int contextModif;


        public CharacterBaseCharacteristic()
        {
        }

        public CharacterBaseCharacteristic(int @base, int additionnal, int objectsAndMountBonus, int alignGiftBonus, int contextModif)
        {
            this.@base = @base;
            this.additionnal = additionnal;
            this.objectsAndMountBonus = objectsAndMountBonus;
            this.alignGiftBonus = alignGiftBonus;
            this.contextModif = contextModif;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteVarShort((short)@base);
            writer.WriteVarShort((short)additionnal);
            writer.WriteVarShort((short)objectsAndMountBonus);
            writer.WriteVarShort((short)alignGiftBonus);
            writer.WriteVarShort((short)contextModif);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            @base = reader.ReadVarUhShort();
            additionnal = reader.ReadVarUhShort();
            objectsAndMountBonus = reader.ReadVarUhShort();
            alignGiftBonus = reader.ReadVarUhShort();
            contextModif = reader.ReadVarUhShort();


        }


    }


}