using System;

namespace MageBot.Core.Fight
{
    public class FightCondition
    {
        #region Fields
        FightConditionEnum m_Cond;
        object m_delta;
        char m_operateur;
        Account.Account account;
        #endregion

        #region Constructors
        public FightCondition(FightConditionEnum condition, object delta, char operateur, Account.Account Account)
        {
            m_Cond = condition;
            m_delta = delta;
            m_operateur = operateur;
            account = Account;
        }
        #endregion

        #region Public Methods
        public bool CheckCondition()
        {
            switch (m_Cond)
            {
                case FightConditionEnum.Null:
                    return true;
                case FightConditionEnum.AvailableAP:
                    switch (m_operateur)
                    {
                        case '<':
                            if (account.FightData.Fighter.GameFightMinimalStats.ActionPoints < Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '>':
                            if (account.FightData.Fighter.GameFightMinimalStats.ActionPoints > Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '=':
                            if (account.FightData.Fighter.GameFightMinimalStats.ActionPoints == Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                case FightConditionEnum.AvailablePM:
                    switch (m_operateur)
                    {
                        case '<':
                            if (account.FightData.Fighter.GameFightMinimalStats.MovementPoints < Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '>':
                            if (account.FightData.Fighter.GameFightMinimalStats.MovementPoints > Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '=':
                            if (account.FightData.Fighter.GameFightMinimalStats.MovementPoints == Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                case FightConditionEnum.BossInFight:
                    return account.FightData.BossInFight();
                case FightConditionEnum.DistanceFromTarget:
                    int distance = account.FightData.DistanceFrom(account.FightParser.Target);
                    switch (m_operateur)
                    {
                        case '<':
                            if (distance < Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '>':
                            if (distance > Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '=':
                            if (distance == Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                case FightConditionEnum.MonstersNumber:
                    int number = account.FightData.MonsterNumber;
                    switch (m_operateur)
                    {
                        case '<':
                            if (number < Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '>':
                            if (number > Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '=':
                            if (number == Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                case FightConditionEnum.PlayerLife:
                    int LifePercent = (account.FightData.Fighter.LifePoints / account.FightData.Fighter.MaxLifePoints) * 100;
                    switch (m_operateur)
                    {
                        case '<':
                            if (LifePercent < Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '>':
                            if (LifePercent  > Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '=':
                            if (LifePercent == Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                case FightConditionEnum.TargetLife:
                    int TargetPercent = (account.FightParser.Target.LifePoints / account.FightParser.Target.MaxLifePoints) * 100;
                    switch (m_operateur)
                    {
                        case '<':
                            if (TargetPercent < Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '>':
                            if (TargetPercent > Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '=':
                            if (TargetPercent == Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                case FightConditionEnum.TargetResAir:
                    switch (m_operateur)
                    {
                        case '<':
                            if (account.FightParser.Target.GameFightMinimalStats.AirElementResistPercent < Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '>':
                            if (account.FightParser.Target.GameFightMinimalStats.AirElementResistPercent > Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '=':
                            if (account.FightParser.Target.GameFightMinimalStats.AirElementResistPercent == Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                case FightConditionEnum.TargetResEarth:
                    switch (m_operateur)
                    {
                        case '<':
                            if (account.FightParser.Target.GameFightMinimalStats.EarthElementResistPercent < Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '>':
                            if (account.FightParser.Target.GameFightMinimalStats.EarthElementResistPercent > Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '=':
                            if (account.FightParser.Target.GameFightMinimalStats.EarthElementResistPercent == Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                case FightConditionEnum.TargetResFire:
                    switch (m_operateur)
                    {
                        case '<':
                            if (account.FightParser.Target.GameFightMinimalStats.FireElementResistPercent < Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '>':
                            if (account.FightParser.Target.GameFightMinimalStats.FireElementResistPercent > Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '=':
                            if (account.FightParser.Target.GameFightMinimalStats.FireElementResistPercent == Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                case FightConditionEnum.TargetResNeutral:
                    switch (m_operateur)
                    {
                        case '<':
                            if (account.FightParser.Target.GameFightMinimalStats.NeutralElementResistPercent < Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '>':
                            if (account.FightParser.Target.GameFightMinimalStats.NeutralElementResistPercent > Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '=':
                            if (account.FightParser.Target.GameFightMinimalStats.NeutralElementResistPercent == Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                case FightConditionEnum.TargetResWater:
                    switch (m_operateur)
                    {
                        case '<':
                            if (account.FightParser.Target.GameFightMinimalStats.WaterElementResistPercent < Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '>':
                            if (account.FightParser.Target.GameFightMinimalStats.WaterElementResistPercent > Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '=':
                            if (account.FightParser.Target.GameFightMinimalStats.WaterElementResistPercent == Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                case FightConditionEnum.Turn:
                    switch (m_operateur)
                    {
                        case '<':
                            if (account.FightData.TurnId < Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '>':
                            if (account.FightData.TurnId > Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '=':
                            if (account.FightData.TurnId == Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
            }
            return false;
        }
        #endregion
    }
}
