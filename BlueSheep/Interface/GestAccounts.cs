using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BlueSheep.Common.Types;
using BlueSheep.AccountsManager;
using System.Diagnostics;
using BlueSheep.Util.I18n.Strings;

namespace BlueSheep.Interface
{
    public partial class GestAccounts : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Account manager.
        /// </summary>

        #region Fields
        MainForm m_Form;
        #endregion

        #region Constructors
        public GestAccounts(MainForm mainfrm)
        {
            InitializeComponent();
            listViewAccounts.Columns.Add(Strings.Name, 200, HorizontalAlignment.Center);
            listViewAccounts.Columns.Add(Strings.Password, 0, HorizontalAlignment.Center);
            DelBt.Text = Strings.Delete;
            LaunchAccountsSelecBt.Text = Strings.LaunchSelectedAccounts;
            Text = Strings.AccountsManager;
            sadikTabControl1.TabPages[0].Text = Strings.Launch;
            sadikTabControl1.TabPages[1].Text = Strings.Add;
            AddBt.Text = Strings.Add;
            sadikLabel1.Text = Strings.Name;
            sadikLabel2.Text = Strings.Password;
            listViewAccounts.Columns[1].Width = 0;

            LoadAccounts();
            m_Form = mainfrm;
        }
        #endregion

        #region Interface methods

        private void LaunchAccountsSelecBt_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem account in listViewAccounts.SelectedItems)
            {
                if (!IsMITM.Checked)
                {
                    AccountFrm frm = new AccountFrm(account.SubItems[0].Text, account.SubItems[1].Text, true);
                    frm.Show();
                    MainForm.AccFrm = frm;
                    MainForm.ActualMainForm.AddForm(frm);
                }
                else
                {
                    Process proc = new Process();
                    string directoryPath = System.IO.Path.Combine(MainForm.ActualMainForm.DofusPath, "app", "Dofus.exe");
                    proc.StartInfo.FileName = directoryPath;
                    proc.Start();
                    AccountFrm frm = new AccountFrm(account.SubItems[0].Text, account.SubItems[1].Text, false);
                    frm.Show();
                    MainForm.ActualMainForm.AddForm(frm);
                    MainForm.AccFrm = frm;
                }
            }
            Close();
        }
        private void buttonAddAccount_Click(object sender, EventArgs e)
        {
            if ((textBoxAccountName.Text == string.Empty) || (textBoxAccountName.Text == "Entrez le nom de compte..."))
                MessageBox.Show("Merci d'entrer un nom de compte.", "BS Error");
            else if ((textBoxPassword.Text == string.Empty) || (textBoxPassword.Text == "Entrez le mot de passe..."))
                MessageBox.Show("Merci d'entrer un mot de passe.", "BS Error");
            else
            {
                foreach (ListViewItem bot in listViewAccounts.Items)
                {
                    if (bot.Text == textBoxAccountName.Text)
                    {
                        MessageBox.Show("Ce compte a déjà été ajouté.", "BS Error");
                        textBoxAccountName.Text = string.Empty;
                        textBoxPassword.Text = string.Empty;
                        return;
                    }
                }
                string[] row1 = { textBoxAccountName.Text, textBoxPassword.Text };
                ListViewItem li = new ListViewItem(row1);
                listViewAccounts.Items.Add(li);
                AccountsFileInteractions accountsFileInteractions = new AccountsFileInteractions();
                List<Bot> listaccount = new List<Bot>();
                foreach (ListViewItem item in listViewAccounts.Items)
                {
                    listaccount.Add(new Bot(new Account(item.SubItems[0].Text, item.SubItems[1].Text)));
                }
                accountsFileInteractions.SaveAccountsInfos(listaccount);
                textBoxAccountName.Text = string.Empty;
                textBoxPassword.Text = string.Empty;
                listViewAccounts.Items[0].Focused = true;
                listViewAccounts.Items[0].Selected = true;
            }
        }
        private void textBoxPassword_GotFocus(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == Strings.Password)
            {
                // Vide la TextBox de commande
                textBoxPassword.Text = string.Empty;
                // Change la mise en forme
                textBoxPassword.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
        }
        private void textBoxPassword_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonAddAccount_Click(null, null);
        }
        private void textBoxPassword_LostFocus(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "")
            {
                // Réinitialise le text
                textBoxPassword.Text = Strings.Password;
                // Réinitialise la mise en forme
                textBoxPassword.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            }
        }
        private void textBoxAccountName_GotFocus(object sender, EventArgs e)
        {
            if (textBoxAccountName.Text == Strings.NewCellidOf)
            {
                // Vide la TextBox de commande
                textBoxAccountName.Text = string.Empty;
                // Change la mise en forme
                textBoxAccountName.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
        }
        private void textBoxAccountName_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Touche "Enter" saisie
                buttonAddAccount_Click(null, null);
        }
        private void textBoxAccountName_LostFocus(object sender, EventArgs e)
        {
            if (textBoxAccountName.Text == "")
            {
                // Réinitialise le text
                textBoxAccountName.Text = Strings.Name;
                // Réinitialise la mise en forme
                textBoxAccountName.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            }
        }
        private void DelBt_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewAccounts.SelectedItems.Count; i++)
            //parcours des comptes sélectionnés
            {
                ListViewItem listViewItem2 = listViewAccounts.SelectedItems[i];
                // Sauvegarde des comptes
                AccountsFileInteractions accountsFileInteractions = new AccountsFileInteractions();
                List<Bot> listaccount = new List<Bot>();
                foreach (ListViewItem item in listViewAccounts.Items)
                {
                    listaccount.Add(new Bot(new Account(item.SubItems[0].Text, item.SubItems[1].Text)));
                }
                accountsFileInteractions.SaveAccountsInfos(listaccount);
                // suppression de l'interface
                listViewAccounts.Items.Remove(listViewItem2);
            }
        }

        private void IsMITM_CheckedChanged(object sender)
        {
            MessageBox.Show("La fonction MITM est encore experimentale.");
        }

        #endregion

        #region Private methods
        private void LoadAccounts()
        {
            AccountsFileInteractions accountsFileInteractions = new AccountsFileInteractions();
            accountsFileInteractions.RecoverAccountsInfos();
            foreach (Account accountObject in accountsFileInteractions.Accounts)
            {
                string[] row1 = { accountObject.Name, accountObject.Password };
                ListViewItem li = new ListViewItem(row1);
                listViewAccounts.Items.Add(li);
            }
        }
        #endregion


    }
}