using BlueSheep.Core.Account;
using BlueSheep.Core.Groups;
using BlueSheep.Util.I18n.Strings;
using Core.Storage.AccountsManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace BlueSheep.Interface
{
    public partial class GestGroupe : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Group manager.
        /// </summary>

        #region Fields
        List<Group> groups = new List<Group>();
        #endregion

        #region Constructors
        public GestGroupe()
        {
            InitializeComponent();
            LoadGroups();
            LoadAccounts();
            listViewAccounts.HideSelection = true;
            listViewGroups.HideSelection = true;
            listViewAccounts.Columns.Add(Strings.Name, 200, HorizontalAlignment.Center);
            listViewAccounts.Columns.Add(Strings.Password, 0, HorizontalAlignment.Center);
            listViewGroups.Columns.Add(Strings.Name, -2, HorizontalAlignment.Center);
            DelBt.Text = Strings.Delete;
            LaunchGroupsSelecBt.Text = Strings.LaunchSelectedGroups;
            Text = Strings.GroupsManager;
            sadikTabControl1.TabPages[0].Text = Strings.Launch;
            sadikTabControl1.TabPages[1].Text = Strings.Create;
            AddBt.Text = Strings.Create;
        }
        #endregion

        #region Interface methods
        private void LaunchGroupsSelecBt_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewGroups.SelectedItems)
            {
                Group group = SearchGroups(item.Text);
                GroupForm frm = new GroupForm(group.accounts, group.name);
                foreach (Account account in group.accounts)
                {
                    account.Init();
                }
                frm.Show();
                MainForm.ActualMainForm.AddForm(frm);
            }
            Close();
        }

        private void DelBt_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGroups.SelectedItems.Count; i++)
            //parcours des groupes sélectionnés
            {
                ListViewItem listViewItem2 = listViewGroups.SelectedItems[i];
                string ApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string combinedPath = System.IO.Path.Combine(ApplicationDataPath, "BlueSheep", "Groups", listViewItem2.Text + ".bs");
                File.Delete(combinedPath);
                listViewGroups.Items.Remove(listViewItem2);
            }
        }

        private void AddBt_Click(object sender, EventArgs e)
        {
            if (listViewAccounts.SelectedItems.Count > 0)
            {
                List<Account> listaccounts = new List<Account>();
                foreach (ListViewItem account in listViewAccounts.SelectedItems)
                {
                    listaccounts.Add(new Account(account.SubItems[0].Text, account.SubItems[1].Text, false));
                }
                if (NameBox.Text.Length > 0)
                {
                    AccountsFileInteractions accountsFileInteractions = new AccountsFileInteractions();
                    accountsFileInteractions.SaveGroup(listaccounts, NameBox.Text + ".bs");
                    listViewGroups.Items.Add(NameBox.Text);
                    groups.Add(new Group(listaccounts, NameBox.Text));
                    listViewAccounts.SelectedItems.Clear();
                    NameBox.Clear();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Merci de rajouter un nom pour le groupe. Merci pour eux. :3");
                }
            }
        }
        #endregion

        #region Private methods
        private Group SearchGroups(string groupname)
        {
            foreach (Group group in groups)
            {
                if (group.name == groupname)
                    return group;
            }
            return null;
        }

        private void LoadGroups()
        {
            AccountsFileInteractions accountsFileInteractions = new AccountsFileInteractions();

            accountsFileInteractions.RecoverGroups();

            foreach (Group Groupobject in accountsFileInteractions.Groups)
            {
                string[] row1 = { Groupobject.name };
                ListViewItem li = new ListViewItem(row1);
                listViewGroups.Items.Add(li);
                groups.Add(Groupobject);
            }
        }



        private void LoadAccounts()
        {
            AccountsFileInteractions accountsFileInteractions = new AccountsFileInteractions();
            accountsFileInteractions.RecoverAccountsInfos();
            foreach (Account accountObject in accountsFileInteractions.Accounts)
            {
                string[] row1 = { accountObject.AccountName, accountObject.AccountPassword};
                ListViewItem li = new ListViewItem(row1);
                listViewAccounts.Items.Add(li);
            }
        }

        #endregion
    }
}
