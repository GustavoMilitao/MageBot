using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class ObjectItemGenericQuantity : Item
    {
        public new const int ID = 483;
        public uint objectGID = 0;
        public uint quantity = 0;

        public ObjectItemGenericQuantity()
        {

        }

        public virtual void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            if (objectGID < 0)
            {
                throw new Exception("Forbidden value ("  + objectGID +  ") on element objectGID.");
            }
            writer.WriteVarShort((short)(int)objectGID);
            if (quantity < 0)
            {
                throw new Exception("Forbidden value (" +  quantity + ") on element quantity.");
            }
            writer.WriteVarInt((int)quantity);
        }

        public virtual void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            _objectGIDFunc(reader);
            _quantityFunc(reader);
        }

        private void _objectGIDFunc(BigEndianReader reader)
        {
            objectGID = reader.ReadVarUhShort();
            if (objectGID < 0)
            {
                throw new Exception("Forbidden value (" + objectGID + ") on element of ObjectItemGenericQuantity.objectGID.");
            }
        }

        private void _quantityFunc(BigEndianReader reader)
        {
            quantity = reader.ReadVarUhInt();
            if (quantity < 0)
            {
                throw new Exception("Forbidden value (" + quantity + ") on element of ObjectItemGenericQuantity.quantity.");
            }
        }

    }
}