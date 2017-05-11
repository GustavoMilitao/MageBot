using BlueSheep.Util.Text.Log;
using System;
using System.Linq;

namespace BlueSheep.Core.Path
{
    public class Action
    {
        #region Fields
        string m_action;
        public object m_delta;
        Account.Account account;
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
            account = Account;
        }
        #endregion

        #region Public Methods
        public async void PerformAction()
        {
            if (account.Config.Path == null)
                return;
            while (account.Busy == true)
                await account.PutTaskDelay(5);
            switch (m_action)
            {
                case "move(":
                    m_delta = RandomDir((string)m_delta);
                    if (account.Config.IsMaster == true && account.MyGroup != null)
                    {
                        account.MyGroup.MoveGroup((string)m_delta);
                        await account.PutTaskDelay(3000);
                    }
                    else if (account.Config.IsSlave == false)
                    {
                        account.Map.ChangeMap((string)m_delta);
                        await account.PutTaskDelay(3000);
                    }
                    else
                    {
                        account.Log(new ErrorTextInformation("Configuration error : This character is a islave and haven't any group."),0);
                    }
                    break;
                case "object(":
                    if (account.Config.IsMaster == true && account.MyGroup != null)
                    {
                        account.MyGroup.UseItemGroup(account.Inventory.GetItemFromGID(Convert.ToInt32(m_delta)).UID);
                        await account.PutTaskDelay(3000);
                    }
                    else if (account.Config.IsSlave == false)
                    {
                        account.Inventory.UseItem(account.Inventory.GetItemFromGID(Convert.ToInt32(m_delta)).UID);
                    }
                    else
                    {
                        account.Log(new ErrorTextInformation("Configuration error : This character is a islave and haven't any group."), 0);
                    }
                    break;
                case "cell(":
                    if (account.Config.IsMaster == true && account.MyGroup != null)
                    {
                        account.MyGroup.MoveToCellGroup(Convert.ToInt32(m_delta));
                        await account.PutTaskDelay(3000);
                    }
                    else if (account.Config.IsSlave == false)
                    {
                        await account.Map.MoveToCell(Convert.ToInt32(m_delta));
                    }
                    else
                    {
                        account.Log(new ErrorTextInformation("Impossible d'enclencher le déplacement. (mûle ? plus d'objet ?)"), 0);
                    }       
                    account.Log(new BotTextInformation("Trajet : Déplacement sur la cellule " + Convert.ToString(m_delta)),5);
                    break;
                case "npc(":
                    if (account.Config.IsMaster == true && account.MyGroup != null)
                    {
                        account.MyGroup.TalkToNpcGroup(Convert.ToInt32(m_delta));
                        await account.PutTaskDelay(3000);
                    }
                    else if (account.Config.IsSlave == false)
                    {
                        account.Npc.TalkToNpc(Convert.ToInt32(m_delta));
                    }
                    else
                    {
                        account.Log(new ErrorTextInformation("Impossible d'enclencher le dialogue. (mûle ?)"), 0);
                    } 
                    break;
                    
                case "use(":
                    account.Map.MoveToSecureElement(Convert.ToInt32(m_delta));
                    break;
                case "zaap(":
                    if (account.Config.IsMaster == true && account.MyGroup != null)
                    {
                        account.MyGroup.UseZaapGroup();
                        await account.PutTaskDelay(3000);
                    }
                    else if (account.Config.IsSlave == false)
                    {
                        account.Map.UseZaapTo(Convert.ToInt32(account.Config.Path.Current_Action.m_delta));
                    }
                    else
                    {
                        account.Log(new ErrorTextInformation("Impossible d'enclencher le déplacement. (mûle ? plus d'objet ?)"), 0);
                    }                         
                    break;
                case "zaapi(":
                    if (account.Config.IsMaster == true && account.MyGroup != null)
                    {
                        account.MyGroup.UseZaapiGroup();
                        await account.PutTaskDelay(3000);
                    }
                    else if (account.Config.IsSlave == false)
                    {
                        account.Map.useZaapiTo(Convert.ToInt32(account.Config.Path.Current_Action.m_delta));
                    }
                    else
                    {
                        account.Log(new ErrorTextInformation("Impossible d'enclencher le déplacement. (mûle ? plus d'objet ?)"), 0);
                    } 
                    break;
                case "exchange(":
                    if (account.Config.IsMaster == true && account.MyGroup != null)
                    {
                        account.MyGroup.RequestExchangeGroup((string)m_delta);
                        await account.PutTaskDelay(3000);
                    }
                    else if (account.Config.IsSlave == false)
                    {
                        account.Inventory.RequestExchange((string)m_delta);
                    }
                    else
                    {
                        account.Log(new ErrorTextInformation("Impossible d'enclencher le dialogue. (mûle ?)"), 0);
                    } 
                    break;
            }
            account.WatchDog.Update();
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
