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
                    if (Account.Config.IsMaster == true && Account.MyGroup != null)
                    {
                        Account.MyGroup.MoveGroup((string)m_delta);
                        //account.PutTaskDelay(3000);
                        Account.Wait(3000);
                    }
                    else if (Account.Config.IsSlave == false)
                    {
                        Account.Map.ChangeMap((string)m_delta);
                        //account.PutTaskDelay(3000);
                        Account.Wait(3000);
                    }
                    else
                    {
                        Account.Log(new ErrorTextInformation("Configuration error : This character is a islave and haven't any group."),0);
                    }
                    break;
                case "object(":
                    if (Account.Config.IsMaster == true && Account.MyGroup != null)
                    {
                        Account.MyGroup.UseItemGroup(Account.Inventory.GetItemFromGID(Convert.ToInt32(m_delta)).UID);
                        //account.PutTaskDelay(3000);
                        Account.Wait(3000);
                    }
                    else if (Account.Config.IsSlave == false)
                    {
                        Account.Inventory.UseItem(Account.Inventory.GetItemFromGID(Convert.ToInt32(m_delta)).UID);
                    }
                    else
                    {
                        Account.Log(new ErrorTextInformation("Configuration error : This character is a islave and haven't any group."), 0);
                    }
                    break;
                case "cell(":
                    if (Account.Config.IsMaster == true && Account.MyGroup != null)
                    {
                        Account.MyGroup.MoveToCellGroup(Convert.ToInt32(m_delta));
                        //account.PutTaskDelay(3000);
                        Account.Wait(3000);
                    }
                    else if (Account.Config.IsSlave == false)
                    {
                        Account.Map.MoveToCell(Convert.ToInt32(m_delta));
                    }
                    else
                    {
                        Account.Log(new ErrorTextInformation("Impossible d'enclencher le déplacement. (mûle ? plus d'objet ?)"), 0);
                    }       
                    Account.Log(new BotTextInformation("Trajet : Déplacement sur la cellule " + Convert.ToString(m_delta)),5);
                    break;
                case "npc(":
                    if (Account.Config.IsMaster == true && Account.MyGroup != null)
                    {
                        Account.MyGroup.TalkToNpcGroup(Convert.ToInt32(m_delta));
                        //await account.PutTaskDelay(3000);
                        Account.Wait(3000);
                    }
                    else if (Account.Config.IsSlave == false)
                    {
                        Account.Npc.TalkToNpc(Convert.ToInt32(m_delta));
                    }
                    else
                    {
                        Account.Log(new ErrorTextInformation("Impossible d'enclencher le dialogue. (mûle ?)"), 0);
                    } 
                    break;
                    
                case "use(":
                    Account.Map.MoveToSecureElement(Convert.ToInt32(m_delta));
                    break;
                case "zaap(":
                    if (Account.Config.IsMaster == true && Account.MyGroup != null)
                    {
                        Account.MyGroup.UseZaapGroup();
                        //await account.PutTaskDelay(3000);
                        Account.Wait(3000);
                    }
                    else if (Account.Config.IsSlave == false)
                    {
                        Account.Map.UseZaapTo(Convert.ToInt32(Account.Path.Current_Action.m_delta));
                    }
                    else
                    {
                        Account.Log(new ErrorTextInformation("Impossible d'enclencher le déplacement. (mûle ? plus d'objet ?)"), 0);
                    }                         
                    break;
                case "zaapi(":
                    if (Account.Config.IsMaster == true && Account.MyGroup != null)
                    {
                        Account.MyGroup.UseZaapiGroup();
                        //await account.PutTaskDelay(3000);
                        Account.Wait(3000);
                    }
                    else if (Account.Config.IsSlave == false)
                    {
                        Account.Map.useZaapiTo(Convert.ToInt32(Account.Path.Current_Action.m_delta));
                    }
                    else
                    {
                        Account.Log(new ErrorTextInformation("Impossible d'enclencher le déplacement. (mûle ? plus d'objet ?)"), 0);
                    } 
                    break;
                case "exchange(":
                    if (Account.Config.IsMaster == true && Account.MyGroup != null)
                    {
                        Account.MyGroup.RequestExchangeGroup((string)m_delta);
                        //await account.PutTaskDelay(3000);
                        Account.Wait(3000);
                    }
                    else if (Account.Config.IsSlave == false)
                    {
                        Account.Inventory.RequestExchange((string)m_delta);
                    }
                    else
                    {
                        Account.Log(new ErrorTextInformation("Impossible d'enclencher le dialogue. (mûle ?)"), 0);
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
