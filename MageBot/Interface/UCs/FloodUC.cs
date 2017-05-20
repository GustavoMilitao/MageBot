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
            foreach (KeyValuePair<string, long> p in accUserControl.Account.Config.ListOfPlayersWithLevel)
            {
                PlayerListLb.Items.Add(String.Join(",", p.Key, p.Value));
            }
            accUserControl.Account.Config.FloodContent = String.Empty;
        }
        #endregion

        #region Public Methods
        public void AddItem(string line)
        {
            PlayerListLb.Items.Add(line);
        }

        public void FillInitialConfig()
        {
            FloodContentRbox.Text = accUserControl.Account.Config.FloodContent;
            CommerceBox.Checked = accUserControl.Account.Config.FloodInCommerceChannel;
            RecrutementBox.Checked = accUserControl.Account.Config.FloodInRecruitmentChannel;
            GeneralBox.Checked = accUserControl.Account.Config.FloodInGeneralChannel;
            PrivateEnterBox.Checked = accUserControl.Account.Config.FloodInPrivateChannel;
            IsRandomingSmileyBox.Checked = accUserControl.Account.Config.AddRandomingSmiley;
            IsRandomingNumberBox.Checked = accUserControl.Account.Config.AddRandomingNumber;
            NUDFlood.Value = accUserControl.Account.Config.FloodInterval;
            IsMemoryCheck.Checked = accUserControl.Account.Config.FloodSaveInMemory;
            foreach (KeyValuePair<string, long> player in accUserControl.Account.Flood.ListOfPlayersWithLevel)
            {
                PlayerListLb.Items.Add(player.Key + "," + player.Value);
            }
        }

        #endregion

        #region events
        private void RemovePlayerBt_Click(object sender, EventArgs e)
        {
            string pathPlayers = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MageBot", "Accounts", accUserControl.Account.AccountName, "Flood");
            if (File.Exists(pathPlayers + "\\Players.txt"))
            {
                DeleteLine(pathPlayers + "\\Players.txt", PlayerListLb.SelectedItem.ToString());
            }
            if (PlayerListLb.SelectedItem != null)
            {
                PlayerListLb.Items.Remove(PlayerListLb.SelectedItem);
            }
            string playerName = ((string)PlayerListLb.SelectedItem).Split(',')[0];
            if (accUserControl.Account.Flood.ListOfPlayersWithLevel.ContainsKey(playerName))
                accUserControl.Account.Flood.ListOfPlayersWithLevel.Remove(playerName);
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
                sw.Close();
            }
            accUserControl.Account.Flood.ListOfPlayersWithLevel.Clear();
        }

        private void FloodPlayersBt_Click(object sender, EventArgs e)
        {
            foreach (var elem in PlayerListLb.Items)
            {
                try
                {
                    string[] parsed = ((string)elem).Split(',');
                    accUserControl.Account.Config.FloodContent = accUserControl.Account.Config.FloodContent.Replace("%name%", parsed[0]).Replace("%level%", parsed[1]);
                    accUserControl.Account.Flood.SendPrivateTo(parsed[0], accUserControl.Account.Config.FloodContent);
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
                accUserControl.Account.Flood.FloodStarted = false;
                accUserControl.Account.Log(new BotTextInformation("Flood stopped"), 1);
                accUserControl.Account.Log(new BotTextInformation(accUserControl.Account.Flood.PMCount + " PMs sent."), 0);
                accUserControl.Account.Log(new BotTextInformation(accUserControl.Account.Flood.MessageCount + " Other messages sent."), 0);
                accUserControl.Account.Flood.PMCount = 0;
                accUserControl.Account.Flood.MessageCount = 0;
                return;
            }
            accUserControl.Account.Log(new BotTextInformation("Flood activated"), 1);
            accUserControl.Account.Flood.StartFlood();
        }

        private void CommerceBox_CheckedChanged(object sender)
        {
            accUserControl.Account.Config.FloodInCommerceChannel = CommerceBox.Checked;
        }

        private void RecrutementBox_CheckedChanged(object sender)
        {
            accUserControl.Account.Config.FloodInRecruitmentChannel = RecrutementBox.Checked;
        }

        private void GeneralBox_CheckedChanged(object sender)
        {
            accUserControl.Account.Config.FloodInGeneralChannel = GeneralBox.Checked;
        }

        private void PrivateEnterBox_CheckedChanged(object sender)
        {
            accUserControl.Account.Config.FloodInPrivateChannel = PrivateEnterBox.Checked;
        }

        private void IsRandomingSmileyBox_CheckedChanged(object sender)
        {
            accUserControl.Account.Config.AddRandomingSmiley = IsRandomingSmileyBox.Checked;
        }

        private void IsRandomingNumberBox_CheckedChanged(object sender)
        {
            accUserControl.Account.Config.AddRandomingNumber = IsRandomingNumberBox.Checked;
        }

        private void NUDFlood_ValueChanged(object sender, EventArgs e)
        {
            accUserControl.Account.Config.FloodInterval = (int)NUDFlood.Value;
        }

        private void IsMemoryCheck_CheckedChanged(object sender)
        {
            accUserControl.Account.Config.FloodSaveInMemory = IsMemoryCheck.Checked;
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
