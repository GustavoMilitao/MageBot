using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class HouseInstanceInformations : Message
    {
        public const uint ID = 511;
        public uint instanceId = 0;
        public bool secondHand = false;
        public String ownerName = "";
        public bool isOnSale = false;
        public bool isSaleLocked = false;

        public override uint ProtocolID => ID;

        public override void Serialize(BigEndianWriter writer)
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

        public override void Deserialize(BigEndianReader reader)
        {
            this.deserializeByteBoxes(reader);
            _instanceIdFunc(reader);
            _ownerNameFunc(reader);
        }

        private void _instanceIdFunc(BigEndianReader reader)
        {
            instanceId = reader.ReadUInt();
            if (instanceId < 0)
            {
                throw new Exception("Forbidden value (" + instanceId + ") on element of HouseInstanceInformations.instanceId.");
            }
        }

        private void deserializeByteBoxes(BigEndianReader reader)
        {
            uint _loc2_ = reader.ReadByte();
            this.secondHand = BooleanByteWrapper.GetFlag((byte)_loc2_, 0);
            this.isOnSale = BooleanByteWrapper.GetFlag((byte)_loc2_, 1);
            this.isSaleLocked = BooleanByteWrapper.GetFlag((byte)_loc2_, 2);
        }

        private void _ownerNameFunc(BigEndianReader reader) 
       {
          this.ownerName = reader.ReadUTF();
       }

    }
}