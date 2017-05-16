using BlueSheep.Core.Fight;
using BlueSheep.Util.I18n.Strings;
using BlueSheep.Util.Text.Log;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BlueSheep.Interface
{
    public partial class IAChoice : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// AI Choice form.
        /// </summary>

        #region Fields
        AccountUC accUserControl;
        #endregion

        #region Constructors
        public IAChoice(AccountUC account)
        {
            InitializeComponent();
            accUserControl = account;
            Text = Strings.AIManager;
            DelBt.Text = Strings.Delete;
            LoadBt.Text = Strings.Load;
            AddBt.Text = Strings.Add;
            FilesList.Columns[0].Text = Strings.Name ;
            FilesList.Columns[1].Text = Strings.Author;
            FilesList.Columns[2].Text = Strings.Breed;

            Init();
        }
        #endregion

        #region Public methods
        public void LoadIA(FileInfo fi)
        {
            StreamReader sr = new StreamReader(fi.FullName);
            string line = null;
            string[] infos = new string[6];
            for (int i = 0; i < 5; i++)
            {
                line = sr.ReadLine();
                if (line == null || line.Length < 5)
                {
                    MessageBox.Show("Malformated header. Check your AI's syntax. " + fi.FullName);
                    return;
                }
                line.Replace(" ", "");
                switch (i)
                {
                    case 0:
                        infos[i] = line.Remove(0, 5).Trim();
                        //enleve @NAME
                        break;
                    case 1:
                        infos[i] = line.Remove(0, 6).Trim();
                        //@BREED
                        break;
                    case 2:
                        infos[i] = line.Remove(0, 8).Trim();
                        //enleve @VERSION
                        break;
                    case 3:
                        infos[i] = line.Remove(0, 7).Trim();
                        //enleve @AUTHOR
                        break;
                    case 4:
                        infos[i] = line.Remove(0, 12).Trim();
                        //enleve @DESCRIPTION
                        break;
                }
            }
            infos[5] = fi.FullName;
            sr.Close();
            FilesList.Items.Add(infos[0]).SubItems.AddRange(new string[] {
        infos[1],
        infos[2],
        infos[3],
        infos[4],
        infos[5]
    });
        }
        #endregion

        #region Méthodes d'interface
        private void LoadBt_Click(object sender, EventArgs e)
        {
            LaunchIA();
        }

        private void DelBt_Click(object sender, EventArgs e)
        {
            if (FilesList.SelectedItems.Count > 0)
            {
                string path = FilesList.SelectedItems[0].SubItems[4].Text;
                File.Delete(path);
                FilesList.Items.Remove(FilesList.SelectedItems[0]);
            }

        }

        private void AddBt_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string combinedPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlueSheep", "IAs", openFileDialog1.SafeFileName);
                if (!File.Exists(combinedPath))
                {
                    File.Copy(openFileDialog1.FileName, combinedPath);
                    FilesList.Items.Clear();
                    Init();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Le trajet a déjà été ajouté !");
                }
            }
        }
        #endregion

        #region Private Methods
        private void Init()
        {
            string combinedPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlueSheep", "IAs");
            openFileDialog1.InitialDirectory = combinedPath;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(combinedPath);
            if (di.GetFiles().Count() == 0)
            {
                System.Windows.Forms.MessageBox.Show("Aucune IA, veuillez en télécharger sur le forum ou créer le vôtre :) ");
            }
            else
            {
                foreach (System.IO.FileInfo file in di.GetFiles())
                {
                    LoadIA(file);
                }
            }
        }

        private void LaunchIA()
        {
            if (FilesList.SelectedItems.Count > 0 && accUserControl != null)
            {
                accUserControl.Account.Config.FightParser = new FightParser(accUserControl.Account, FilesList.SelectedItems[0].SubItems[5].Text, FilesList.SelectedItems[0].SubItems[0].Text);
                accUserControl.Log(new BotTextInformation("AI loaded : " + FilesList.SelectedItems[0].Text), 0);
                accUserControl.Account.Config.FightParser.Name = FilesList.SelectedItems[0].Text;
                accUserControl.NomIA.Text = accUserControl.Account.Config.FightParser.Name;
                accUserControl.Account.Fight = new BFight(accUserControl.Account, accUserControl.Account.Config.FightParser, accUserControl.Account.FightData);
                Close();
            }
        }
        #endregion

    }
}
