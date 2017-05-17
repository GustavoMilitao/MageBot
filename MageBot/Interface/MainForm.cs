using Util.Util.I18n.Strings;
using MageBot.Core.Engine.Constants;
using MageBot.DataFiles.Data.D2o;
using MageBot.DataFiles.Data.D2p;
using MageBot.DataFiles.Data.I18n;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MageBot.Interface
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Main form.
        /// </summary>

        #region Fields
        private string m_DofusPath;
        #endregion

        #region Properties
        public string DofusPath
        {
            get { return m_DofusPath; }
            set { m_DofusPath = value; }
        }
        public static MainForm ActualMainForm { get; set; }
        public static AccountFrm AccFrm { get; set; }
        #endregion

        #region Constructors
        public MainForm(string version)
        {
            InitializeComponent();
            ActualMainForm = this;
            Text = "MageBot " + version;
            CheckMageBotDatas();
            Show();
        }
        #endregion

        #region Public methods
        
        public void AddForm(Form frm)
        {
            // Add the form in the main form using MDI layers.
                if (((((this != null)) && !IsDisposed) && (((frm != null)) && !frm.IsDisposed)))
                {
                    frm.MdiParent = this;
                }

        }
        #endregion

        #region Interface methods

        private void AccountsBt_Click(object sender, EventArgs e)
        {
            GestAccounts frm = new GestAccounts(this);
            frm.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void GroupsBt_Click(object sender, EventArgs e)
        {
            GestGroupe frm = new GestGroupe();
            frm.Show();
        }

        private void LanguageChoice_SelectedTextChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(LanguageChoice.Text);
            AccountsBt.Text = Strings.Accounts;
            GroupsBt.Text = Strings.Groups;
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(3000);
                ShowInTaskbar = false;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            notifyIcon.Visible = false;
        }
        #endregion

        #region Private methods
        private static void CheckMageBotDatas()
        {
            // Create the MageBot.needed folders
            string applicationDataPath = Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData);
            string mageBotPath = Path.Combine (applicationDataPath, "MageBot");
            if (!Directory.Exists(mageBotPath))
                Directory.CreateDirectory(mageBotPath);
            if (!Directory.Exists(Path.Combine(mageBotPath, "Accounts")))
                Directory.CreateDirectory(Path.Combine(mageBotPath, "Accounts")).Attributes = FileAttributes.Normal;
            if (!Directory.Exists(Path.Combine(mageBotPath, "Groups")))
                Directory.CreateDirectory(Path.Combine(mageBotPath, "Groups")).Attributes = FileAttributes.Normal;
            if (!Directory.Exists(Path.Combine(mageBotPath, "Temp")))
                Directory.CreateDirectory(Path.Combine(mageBotPath, "Temp")).Attributes = FileAttributes.Normal;
            if (!Directory.Exists(Path.Combine(mageBotPath, "Paths")))
                Directory.CreateDirectory(Path.Combine(mageBotPath, "Paths")).Attributes = FileAttributes.Normal;
            if (!Directory.Exists(Path.Combine(mageBotPath, "IAs")))
                Directory.CreateDirectory(Path.Combine(mageBotPath, "IAs")).Attributes = FileAttributes.Normal;
            if (!Directory.Exists(Path.Combine(mageBotPath, "Logs")))
                Directory.CreateDirectory(Path.Combine(mageBotPath, "Logs")).Attributes = FileAttributes.Normal;

            string bsConfPath = Path.Combine (mageBotPath, "mb.conf");
            if (File.Exists(bsConfPath))
            {
                StreamReader sr = new StreamReader(bsConfPath);
                string path = sr.ReadLine();
                if (Directory.Exists(Path.Combine(path, "app", "content", "maps")))
                    ActualMainForm.DofusPath = path;
                else
                {
                    sr.Close();
                    DofusPathForm frm = new DofusPathForm(ActualMainForm);             
                    frm.ShowDialog();
                }
                
            }
            else
            {
                DofusPathForm frm = new DofusPathForm(ActualMainForm);
                frm.ShowDialog();
            }


            FileInfo fileInfo = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MageBot\Logs.txt");
            fileInfo.Delete();
            using (fileInfo.Create())
            {
            }

            //fileInfo = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MageBot.Packets.txt");
            //fileInfo.Delete();
            //using (fileInfo.Create())
            //{
            //}


            I18NFileAccessor i18NFileAccessor = new I18NFileAccessor();

            if (File.Exists(@"C:\Program Files (x86)\Dofus2\app\data\i18n\i18n_fr.d2i"))
            {
                string path = @"C:\Program Files (x86)\Dofus2\app\data\i18n\i18n_fr.d2i";
                i18NFileAccessor.Init(path);
                I18N i18N = new I18N(i18NFileAccessor);
                GameData.Init(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
                    + @"\Dofus2\app\data\common");
                MapsManager.Init(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
                    + @"\Dofus2\app\content\maps");
            }
            else if (File.Exists(bsConfPath))
            {
                List<string> PaysList = new List<string>();
                PaysList.AddRange(new List<string>() { "fr", "en", "ja", "es", "de", "pt" });
                foreach (string pays in PaysList)
                {
                    string combinedPath = Path.Combine(ActualMainForm.DofusPath, "app", "data", "i18n", "i18n_" + pays + ".d2i");
                    if (File.Exists(combinedPath))
                    {
                        i18NFileAccessor.Init(combinedPath);
                        break;
                    }
                }
                I18N i18N = new I18N(i18NFileAccessor);
                GameData.Init(Path.Combine (ActualMainForm.DofusPath, "app","data", "common"));
                MapsManager.Init(Path.Combine(ActualMainForm.DofusPath, "app", "content", "maps"));
            }
            //else
            //{
            //    i18NFileAccessor.Init(Path.Combine(ActualMainForm.DofusPath, "app", "data", "i18n", "i18n_fr.d2i"));
            //    I18N i18N = new I18N(i18NFileAccessor);
            //    GameData.Init(@"D:\Dofus2\app\data\common");
            //    MapsManager.Init(@"D:\Dofus2\app\content\maps");
            //}
            IntelliSense.InitMonsters();
            IntelliSense.InitItems();
            IntelliSense.InitServers();
        }






        #endregion
    }
}