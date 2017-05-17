using BlueSheep.Util.Enums.Internal;
using Util.Util.I18n.Strings;
using Util.Util.Text.Log;
using Core.Engine.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BlueSheep.Interface.UCs
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
            accUserControl.Account.Inventory = new Core.Inventory.Inventory(accUserControl.Account);
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
        public List<int> GetItemsToGetFromBank()
        {
            return accUserControl.Account.Inventory.ItemsToGetFromBank.Select(i => i.UID).ToList();
        }

        public List<int> GetItemsToTransfer()
        {
            return accUserControl.Account.Inventory.GetItemsToTransfer();
        }
        #endregion

        #region PrivateMethods
        private void PerformAutoDeletion(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (LVGestItems.InvokeRequired)
            {
                Invoke(new AutoDeleteCallback(PerformAutoDeletion), sender, e);
                return;
            }
            if (accUserControl.Account.State == Status.Fighting)
            {
                accUserControl.Account.Log(new ErrorTextInformation("Automatic deletion can not be performed in battle. Restarting counter"), 2);
                Reset();
                return;
            }
            if (LVGestItems.Items.Count > 0)
            {
                foreach (ListViewItem item in LVGestItems.Items)
                {
                    if (item.SubItems[1].Text == "Suppression automatique")
                    {
                        Core.Inventory.Item i = accUserControl.Account.Inventory.GetItemFromName(item.SubItems[0].Text);
                        if (i != null)
                            accUserControl.Account.Inventory.DeleteItem(i.UID, i.Quantity);
                    }
                }
                Reset();
            }
            else
                Reset();
        }

        private List<int> GetItemsNoBank()
        {
            return accUserControl.Account.Inventory.ItemsToGetFromBank.Select(i => i.UID).ToList();
        }
        #endregion

        #region UI Methods
        private void AddBt_Click(object sender, EventArgs e)
        {
            if (ItemTxtBox.Text.Length > 0 && ActionChoiceCombo.SelectedItem != null)
            {
                ListViewItem item = new ListViewItem(new string[] { ItemTxtBox.Text, (string)ActionChoiceCombo.SelectedItem });
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
                        LVGestItems.Items.RemoveAt(i);
                }
            }
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


    }
}
