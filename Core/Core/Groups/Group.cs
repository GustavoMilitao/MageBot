using System.Collections.Generic;

namespace BlueSheep.Core.Groups
{
    public class Group
    {
        #region Fields
        public List<Account.Account> accounts;
        public string name;
        #endregion

        #region Constructeurs
        public Group(List<Account.Account> list, string namee)
        {
            accounts = list;
            name = namee;
        }
        #endregion

        #region Publics methods
        public Account.Account GetMaster()
        {
            foreach (Account.Account a in accounts)
            {
                if (a.Config.IsMaster)
                    return a;
            }
            return null;
        }

        public void MoveGroup(string move)
        {
            foreach (Account.Account a in accounts)
            {
                if (a.Config.IsMaster)
                {
                    a.Map.ChangeMap(move);
                    foreach (Account.Account ac in accounts)
                    {
                        if (ac.Config.IsSlave == true)
                            ac.Map.ChangeMap(move);
                        System.Threading.Thread.Sleep(500);
                    }
                }
            }
        }

        public void UseItemGroup(int uid)
        {
            foreach (Account.Account a in accounts)
            {
                if (a.Config.IsMaster == true)
                {
                    a.Inventory.UseItem(uid);
                    foreach (Account.Account ac in accounts)
                    {
                        if (ac.Config.IsSlave == true)
                            ac.Inventory.UseItem(uid);
                        System.Threading.Thread.Sleep(500);
                    }
                }
            }
        }

        public void UseZaapGroup()
        {
            foreach (Account.Account a in accounts)
            {
                if (a.Config.IsMaster == true)
                {
                    a.Map.UseZaapTo((int)a.Config.Path.Current_Action.m_delta);
                    foreach (Account.Account ac in accounts)
                    {
                        if (ac.Config.IsSlave == true)
                            ac.Map.UseZaapTo((int)a.Config.Path.Current_Action.m_delta);
                        System.Threading.Thread.Sleep(500);
                    }
                    return;
                }
            }
        }

        public void UseZaapiGroup()
        {
            foreach (Account.Account a in accounts)
            {
                if (a.Config.IsMaster == true)
                {
                    a.Map.useZaapiTo((int)a.Config.Path.Current_Action.m_delta);
                    foreach (Account.Account ac in accounts)
                    {
                        if (ac.Config.IsSlave == true)
                            ac.Map.useZaapiTo((int)a.Config.Path.Current_Action.m_delta);
                        System.Threading.Thread.Sleep(500);
                    }
                    return;
                }
            }
        }

        public async void MoveToCellGroup(int delta)
        {
            foreach (Account.Account a in accounts)
            {
                if (a.Config.IsMaster == true)
                {
                    await a.Map.MoveToCell(delta);
                    foreach (Account.Account ac in accounts)
                    {
                        if (ac.Config.IsSlave == true)
                            await ac.Map.MoveToCell(delta);
                        System.Threading.Thread.Sleep(500);
                    }
                    return;
                }
            }
        }

        public void MoveToElementGroup(int delta)
        {
            foreach (Account.Account a in accounts)
            {
                if (a.Config.IsMaster == true)
                {
                    a.Map.MoveToSecureElement(delta);
                    foreach (Account.Account ac in accounts)
                    {
                        if (ac.Config.IsSlave == true)
                            ac.Map.MoveToSecureElement(delta);
                        System.Threading.Thread.Sleep(500);
                    }
                    return;
                }
            }
        }

        public void TalkToNpcGroup(int delta)
        {
            foreach (Account.Account a in accounts)
            {
                if (a.Config.IsMaster == true)
                {
                    a.Npc.TalkToNpc(delta);
                    foreach (Account.Account ac in accounts)
                    {
                        if (ac.Config.IsSlave == true)
                            ac.Npc.TalkToNpc(delta);
                        System.Threading.Thread.Sleep(500);
                    }
                    return;
                }
            }
        }

        public void RequestExchangeGroup(string delta)
        {
            foreach (Account.Account a in accounts)
            {
                if (a.Config.IsMaster == true)
                {
                    a.Inventory.RequestExchange(delta);
                    foreach (Account.Account ac in accounts)
                    {
                        if (ac.Config.IsSlave == true)
                            ac.Inventory.RequestExchange(delta);
                        System.Threading.Thread.Sleep(2000);
                    }
                    return;
                }
            }
        }
    #endregion

    }
}