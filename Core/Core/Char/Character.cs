using BlueSheep.Common.Data.D2o;
using BlueSheep.Core.Account;
using BlueSheep.Protocol.Enums;
using BlueSheep.Protocol.Messages.Game.Context.Roleplay.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core
{
    public class Character
    {
        public Account account { get; set; }
        public BoostableCharacteristicEnum? CaracToAutoUp { get; set; }

        public Character(Account account)
        {
            this.account = account;
        }

        public void UpAuto()
        {
            if (CaracToAutoUp.HasValue)
            {
                int boost = GetBoost(CaracToAutoUp.Value);
                int quantity = account.CharacterStats.StatsPoints / boost;
                for (int i = 0; i < quantity; i++)
                {
                    UpStat(CaracToAutoUp.Value, boost);
                }
            }
        }

        public void UpStat(BoostableCharacteristicEnum statToUp)
        {
            int boost = GetBoost(statToUp);
            if (account.CharacterStats.StatsPoints >= boost)
                UpStat(statToUp, boost);
        }

        #region Private Methods
        private void UpStat(BoostableCharacteristicEnum statId, int boost)
        {
            StatsUpgradeRequestMessage msg = new StatsUpgradeRequestMessage(false, (byte)statId, (ushort)boost);
            account.SocketManager.Send(msg);
        }


        private int GetBoost(BoostableCharacteristicEnum statId)
        {
            DataClass d = GameData.GetDataObject(D2oFileEnum.Breeds, account.CharacterBaseInformations.Breed);
            switch (statId)
            {
                case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_STRENGTH:
                    return getBoostByArrayListAndUpPoints((ArrayList)d.Fields["statsPointsForStrength"], account.CharacterStats.Strength.Base);
                case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_VITALITY:
                    return getBoostByArrayListAndUpPoints((ArrayList)d.Fields["statsPointsForVitality"], account.CharacterStats.Vitality.Base);
                case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_WISDOM:
                    return getBoostByArrayListAndUpPoints((ArrayList)d.Fields["statsPointsForWisdom"], account.CharacterStats.Wisdom.Base);
                case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_CHANCE:
                    return getBoostByArrayListAndUpPoints((ArrayList)d.Fields["statsPointsForChance"], account.CharacterStats.Chance.Base);
                case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_AGILITY:
                    return getBoostByArrayListAndUpPoints((ArrayList)d.Fields["statsPointsForAgility"], account.CharacterStats.Agility.Base);
                case BoostableCharacteristicEnum.BOOSTABLE_CHARAC_INTELLIGENCE:
                    return getBoostByArrayListAndUpPoints((ArrayList)d.Fields["statsPointsForIntelligence"], account.CharacterStats.Intelligence.Base);
            }
            return 1;
        }

        private int getBoostByArrayListAndUpPoints(ArrayList boost, int upPoints)
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
