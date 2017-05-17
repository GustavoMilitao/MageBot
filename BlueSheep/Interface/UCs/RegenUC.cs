using Util.Util.I18n.Strings;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BlueSheep.Interface.UCs
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
            accUserControl.Account.Config.RegenConfig = new Core.Regen.Regen(accUserControl.Account);
            Init();
        }
        #endregion

        #region Public Methods
        public void PulseRegen()
        {
            accUserControl.Account.Config.RegenConfig.PulseRegen();
        }

        public void RefreshQuantity()
        {
            for (int i = 0; i < LVItems.Items.Count; i++)
            {
                try
                {
                    LVItems.Items[i].SubItems[1].Text = accUserControl.Account.Inventory.GetItemFromName(LVItems.Items[i].SubItems[0].Text).Quantity.ToString();
                    LVItems.Invalidate();
                }
                catch { }
            }
        }
        #endregion

        #region Private methods
        private void GetRegenItems()
        {
            var names = new List<string>();
            foreach (ListViewItem i in LVItems.Items)
                if (int.Parse(i.SubItems[1].Text) > 0)
                    names.Add(i.SubItems[0].Text);
            accUserControl.Account.Config.RegenConfig.GetRegenItemsByNames(names);
        }

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
        #endregion
    }
}
