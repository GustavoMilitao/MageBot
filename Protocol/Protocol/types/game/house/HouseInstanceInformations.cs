using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class HouseInstanceInformations 
    {
        public const short ID = 511;
        public int instanceId = 0;
        public bool secondHand = false;
        public String ownerName = "";
        public bool isOnSale = false;
        public bool isSaleLocked = false;

        public HouseInstanceInformations()
        {

        }

        public virtual void Serialize(BigEndianWriter writer)
        {
            uint _loc2_ = 0;
            _loc2_ = BooleanByteWrapper.SetFlag((byte)_loc2_, 0, secondHand);
            _loc2_ = BooleanByteWrapper.SetFlag((byte)_loc2_, 1, isOnSale);
            _loc2_ = BooleanByteWrapper.SetFlag((byte)_loc2_, 2, isSaleLocked);
            writer.WriteByte((byte)_loc2_);
            if (instanceId < 0)
            {
                throw new Exception("Forbidden value (" + instanceId + ") on element instanceId.");
            }
            writer.WriteInt((int)instanceId);
            writer.WriteUTF(ownerName);
        }

        public virtual void Deserialize(BigEndianReader reader)
        {
            deserializeByteBoxes(reader);
            _instanceIdFunc(reader);
            _ownerNameFunc(reader);
        }

        private void _instanceIdFunc(BigEndianReader reader)
        {
            instanceId = reader.ReadInt();
            if (instanceId < 0)
            {
                throw new Exception("Forbidden value (" + instanceId + ") on element of HouseInstanceInformations.instanceId.");
            }
        }

        private void deserializeByteBoxes(BigEndianReader reader)
        {
            uint _loc2_ = reader.ReadByte();
            secondHand = BooleanByteWrapper.GetFlag((byte)_loc2_, 0);
            isOnSale = BooleanByteWrapper.GetFlag((byte)_loc2_, 1);
            isSaleLocked = BooleanByteWrapper.GetFlag((byte)_loc2_, 2);
        }

        private void _ownerNameFunc(BigEndianReader reader) 
       {
            ownerName = reader.ReadUTF();
       }

    }
}