using Util.Util.Text.Log;
using System;
using System.Linq;
using System.Threading;

namespace MageBot.Core.Path
{
    public class Action
    {
        #region Fields
        string m_action;
        public object m_delta;
        Account.Account Account;
        #endregion

        #region Constructors
        public Action(string action, object delta, Account.Account Account)
        {
            if (((string)delta).Contains(')'))
            {
                delta = ((string)delta).Substring(0, ((string)delta).Length - 1);
            }
            m_action = action;
            m_delta = delta;
            this.Account = Account;
        }
        #endregion

        #region Public Methods
        public void PerformAction()
        {
            if (Account.Path == null)
                return;
            while (Account.Busy == true)
                //account.PutTaskDelay(5);
                Account.Wait(5);
            switch (m_action)
            {
                case "move(":
                    m_delta = (string)m_delta;
                    if (Account.MyGroup != null)
                    {
                        Account.MyGroup.MoveGroup((string)m_delta);
                        //account.PutTaskDelay(3000);
                        Account.Wait(3000);
                        //if (Account.State == Util.Enums.Internal.Status.Busy)
                        //    PerformAction();
                    }
                    else
                    {
                        Account.Map.ChangeMap((string)m_delta);
                        //account.PutTaskDelay(3000);
                        Account.Wait(3000);
                        //if (Account.State == Util.Enums.Internal.Status.Busy)
                        //    PerformAction();
                    }
                    break;
                case "object(":
                    if (Account.MyGroup != null)
                    {
                        Account.MyGroup.UseItemGroup(Account.Inventory.GetItemFromGID(Convert.ToInt32(m_delta)).UID);
                        //account.PutTaskDelay(3000);
                        Account.Wait(3000);
                    }
                    else
                    {
                        Account.Inventory.UseItem(Account.Inventory.GetItemFromGID(Convert.ToInt32(m_delta)).UID);
                    }
                    break;
                case "cell(":
                    if (Account.MyGroup != null)
                    {
                        Account.MyGroup.MoveToCellGroup(Convert.ToInt32(m_delta));
                        //account.PutTaskDelay(3000);
                        Account.Wait(3000);
                    }
                    else 
                    {
                        Account.Map.MoveToCell(Convert.ToInt32(m_delta));
                    }    
                    Account.Log(new BotTextInformation("Path: Moving to the cell " + Convert.ToString(m_delta)),5);
                    break;
                case "npc(":
                    if (Account.MyGroup != null)
                    {
                        Account.MyGroup.TalkToNpcGroup(Convert.ToInt32(m_delta));
                        //await account.PutTaskDelay(3000);
                        Account.Wait(3000);
                    }
                    else 
                    {
                        Account.Npc.TalkToNpc(Convert.ToInt32(m_delta));
                    }
                    break;
                    
                case "use(":
                    Account.Map.MoveToSecureElement(Convert.ToInt32(m_delta));
                    break;
                case "zaap(":
                    if (Account.MyGroup != null)
                    {
                        Account.MyGroup.UseZaapGroup();
                        //await account.PutTaskDelay(3000);
                        Account.Wait(3000);
                    }
                    else
                    {
                        Account.Map.UseZaapTo(Convert.ToInt32(Account.Path.Current_Action.m_delta));
                    }                       
                    break;
                case "zaapi(":
                    if (Account.MyGroup != null)
                    {
                        Account.MyGroup.UseZaapiGroup();
                        //await account.PutTaskDelay(3000);
                        Account.Wait(3000);
                    }
                    else
                    {
                        Account.Map.useZaapiTo(Convert.ToInt32(Account.Path.Current_Action.m_delta));
                    }
                    break;
                case "exchange(":
                    if (Account.MyGroup != null)
                    {
                        Account.MyGroup.RequestExchangeGroup((string)m_delta);
                        //await account.PutTaskDelay(3000);
                        Account.Wait(3000);
                    }
                    else
                    {
                        Account.Inventory.RequestExchange((string)m_delta);
                    }
                    break;
            }
            Account.WatchDog.Update();
        }
        #endregion

        #region Private Methods
        private string RandomDir(string delta)
        {
            string[] t = delta.Split(',');
            Random r = new Random();
            int pos = r.Next(0, t.Length);
            return t[pos];
        }
        #endregion
    }
}
