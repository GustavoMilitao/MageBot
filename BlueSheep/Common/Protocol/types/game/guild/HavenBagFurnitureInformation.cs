using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class HavenBagFurnitureInformation
    {
        public new const uint ID = 498;
        public uint cellId = 0;
        public int funitureId = 0;
        public uint orientation = 0;

        public void serializeAs_HavenBagFurnitureInformation(BigEndianWriter writer)
        {
            if (cellId < 0)
            {
                throw new Exception("Forbidden value (" + cellId + ") on element cellId.");
            }
            writer.WriteVarShort((short)cellId);
            writer.WriteInt(funitureId);
            if (orientation < 0)
            {
                throw new Exception("Forbidden value (" + orientation + ") on element orientation.");
            }
            writer.WriteByte((byte)orientation);
        }
        public void deserializeAs_HavenBagFurnitureInformation(BigEndianReader reader)
        {
            this._cellIdFunc(reader);
            _funitureIdFunc(reader);
            this._orientationFunc(reader);
        }
        private void _funitureIdFunc(BigEndianReader reader)
        {
            funitureId = reader.ReadInt();
        }

        private void _cellIdFunc(BigEndianReader reader)
        {
            cellId = reader.ReadVarUhShort();
            if (this.cellId < 0)
            {
                throw new Exception("Forbidden value (" + this.cellId + ") on element of HavenBagFurnitureInformation.cellId.");
            }
        }

        private void _orientationFunc(BigEndianReader reader)
        {
            orientation = reader.ReadByte();
            if (this.orientation < 0)
            {
                throw new Exception("Forbidden value (" + this.orientation + ") on element of HavenBagFurnitureInformation.orientation.");
            }
        }


    }
}