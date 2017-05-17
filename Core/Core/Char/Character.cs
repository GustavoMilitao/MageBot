using DataFiles.Data.D2o;
using BlueSheep.Protocol.Enums;
using BlueSheep.Protocol.Messages.Game.Context.Roleplay.Stats;
using System.Collections;

namespace BlueSheep.Core.Char
{
    public class Character
    {
        private Account.Account Account { get; set; }
        public BoostableCharacteristicEnum? CaracToAutoUp { get; set; }

        public Character(Account.Account account)
        {
            this.Account = account;
        }

        public void UpAuto()
        {
            if (CaracToAutoUp.HasValue)
            {
                int boost = GetBoost(CaracToAutoUp.Value);
                int quantity = Account.CharacterStats.StatsPoints / boost;
                for (int i = 0; i < quantity; i++)
                {
                    UpStat(CaracToAutoUp.Value, boost);
                }
            }
        }

        public void UpStat(BoostableCharacteristicEnum statToUp)
        {
            int boost = GetBoost(statToUp);
            if (Account.CharacterStats.StatsPoints >= boost)
                UpStat(statToUp, boost);
        }

        #region Private Methods
        private void UpStat(BoostableCharacteristicEnum statId, int boost)
        {
            StatsUpgradeRequestMessage msg = new StatsUpgradeRequestMessage(false, (byte)statId, (ushort)boost);
            Account.SocketManager.Send(msg);
        }


        private int GetBoost(BoostableCharacteristicEnum statId)
        {
            DataClass d = GameData.GetDataObject(D2oFileEnum.Breeds, Account.CharacterBaseInformations.Breed);
            switch (statId)
            {
                case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_STRENGTH:
                    return GetBoostByArrayListAndUpPoints((ArrayList)d.Fields["statsPointsForStrength"], Account.CharacterStats.Strength.Base);
                case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_VITALITY:
                    return GetBoostByArrayListAndUpPoints((ArrayList)d.Fields["statsPointsForVitality"], Account.CharacterStats.Vitality.Base);
                case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_WISDOM:
                    return GetBoostByArrayListAndUpPoints((ArrayList)d.Fields["statsPointsForWisdom"], Account.CharacterStats.Wisdom.Base);
                case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_CHANCE:
                    return GetBoostByArrayListAndUpPoints((ArrayList)d.Fields["statsPointsForChance"], Account.CharacterStats.Chance.Base);
                case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_AGILITY:
                    return GetBoostByArrayListAndUpPoints((ArrayList)d.Fields["statsPointsForAgility"], Account.CharacterStats.Agility.Base);
                case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_INTELLIGENCE:
                    return GetBoostByArrayListAndUpPoints((ArrayList)d.Fields["statsPointsForIntelligence"], Account.CharacterStats.Intelligence.Base);
            }
            return 1;
        }

        private int GetBoostByArrayListAndUpPoints(ArrayList boost, int upPoints)
        {
            if (boost.Count > 1)
            {
                int boostPoint = 0;
                foreach (ArrayList upPointsAndBoostCost in boost)
                {
                    boostPoint = (int)upPointsAndBoostCost[0] > upPoints ? boostPoint : (int)upPointsAndBoostCost[1];
                }
            }
            return (int)boost[1];
        }

        #endregion
    }
}
