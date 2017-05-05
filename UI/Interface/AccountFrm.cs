namespace BlueSheep.Interface
{
    public partial class AccountFrm : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Container for AccountUC when there is only one account.
        /// </summary>

        #region Fields
        private string m_user;
        private string m_pass;
        private bool m_socket;
        public AccountUC UC { get; set; }
        #endregion

        private delegate void Callback();

        #region Constructors
        public AccountFrm()
        {
            InitializeComponent();
        }

        public AccountFrm(string username, string password, bool socket)
        {
            InitializeComponent();
            // Add the UC
            m_user = username;
            m_pass = password;
            m_socket = socket;
            Init();
        }

        public void Reconnect()
        {
            if (Controls[0].InvokeRequired)
            {
                Invoke(new Callback(Reconnect));
                return;
            }
            Controls.Remove(UC);
            Init();
        }

        private void Init()
        {
            AccountUC Uc = new AccountUC(m_user, m_pass, m_socket, this);
            Uc.DebugMode.Checked = true;
            UC = Uc;
            Controls.Add(Uc);
            Uc.Show();

            // Show the form
            Show();

            // Not in a group
            Uc.IsMaster = false;
            Uc.IsSlave = false;

            // Fill the account form
            Uc.Dock = DockStyle.Fill;

            // Call socket/mitm init
            if (m_socket)
                Uc.Init();
            else
                Uc.InitMITM();
        }

        private void SaveConfig(object sender , object e)
        {
            UC.ConfigManager.SaveConfig();
        }
        #endregion
    }
}

