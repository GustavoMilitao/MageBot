using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BlueSheep.Interface
{
    public partial class GroupForm : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Container for multiple accountUC.
        /// </summary>

        #region Fields
        public List<AccountUC> listAccounts;
        public List<Account> list;
        #endregion

        private delegate void Callback(AccountUC account);

        #region Constructors
        public GroupForm(List<AccountUC> accounts, string name)
        {
            InitializeComponent();
            Text = name;
            list = new List<Account>();
            foreach (AccountUC account in accounts)
            {
                TabPage tab = new TabPage(account.AccountName);
                AccountUC naccount = new AccountUC(account.AccountName, account.AccountPassword, true, this);
                AccountTabs.TabPages.Add(tab);
                tab.Controls.Add(naccount);
                naccount.Dock = DockStyle.Fill;
                naccount.Show();
                naccount.MyGroup = new Group(accounts, Name);
                list.Add(new Account(account.AccountName, account.AccountPassword));
                naccount.Init();
            }
        }

        public void Reconnect(AccountUC account)
        {
            if (account.InvokeRequired)
            {
                Invoke(new Callback(Reconnect),account);
                return;
            }
            Controls.Remove(account);
            string user = account.AccountName;
            string pass = account.AccountPassword;
            account = new AccountUC(user, pass, true, this);
            Controls.Add(account);
            account.Show();
            Show();
            account.IsMaster = false;
            account.IsSlave = false;
            account.Dock = DockStyle.Fill;
            account.Init();
        }

        #endregion

        #region Public Methods
        public void AddMember(string name)
        {
            MasterChoice1.Items.Add(name);
        }
        #endregion

        #region Interface methods
        private void MasterChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < MasterChoice1.Items.Count; i++)
            {
                string item = (string)MasterChoice1.Items[i];
                foreach (AccountUC account in listAccounts)
                {
                    if (item == account.AccountName)
                    {
                        account.IsSlave = true;
                        account.IsMaster = false;
                        account.MyGroup = new Group(listAccounts, Name);
                    }
                }
                if (item == (string)MasterChoice1.SelectedItem)
                {
                    foreach (AccountUC account in listAccounts)
                    {
                        if (item == account.AccountName)
                        {
                            account.IsSlave = false;
                            account.IsMaster = true;
                            account.MyGroup = new Group(listAccounts, Name);
                            account.Log(new BotTextInformation("This is the Master account now !"), 1);
                            account.Focus();
                            foreach (AccountUC slave in listAccounts)
                            {
                                // Si le compte n'est pas le compte chef
                                if (account.AccountName != slave.AccountName)
                                {
                                    // On l'invite en groupe
                                    Invite(slave.CharacterBaseInformations.Name, account);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Dofus group methods
        private void Invite(string name, AccountUC account)
        {
            PartyInvitationRequestMessage msg = new PartyInvitationRequestMessage(name);
            account.SocketManager.Send(msg);
        }
        private void QuitGroup(int partyid, AccountUC account)
        {
            PartyLeaveRequestMessage msg = new PartyLeaveRequestMessage((uint)partyid);
            account.SocketManager.Send(msg);
        }
        #endregion
    }
}