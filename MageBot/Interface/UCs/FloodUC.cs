using Util.Util.Text.Log;
using System;
using System.IO;
using System.Collections.Generic;

namespace MageBot.Interface.UCs
{
    public partial class FloodUC : MetroFramework.Controls.MetroUserControl
    {
        #region Fields
        private AccountUC accUserControl { get; set; }
        #endregion

        #region Constructors
        public FloodUC(AccountUC account)
        {
            InitializeComponent();
            accUserControl = account;
            accUserControl.Account.Flood = new MageBot.Core.Misc.Flood(accUserControl.Account);
            foreach(KeyValuePair<string, long> p in accUserControl.Account.Config.ListOfPlayersWithLevel)
            {
                PlayerListLb.Items.Add(String.Join(",", p.Key, p.Value));
            }
            PrivateExitBox.Hide();
            accUserControl.Account.Config.FloodContent = String.Empty;
        }
        #endregion

        #region Public Methods
        public void AddItem(string line)
        {
           PlayerListLb.Items.Add(line);
        }

        public void Increase(bool pm)
        {
            accUserControl.Account.Flood.increase(pm);
        }
        #endregion

        #region events
        private void RemovePlayerBt_Click(object sender, EventArgs e)
        {

            string pathPlayers = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MageBot", "Accounts", accUserControl.Account.AccountName, "Flood");
            if (File.Exists(pathPlayers + "\\Players.txt"))
            {
                DeleteLine(pathPlayers + "\\Players.txt", PlayerListLb.SelectedItem.ToString());
            }
            if (PlayerListLb.SelectedItem != null)
            {
                PlayerListLb.Items.Remove(PlayerListLb.SelectedItem);
            }
        }

        private void FloodContentRbox_TextChanged(object sender, EventArgs e)
        {
            accUserControl.Account.Config.FloodContent = FloodContentRbox.Text;
        }

        private void ClearListeBt_Click(object sender, EventArgs e)
        {
            if (PlayerListLb.Items.Count != 0)
            {
                PlayerListLb.Items.Clear();
            }
            string pathPlayers = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MageBot", "Accounts", accUserControl.Account.AccountName, "Flood");
            if (File.Exists(pathPlayers + "\\Players.txt"))
            {
                var sw = new StreamWriter(pathPlayers + "\\Players.txt");
                //sw.Write("");
                sw.Close();
            }
        }

        private void FloodPlayersBt_Click(object sender, EventArgs e)
        {
            accUserControl.Account.Config.FloodContent = FloodContentRbox.Text;
            foreach (var elem in PlayerListLb.Items)
            {
                try
                {
                    string[] parsed = ((string)elem).Split(',');
                    accUserControl.Account.Config.FloodContent = accUserControl.Account.Config.FloodContent.Replace("%name%", parsed[0]).Replace("%level%", parsed[1]);
                    accUserControl.Account.Flood.SendPrivateTo((string)parsed[0], accUserControl.Account.Config.FloodContent);
                }
                catch (Exception)
                {
                    accUserControl.Account.Log(new ErrorTextInformation("Impossible send message to : " + (string)elem), 3);
                }
            }
        }

        private void StartStopFloodingBox_CheckedChanged(object sender)
        {
            if (StartStopFloodingBox.Checked == false)
            {
                accUserControl.Account.Config.FloodStarted = false;
                accUserControl.Account.Log(new BotTextInformation("Flood stopped"), 1);
                accUserControl.Account.Log(new BotTextInformation(accUserControl.Account.Flood.PMCount + " PMs sent."), 0);
                accUserControl.Account.Log(new BotTextInformation(accUserControl.Account.Flood.MessageCount + " Other messages sent."), 0);
                accUserControl.Account.Flood.PMCount = 0;
                accUserControl.Account.Flood.MessageCount = 0;
                return;
            }
            accUserControl.Account.Log(new BotTextInformation("Flood activated"), 1);
            if (CommerceBox.Checked)
                accUserControl.Account.Flood.StartFlood(5, IsRandomingSmileyBox.Checked, IsRandomingNumberBox.Checked, FloodContentRbox.Text, (int)NUDFlood.Value);
            if (RecrutementBox.Checked)
                accUserControl.Account.Flood.StartFlood(6, IsRandomingSmileyBox.Checked, IsRandomingNumberBox.Checked, FloodContentRbox.Text, (int)NUDFlood.Value);
            if (GeneralBox.Checked)
                accUserControl.Account.Flood.StartFlood(0, IsRandomingSmileyBox.Checked, IsRandomingNumberBox.Checked, FloodContentRbox.Text, (int)NUDFlood.Value);
        }
        #endregion

        #region Private Methods
        private void DeleteLine(string path, string ligne)
        {

            string texte = null;
            string ligneActuelle = null;
            StreamReader sr = new StreamReader(path);

            while ((sr.Peek() != -1))
            {
                ligneActuelle = sr.ReadLine();
                if (!(ligneActuelle == ligne))
                {
                    texte = (texte + (ligneActuelle + "\r\n"));
                }
            }
            sr.Close();

            File.WriteAllText(path, texte);
        }
        #endregion


    }
}
