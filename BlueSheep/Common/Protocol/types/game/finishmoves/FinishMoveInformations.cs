using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class FinishMoveInformations
    {
        public new const short ID = 506;
        public uint finishMoveId = 0;
        public bool finishMoveState = false;

        public void Serialize(BigEndianWriter writer)
        {
            if (finishMoveId < 0)
            {
                throw new Exception("Forbidden value (" + finishMoveId + ") on element finishMoveId.");
            }
            writer.WriteInt((int)finishMoveId);
            writer.WriteBoolean(finishMoveState);
        }
        public void deserializeAs_FinishMoveInformations(BigEndianReader reader)
        {
            _finishMoveIdFunc(reader);
            _finishMoveStateFunc(reader);
        }

        private void _finishMoveStateFunc(BigEndianReader reader)
        {
            finishMoveState = reader.ReadBoolean();
        }

        private void _finishMoveIdFunc(BigEndianReader reader)
        {
            finishMoveId = reader.ReadUInt();
            if (finishMoveId < 0)
            {
                throw new Exception("Forbidden value (" + finishMoveId + ") on element of FinishMoveInformations.finishMoveId.");
            }
        }

    }
}