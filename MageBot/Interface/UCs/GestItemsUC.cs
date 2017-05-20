using MageBot.Util.Enums.Internal;
using Util.Util.I18n.Strings;
using Util.Util.Text.Log;
using MageBot.Core.Engine.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MageBot.Core.Inventory;

namespace MageBot.Interface.UCs
{
    public partial class GestItemsUC : MetroFramework.Controls.MetroUserControl
    {
        #region Fields
        AccountUC accUserControl { get; set; }
        System.Timers.Timer AutoDeletionTimer { get; set; }
        #endregion

        private delegate void AutoDeleteCallback(object sender, System.Timers.ElapsedEventArgs e);

        #region Constructors
        public GestItemsUC(AccountUC Account)
        {
            InitializeComponent();
            accUserControl = Account;
            accUserControl.Account.Inventory = new MageBot.Core.Inventory.Inventory(accUserControl.Account);
            LVGestItems.Columns.Add(Strings.Name, 200, HorizontalAlignment.Center);
            LVGestItems.Columns.Add(Strings.Action, 200, HorizontalAlignment.Center);
            ItemTxtBox.KeyUp += (s, e) =>
            {
                IntelliSense.AutoCompleteTextBox(ItemTxtBox, litPopup, IntelliSense.ItemsList, e);
            };
            AutoDeletionTimer = new System.Timers.Timer(Convert.ToDouble(NUDAutoDeletion.Value * 1000)) { AutoReset = false };
            AutoDeletionTimer.Elapsed += new System.Timers.ElapsedEventHandler(PerformAutoDeletion);
            if (AutoDeletionBox.Checked)
                AutoDeletionTimer.Start();
        }
        #endregion


        #region Public Methods

        public void FillRecoveredConfig()
        {
            NUDBank.Value = accUserControl.Account.Config.MaxPodsPercent;
            ListenerBox.Checked = accUserControl.Account.Config.ListeningToExchange;
            NUDAutoDeletion.Value = accUserControl.Account.Config.AutoDeletionTime;
            LVGestItems.Clear();
            //Automatic deletion
            //Remove from bank
            //Do not bank
            foreach (Item i in accUserControl.Account.Config.ItemsToAutoDelete)
            {
                string[] row = { i.Name, "Automatic deletion" };
                LVGestItems.Items.Add(
                    new ListViewItem(row));
            }
            foreach (Item i in accUserControl.Account.Config.ItemsToGetFromBank)
            {
                string[] row = { i.Name, "Remove from bank" };
                LVGestItems.Items.Add(
                    new ListViewItem(row));
            }
            foreach (Item i in accUserControl.Account.Config.ItemsToStayOnCharacter)
            {
                string[] row = { i.Name, "Do not bank" };
                LVGestItems.Items.Add(
                    new ListViewItem(row));
            }

        }

        #endregion

        #region Private Methods
        private void PerformAutoDeletion(object sender, System.Timers.ElapsedEventArgs e)
        {
            accUserControl.Account.Inventory.PerformAutoDeletion();
            Reset();
        }

        private void Reset()
        {
            AutoDeletionTimer.Dispose();
            AutoDeletionTimer = new System.Timers.Timer(Convert.ToDouble(NUDAutoDeletion.Value * 1000));
            AutoDeletionTimer.Elapsed += new System.Timers.ElapsedEventHandler(PerformAutoDeletion);
            if (AutoDeletionBox.Checked)
                AutoDeletionTimer.Start();
        }
        #endregion

        #region UI Methods
        private void AddBt_Click(object sender, EventArgs e)
        {
            if (ItemTxtBox.Text.Length > 0 && ActionChoiceCombo.SelectedItem != null)
            {
                //0 - automatic deletion, 1 - get from bank, 2 - do not bank
                ListViewItem item = new ListViewItem(new string[] { ItemTxtBox.Text, (string)ActionChoiceCombo.SelectedItem });
                Item i = accUserControl.Account.Inventory.GetItemFromName(ItemTxtBox.Text);
                if (i != null)
                {
                    switch (ActionChoiceCombo.SelectedIndex)
                    {
                        case 0: accUserControl.Account.Config.ItemsToAutoDelete.Add(i); break;
                        case 1: accUserControl.Account.Config.ItemsToGetFromBank.Add(i); break;
                        case 2: accUserControl.Account.Config.ItemsToStayOnCharacter.Add(i); break;
                    }
                }
                LVGestItems.Items.Add(item);
                ItemTxtBox.Text = "Enter the name of item...";
            }

        }

        private void NUDAutoDeletion_ValueChanged(object sender, EventArgs e)
        {
            AutoDeletionTimer = new System.Timers.Timer(Convert.ToDouble(NUDAutoDeletion.Value * 1000));
            AutoDeletionTimer.Elapsed += new System.Timers.ElapsedEventHandler(PerformAutoDeletion);
            if (AutoDeletionBox.Checked)
                AutoDeletionTimer.Start();
        }

        private void AutoDeletionBox_CheckedChanged(object sender)
        {
            if (AutoDeletionBox.Checked)
                AutoDeletionTimer.Start();
            else
                AutoDeletionTimer.Stop();
        }

        private void ItemTxtBox_GotFocus(object sender, EventArgs e)
        {
            if (ItemTxtBox.Text == "Enter the name of an item...")
                ItemTxtBox.Text = String.Empty;
        }

        private void ItemTxtBox_LostFocus(object sender, EventArgs e)
        {
            if (ItemTxtBox.Text == "")
                ItemTxtBox.Text = "Enter the name of an item...";
        }

        private void LVGestItems_TouchPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Delete)
            {
                for (int i = 0; i < LVGestItems.Items.Count; i++)
                {
                    if (LVGestItems.Items[i].Selected)
                    {
                        //Automatic deletion
                        //Remove from bank
                        //Do not bank
                        LVGestItems.Items.RemoveAt(i);
                        string itemName = LVGestItems.Items[i].SubItems[0].Text;
                        string action = LVGestItems.Items[i].SubItems[1].Text;
                        switch(action)
                        {
                            case "Automatic deletion":
                                accUserControl.Account.Config.ItemsToAutoDelete.RemoveAll(item => item.Name == itemName);
                                break;
                            case "Remove from bank":
                                accUserControl.Account.Config.ItemsToGetFromBank.RemoveAll(item => item.Name == itemName);
                                break;
                            case "Do not bank":
                                accUserControl.Account.Config.ItemsToStayOnCharacter.RemoveAll(item => item.Name == itemName);
                                break;
                        }
                    }
                }
            }
        }

        private void ListenerBox_CheckedChanged(object sender)
        {
            accUserControl.Account.Config.ListeningToExchange = ListenerBox.Checked;
        }

        #endregion
    }
}
