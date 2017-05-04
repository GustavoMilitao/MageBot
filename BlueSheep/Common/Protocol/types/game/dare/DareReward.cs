namespace BlueSheep.Common.Protocol.Types
{
    public class DareReward
    {
        public static const uint protocolId = 505;
        public uint type = 0;
        public uint monsterId = 0;
        public double kamas = 0;
        public double dareId = 0;

        public uint getTypeId()
        {
            return 505;
        }
        public void reset()
        {
            type = 0;
            monsterId = 0;
            kamas = 0;
            dareId = 0;
        }
        public void serializeAs_DareReward(BigEndianWriter writer)
        {
            param1.writeByte(type);
            if (monsterId < 0)
            {
                throw new Exception("Forbidden value (" + monsterId + ") on element monsterId.");
            }
            param1.writeVarShort(monsterId);
            if (kamas < 0 || kamas > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + kamas + ") on element kamas.");
            }
            param1.writeVarLong(kamas);
            if (dareId < 0 || dareId > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + dareId + ") on element dareId.");
            }
            param1.writeDouble(dareId);
        }
        public void deserializeAs_DareReward(BigEndianReader reader)
        {
            this._typeFunc(param1);
            _monsterIdFunc(param1);
            this._kamasFunc(param1);
            _dareIdFunc(param1);
        }
        public void deserializeAsyncAs_DareReward(FuncTree param1)
        {
            param1.addChild(this._typeFunc);
            param1.addChild(this._monsterIdFunc);
            param1.addChild(this._kamasFunc);
            param1.addChild(this._dareIdFunc);
        }
        private void _monsterIdFunc(BigEndianReader reader)
        {
            monsterId = param1.readVarUhShort();
            if (monsterId < 0)
            {
                throw new Exception("Forbidden value (" + monsterId + ") on element of DareReward.monsterId.");
            }
        }
        private void _dareIdFunc(BigEndianReader reader)
        {
            dareId = param1.readDouble();
            if (dareId < 0 || dareId > 9007199254740990)
            {
                throw new Exception("Forbidden value (" + dareId + ") on element of DareReward.dareId.");
            }
        }

    }
}