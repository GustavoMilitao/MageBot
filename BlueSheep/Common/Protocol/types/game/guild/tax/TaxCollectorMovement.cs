using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class TaxCollectorMovement
    {
        public new const uint ID = 493;
        public uint movementType = 0;
        public TaxCollectorBasicInformations basicInfos;
        public double playerId = 0;
        public String playerName = "";

        public void Serialize(BigEndianWriter writer)
        {
            writer.WriteByte((byte)movementType);
            basicInfos.Serialize(writer);
            if (playerId < 0 || playerId > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + playerId + ") on element playerId.");
            }
            writer.WriteVarLong((long)playerId);
            writer.WriteUTF(playerName);
        }
        public void Deserialize(BigEndianReader reader)
        {
            this._movementTypeFunc(reader);
            basicInfos = new TaxCollectorBasicInformations();
            basicInfos.Deserialize(reader);
            this._playerIdFunc(reader);
            _playerNameFunc(reader);
        }

        private void _playerNameFunc(BigEndianReader reader)
        {
            playerName = reader.ReadUTF();
        }

        private void _movementTypeFunc(BigEndianReader reader)
        {
            this.movementType = reader.ReadByte();
            if (this.movementType < 0)
            {
                throw new Exception("Forbidden value (" + this.movementType + ") on element of TaxCollectorMovement.movementType.");
            }
        }

        private void _playerIdFunc(BigEndianReader reader)
        {
            this.playerId = reader.ReadVarUhLong();
            if (this.playerId < 0 || this.playerId > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + this.playerId + ") on element of TaxCollectorMovement.playerId.");
            }
        }

    }
}