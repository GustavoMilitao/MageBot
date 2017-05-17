using System;
using System.IO;

namespace MageBot.Interface
{
    public partial class DofusPathForm : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Selection form of the dofus path.
        /// </summary>

        #region Fields
        int flag = 0;
        MainForm mainfrm;
        #endregion

        #region Constructors
        public DofusPathForm(MainForm main)
        {
            InitializeComponent();
            mainfrm = main;
        }
        #endregion

        #region Interface methods
        private void BtValider_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                FolderBrowserDialog_DofusPath.ShowDialog();
                TextBox1.Text = FolderBrowserDialog_DofusPath.SelectedPath;
                BtValider.Text = "Continuer";
                flag = 1;
            }
            else
            {
                string directoryPath = Path.Combine (TextBox1.Text, "app", "data", "i18n");

                if (Directory.Exists(directoryPath))
                {
                    mainfrm.DofusPath = TextBox1.Text;
                    string combinedPath = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MageBot", "bs.conf");
                    StreamWriter sr = new StreamWriter(combinedPath);
                    sr.WriteLine(TextBox1.Text);
                    sr.Close();
                    Close();
                    return;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Impossible de trouver le dossier : \"" + directoryPath);
                    flag = 0;
                    BtValider.Text = "Parcourir";
                    TextBox1.Clear();
                }
            }
        }
        #endregion
    }
}
