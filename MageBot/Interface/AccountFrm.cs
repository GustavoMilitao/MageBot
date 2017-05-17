using System.Windows.Forms;
namespace MageBot.Interface
{
    public partial class AccountFrm : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Container for AccountUC when there is only one account.
        /// </summary>

        #region Fields
        public AccountUC accUserControl { get; set; }
        #endregion

        private delegate void Callback();

        #region Constructors
        public AccountFrm()
        {
            InitializeComponent();
        }

        public AccountFrm(AccountUC accUserControl)
        {
            InitializeComponent();
            // Add the UC
            this.accUserControl = accUserControl;
            Init();
        }

        public void Reconnect()
        {
            if (Controls[0].InvokeRequired)
            {
                Invoke(new Callback(Reconnect));
                return;
            }
            Controls.Remove(accUserControl);
            Init();
        }

        private void Init()
        {
            accUserControl.DebugMode.Checked = true;
            Controls.Add(accUserControl);
            accUserControl.Show();

            // Show the form
            Show();

            // Not in a group
            accUserControl.Account.Config.IsMaster = false;
            accUserControl.Account.Config.IsSlave = false;

            // Fill the account form
            accUserControl.Dock = DockStyle.Fill;

            // Call socket/mitm init
            if (accUserControl.Account.Config.IsSocket)
                accUserControl.Init();
            else if(accUserControl.Account.Config.IsMITM)
                accUserControl.InitMITM();
        }

        private void SaveConfig(object sender , object e)
        {
            accUserControl.Account.Config.ConfigRecover.SaveConfig();
        }
        #endregion
    }
}

