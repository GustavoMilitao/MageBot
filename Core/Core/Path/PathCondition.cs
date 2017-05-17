using System;

namespace MageBot.Core.Path
{
    class PathCondition
    {
        #region Fields
        PathConditionEnum m_Cond;
        object m_delta;
        char m_operateur;
        Account.Account account;
        #endregion

        #region Constructors
        public PathCondition(PathConditionEnum condition, object delta, char operateur, Account.Account account)
        {
            m_Cond = condition;
            m_delta = delta;
            m_operateur = operateur;
            this.account = account;
        }
        #endregion

        #region Public Methods
        public bool CheckCondition()
        {
            switch (m_Cond)
            {
                case PathConditionEnum.Null:
                    return true;
                case PathConditionEnum.LastMapId:
                    if (account.MapData.LastMapId == Convert.ToInt32(m_delta))
                        return true;
                    else
                        return false;
                case PathConditionEnum.Level:
                    switch (m_operateur)
                    {
                        case '<':
                            if (account.CharacterBaseInformations.Level < Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '>':
                            if (account.CharacterBaseInformations.Level > Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '=':
                            if (account.CharacterBaseInformations.Level == Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                    //return false;
                case PathConditionEnum.Pods:
                    switch (m_operateur)
                    {
                        case '<':
                            if (account.Inventory.weight < Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '>':
                            if (account.Inventory.weight > Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '=':
                            if (account.Inventory.weight == Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                    //return false;
                case PathConditionEnum.Alive:
                    switch (m_operateur)
                    {
                        case '=':
                            if ((account.CharacterStats.EnergyPoints <= 0) == Convert.ToBoolean(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                case PathConditionEnum.PodsPercent:
                    if (Convert.ToString(m_delta) == "%PODS%")
                        m_delta = account.Config.MaxPodsPercent;
                    switch (m_operateur)
                    {
                        case '<':
                            if (account.Inventory.weightPercent < Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '>':
                            if (account.Inventory.weightPercent > Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        case '=':
                            if (account.Inventory.weightPercent == Convert.ToInt32(m_delta))
                                return true;
                            else
                                return false;
                        default:
                            return false;
                    }
                    //return false;


            }
            return false;
        }
        #endregion
    }
}
