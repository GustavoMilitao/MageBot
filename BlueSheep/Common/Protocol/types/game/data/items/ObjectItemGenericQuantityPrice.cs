using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class ObjectItemGenericQuantityPrice : ObjectItemGenericQuantity
    {
        public new const uint ID = 494;
        public double price = 0;

        public virtual void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            if (this.price < 0 || this.price > 9007199254740990)
              {
                throw new Exception("Forbidden value (" + this.price + ") on element price.");
               }
            writer.WriteVarLong((long)price);
        }
        public virtual void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            this._priceFunc(reader);
        }

        private void _priceFunc(BigEndianReader reader)
        {
            this.price = reader.ReadVarUhLong();
            if (this.price < 0 || this.price > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + this.price + ") on element of ObjectItemGenericQuantityPrice.price.");
            }
        }

    }
}