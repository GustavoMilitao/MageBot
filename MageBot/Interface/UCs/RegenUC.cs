using Util.Util.I18n.Strings;
using System.Collections.Generic;
using System.Windows.Forms;
using MageBot.Core.Inventory;
using System.Windows.Input;

namespace MageBot.Interface.UCs
{
    public partial class RegenUC : MetroFramework.Controls.MetroUserControl
    {
        #region Fields
        private AccountUC accUserControl;
        #endregion

        #region Constructors
        public RegenUC(AccountUC Account)
        {
            InitializeComponent();
            accUserControl = Account;
            accUserControl.Account.Regen = new MageBot.Core.Regen.Regen(accUserControl.Account);
            Init();
        }
        #endregion

        #region Public Methods

        public void RefreshQuantity()
        {
            for (int i = 0; i < LVItems.Items.Count; i++)
            {
                try
                {
                    Item item = accUserControl.Account.Inventory.GetItemFromName(LVItems.Items[i].SubItems[0].Text);
                    if (item.Quantity == 0)
                    {
                        LVItems.Items.RemoveAt(i);
                    }
                    else
                    {
                        LVItems.Items[i].SubItems[1].Text = item.Quantity.ToString();
                    }
                    LVItems.Invalidate();
                }
                catch { }
            }
        }
        #endregion

        #region Private methods

        private void Init()
        {
            LVItems.Columns.Add(Strings.Name, -2, HorizontalAlignment.Center);
            LVItems.Columns.Add("Quantity", -2, HorizontalAlignment.Center);
        }

        #endregion

        #region UI Methods
        private void LVGestItems_TouchPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Delete)
            {
                for (int i = 0; i < LVItems.Items.Count; i++)
                {
                    if (LVItems.Items[i].Selected)
                        LVItems.Items.RemoveAt(i);
                }
            }
        }

        private void RegenUC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Key.Delete)
            {
                foreach (ListViewItem lvi in LVItems.SelectedItems)
                {
                    Item item = accUserControl.Account.Inventory.GetItemFromName(lvi.SubItems[0].Text);
                    if (item != null)
                    {
                        accUserControl.Account.Config.RegenItems.RemoveAll(it => it.UID == item.UID);
                        LVItems.Items.Remove(lvi);
                    }
                }
            }
        }
        #endregion

    }
}
