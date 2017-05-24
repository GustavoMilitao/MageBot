using MageBot.DataFiles.Data.D2o;
using MageBot.Protocol.Enums;
using MageBot.Protocol.Messages.Game.Context.Roleplay.Stats;
using System;
using System.Collections;

namespace MageBot.Core.Char
{
    public class Character
    {
        private Account.Account Account { get; set; }

        public Character(Account.Account account)
        {
            this.Account = account;
        }

        public void UpAuto()
        {
            if (Account.Config.CaracToAutoUp.HasValue)
            {
                //int boost = GetBoost(Account.Config.CaracToAutoUp.Value);
                //int quantity = Account.CharacterStats.StatsPoints / boost;
                UpStat(Account.Config.CaracToAutoUp.Value, Account.CharacterStats.StatsPoints);
                Account.Wait(1000);
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
            int boostPoint = 0;
            foreach (ArrayList upPointsAndBoostCost in boost)
            {
                boostPoint = Convert.ToInt32(upPointsAndBoostCost[0]) > upPoints ? boostPoint : Convert.ToInt32(upPointsAndBoostCost[1]);
            }
            return boostPoint;
        }

        #endregion
    }
}
