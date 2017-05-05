using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class ObjectItemGenericQuantityPrice : ObjectItemGenericQuantity
    {
        public new const short ID = 494;
        public double price = 0;

        public ObjectItemGenericQuantityPrice()
        {

        }

        public virtual void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            if (price < 0 || price > 9007199254740990)
              {
                throw new Exception("Forbidden value (" + price + ") on element price.");
               }
            writer.WriteVarLong((long)price);
        }
        public virtual void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            _priceFunc(reader);
        }

        private void _priceFunc(BigEndianReader reader)
        {
            price = reader.ReadVarUhLong();
            if (price < 0 || price > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + price + ") on element of ObjectItemGenericQuantityPrice.price.");
            }
        }

    }
}