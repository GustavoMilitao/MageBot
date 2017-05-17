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
            AccountUC Uc = new AccountUC(accUserControl.Account ,this);
            Uc.DebugMode.Checked = true;
            accUserControl = Uc;
            Controls.Add(Uc);
            Uc.Show();

            // Show the form
            Show();

            // Not in a group
            Uc.Account.Config.IsMaster = false;
            Uc.Account.Config.IsSlave = false;

            // Fill the account form
            Uc.Dock = DockStyle.Fill;

            // Call socket/mitm init
            if (Uc.Account.Config.IsSocket)
                Uc.Init();
            else if(Uc.Account.Config.IsMITM)
                Uc.InitMITM();
        }

        private void SaveConfig(object sender , object e)
        {
            accUserControl.Account.Config.ConfigRecover.SaveConfig();
        }
        #endregion
    }
}

