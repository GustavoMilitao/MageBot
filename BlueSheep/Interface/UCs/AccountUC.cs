using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BlueSheep.Util.Text.Log;
using BlueSheep.Engine.Network;
using System.Threading;
using BlueSheep.Core.Job;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using BlueSheep.Util.I18n.Strings;
using BlueSheep.Core.Account;
using BlueSheep.Core.Pets;
using BlueSheep.Core.Network;
using BlueSheep.Util.Enums.Internal;
using BlueSheep.Common.Data;
using BlueSheep.Protocol;
using BlueSheep.Protocol.Types.Game.Context.Roleplay;
using BlueSheep.Common.Data.D2o;
using BlueSheep.Protocol.Messages.Game.Inventory.Exchanges;
using BlueSheep.Protocol.Messages.Game.Dialog;
using BlueSheep.Protocol.Types.Game.Data.Items;

namespace BlueSheep.Interface
{
    public partial class AccountUC : MetroFramework.Controls.MetroUserControl
    {
        /// <summary>
        /// Main UC
        /// </summary>

        #region Fields
        public Account Account { get; set; }
        #endregion

        #region Delegates
        private delegate void SetLogsCallback(TextInformation textInformations, int levelVerbose);
        private delegate void ActualizeAccountInformationsCallback();
        private delegate void SetLicenceCallback(bool response, string text);
        private delegate void DelegBar(int Bar, int Max, int value, string text);
        private delegate void DelegListView(ListViewItem delta, ListView gamma);
        private delegate void DelegLabel(string text, Label lab);
        private delegate void DelegBool(bool param1);
        private delegate void DelegGatherPie(Dictionary<string, int> ressources, Dictionary<DateTime, int> xp);
        private delegate bool DelegVerifGroup(List<string> monsters);
        private delegate bool BoolCallback();
        private delegate void Callback();
        #endregion

        #region Constructeurs

        public AccountUC(string username, string password, bool socket)
        {
            InitializeComponent();
            listViewPets.Columns.Add(Strings.Name, 150, HorizontalAlignment.Left);
            listViewPets.Columns.Add(Strings.UID, 0, HorizontalAlignment.Left);
            listViewPets.Columns.Add(Strings.Food + string.Format(" ({0})", Strings.Amount), -2, HorizontalAlignment.Left);
            listViewPets.Columns.Add(Strings.NextMeal, -2, HorizontalAlignment.Left);
            listViewPets.Columns.Add(Strings.Characteristics, -2, HorizontalAlignment.Left);
            LVItems.Columns.Add(Strings.GID, 0, HorizontalAlignment.Center);
            LVItems.Columns.Add(Strings.UID, 0, HorizontalAlignment.Center);
            LVItems.Columns.Add(Strings.Name, -2, HorizontalAlignment.Center);
            LVItems.Columns.Add(Strings.Amount, -2, HorizontalAlignment.Center);
            LVItems.Columns.Add(Strings.Type, -2, HorizontalAlignment.Center);
            LVItems.Columns.Add(string.Format("{0} {1}", Strings.Price, Strings.Medium.ToLower()), -2, HorizontalAlignment.Center);
            LVItemBag.Columns.Add(Strings.GID, 0, HorizontalAlignment.Center);
            LVItemBag.Columns.Add(Strings.UID, 0, HorizontalAlignment.Center);
            LVItemBag.Columns.Add(Strings.Name, -2, HorizontalAlignment.Center);
            LVItemBag.Columns.Add(Strings.Amount, -2, HorizontalAlignment.Center);
            LVItemBag.Columns.Add(Strings.Type, -2, HorizontalAlignment.Center);
            LVItemBag.Columns.Add(string.Format("{0} {1}", Strings.Price, Strings.Medium.ToLower()), -2, HorizontalAlignment.Center);
            LVItemShop.Columns.Add(Strings.GID, 0, HorizontalAlignment.Center);
            LVItemShop.Columns.Add(Strings.UID, 0, HorizontalAlignment.Center);
            LVItemShop.Columns.Add(Strings.Name, -2, HorizontalAlignment.Center);
            LVItemShop.Columns.Add(Strings.Amount, -2, HorizontalAlignment.Center);
            LVItemShop.Columns.Add(Strings.GID, -2, HorizontalAlignment.Center);
            LVItemShop.Columns.Add(Strings.SellingPrice, -2, HorizontalAlignment.Center);
            ComparateurBox.SelectedIndex = 0;
            MonstersRestrictionsView.Columns.Add(Strings.Name, -2);
            MonstersRestrictionsView.Columns.Add(Strings.Comparator, -2);
            MonstersRestrictionsView.Columns.Add(Strings.Number, -2);
            MonstersRestrictionsView.Columns.Add(Strings.Restriction, -2);
            Account = new Account(username, password, socket);
        }

        public AccountUC()
        {
            InitializeComponent();
        }

        #endregion

        #region Methodes d'interfaces

        private void Form_Closed(object sender, EventArgs e)
        {
            if (Account.SocketManager != null)
                Account.SocketManager.DisconnectFromGUI();
            if (Account.Config.IsMITM)
            {
                Account.SocketManager.DisconnectServer("Form closing");
            }
        }

        private void SaveConfig_Click(object sender, EventArgs e)
        {
            Account.Config.ConfigRecover.SaveConfig();
        }

        private void DeleteConfigBt_Click(object sender, EventArgs e)
        {
            Account.Config.ConfigRecover.DeleteConfig();
        }

        public void InitMITM()
        {
            Account.SocketManager = new SocketManager(Account);
            Account.SocketManager.InitMITM();
        }

        public void Log(TextInformation text, int levelVerbose)
        {
            if (IsDisposed == true)
                return;
            if (levelVerbose < Account.Config.VerboseLevel)
                return;

            text.Text = Engine.Constants.Translate.GetTranslation(text.Text);
            text.Text = "[" + DateTime.Now.ToLongTimeString() +
                "] (" + text.Category + ") " + text.Text;
            if (text is DebugTextInformation && !Account.Config.DebugMode)
                return;

            if (LogCb.Checked)
                using (StreamWriter fileWriter = new StreamWriter(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BlueSheep\Logs\" + DateTime.Now.ToShortDateString().Replace("/", "-") + "_" + Account.CharacterBaseInformations.Name + ".txt", true))
                    fileWriter.WriteLine(text.Text);

            int startIndex = LogConsole.TextLength;

            LogConsole.AppendText(text.Text + "\r\n");
            LogConsole.Select(LogConsole.Text.Length, 0);
            LogConsole.ScrollToCaret();

            LogConsole.SelectionStart = startIndex;
            LogConsole.SelectionLength = text.Text.Length;
            LogConsole.SelectionColor = text.Color;

        }

        private void GetMessageLogFromAccountQueue()
        {
            Tuple<TextInformation, int> queueItem;
            TextInformation text;
            int verboseLevel;
            while (true)
            {
                Thread.Sleep(1);
                Application.DoEvents();
                if (Account.InformationQueue.Count > 0)
                {
                    queueItem = Account.InformationQueue.Dequeue();
                    text = queueItem.Item1;
                    verboseLevel = queueItem.Item2;

                    Log(text, verboseLevel);
                }
            }
        }


        private void DeleteItem_Click(object sender, EventArgs e)
        {
            //Delete an item from inventory
            if (Account.State == Status.Fighting)
            {
                Log(new ErrorTextInformation("It's impossible to destroy an item in combat ^^"), 0);
                return;
            }
            for (int i = 0; i < LVItems.Items.Count; i++)
            {
                if (LVItems.Items[i].Selected)
                {
                    Account.Inventory.DeleteItem(Convert.ToInt32(LVItems.Items[i].SubItems[1].Text), Convert.ToInt32(LVItems.Items[i].SubItems[3].Text));
                }
            }
        }

        private void DropItems_Click(object sender, EventArgs e)
        {
            //Drop an item from inventory
            if (Account.State == Status.Fighting)
            {
                Log(new ErrorTextInformation("It is impossible to drop an item in combat ^^"), 0);
                return;
            }
            for (int i = 0; i < LVItems.Items.Count; i++)
            {
                if (LVItems.Items[i].Selected)
                {
                    Account.Inventory.DropItem(Convert.ToInt32(LVItems.Items[i].SubItems[1].Text), Convert.ToInt32(LVItems.Items[i].SubItems[3].Text));
                }
            }
        }

        private void sadikButton1_Click(object sender, EventArgs e)
        {
            //Use an item from inventory
            if (Account.State == Status.Fighting)
            {
                Log(new ErrorTextInformation("It is impossible to use an item in combat ^^"), 0);
                return;
            }
            for (int i = 0; i < LVItems.Items.Count; i++)
            {
                if (LVItems.Items[i].Selected)
                {
                    Account.Inventory.UseItem(Convert.ToInt32(LVItems.Items[i].SubItems[1].Text));
                }
            }
        }

        private void sadikButton2_Click(object sender, EventArgs e)
        {
            //Equip an item from inventory
            if (Account.State == Status.Fighting)
            {
                Log(new ErrorTextInformation("It is impossible to equip an item in combat ^^"), 0);
            }
            for (int i = 0; i < LVItems.Items.Count; i++)
            {
                if (LVItems.Items[i].Selected)
                {
                    Account.Inventory.EquipItem(Convert.ToInt32(LVItems.Items[i].SubItems[1].Text));
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Account.SocketManager.State == SocketState.Connected)
            {
                Account.SocketManager.DisconnectFromGUI();
            }
            else
            {
                //Init();
                Account.TryReconnect(2);
            }
        }

        private void checkBoxBegin_CheckedChanged(object sender)
        {
            if (checkBoxBegin.Checked == true)
            {
                StartFeeding();
            }
            else
            {
                Log(new BotTextInformation("Livestock will be stopped"), 3);
                Account.Running = null;
            }
        }

        public void ModifBar(int Bar, int Max, int value, string text)
        {
            if (VitaBar.InvokeRequired)
                Invoke(new DelegBar(ModifBar), Bar, Max, value, text);
            else
            {
                switch (Bar)
                {
                    case 1:
                        XpBar.Maximum = Max;
                        XpBar.Value = value;
                        XpBar.Text = text;
                        break;
                    case 2:
                        VitaBar.Maximum = Max;
                        VitaBar.Value = value;
                        VitaBar.Text = text;
                        break;
                    case 3:
                        PodsBar.Maximum = Max;
                        PodsBar.Value = value;
                        PodsBar.Text = text;
                        break;
                    case 4:
                        KamasLabel.Text = text + " kamas";
                        break;
                    case 5:
                        PosLabel.Text = text;
                        break;
                    case 7:
                        ParentForm.Text = text;
                        break;
                    case 8:
                        LevelLb.Text = text;
                        break;
                    case 9:
                        SubcribeLb.Text = text;
                        break;
                }
            }
        }

        public void AddItem(ListViewItem li, ListView list)
        {
            //this.BeginInvoke(new MethodInvoker(LVItems.Items.Add),li);
            if (list.InvokeRequired == true)
                Invoke(new DelegListView(AddItem), li, list);
            else
                list.Items.Add(li);
        }

        private void LoadPathBt_Click(object sender, EventArgs e)
        {
            PathChoiceForm frm = new PathChoiceForm(this);
            frm.Show();
        }

        private void LaunchPathBt_Click(object sender, EventArgs e)
        {
            if (Account.Config.Path != null)
            {
                Log(new BotTextInformation("Lancement du trajet"), 1);
                Account.Config.Path.Start();
            }
            else
                Log(new ErrorTextInformation("Aucun trajet chargé"), 3);
        }

        private void StopPathBt_Click(object sender, EventArgs e)
        {
            if (Account.Config.Path != null)
            {
                Account.Config.Path.StopPath();
                Log(new BotTextInformation("Trajet arrêté"), 1);
            }
        }

        private void ChoiceIABt_Click(object sender, EventArgs e)
        {
            IAChoice frm = new IAChoice(Account);
            frm.ShowDialog();
        }

        private void StartWaitingBt_Click(object sender, EventArgs e)
        {
            Log(new BotTextInformation("Waiting for the sale of a house ..."), 1);
        }

        private void ParcourirBt_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                SearcherLogBox.Text = saveFileDialog1.FileName;
        }

        public void ActualizeInventory()
        {
            BeginInvoke(new MethodInvoker(LVItems.Items.Clear));
            foreach (Core.Inventory.Item i in Account.Inventory.Items)
            {
                string[] row1 = { i.GID.ToString(), i.UID.ToString(), i.Name, i.Quantity.ToString(), i.Type.ToString(), i.Price.ToString() };
                System.Windows.Forms.ListViewItem li = new System.Windows.Forms.ListViewItem(row1);
                li.ToolTipText = i.Description;
                AddItem(li, LVItems);
            }
            //RegenUC.RefreshQuantity();
            //TODO Militão: Add Regen Module

            BeginInvoke(new MethodInvoker(LVItemBag.Items.Clear));
            foreach (Core.Inventory.Item i in Account.Inventory.Items)
            {
                string[] row1 = { i.GID.ToString(), i.UID.ToString(), i.Name, i.Quantity.ToString(), i.Type.ToString(), i.Price.ToString() };
                System.Windows.Forms.ListViewItem li = new System.Windows.Forms.ListViewItem(row1);
                li.ToolTipText = i.Description;
                AddItem(li, LVItemBag);
            }
        }

        public void ActualizeFamis()
        {
            if (labelNextMeal.InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(ActualizeFamis));
                return;
            }
            if (Account.Config.NextMeal.Year != 1)
                Invoke(new DelegLabel(ModLabel), "Prochain repas à : " + Account.Config.NextMeal.ToShortTimeString(), labelNextMeal);
            else
                Invoke(new DelegLabel(ModLabel), "Pas de prochain repas", labelNextMeal);

            Invoke(new DelegLabel(ModLabel), Account.Safe != null ? "Coffre : Oui" : "Coffre : Non", labelSafe);

            if (listViewPets.InvokeRequired)
                BeginInvoke(new MethodInvoker(listViewPets.Items.Clear));
            else
                listViewPets.Items.Clear();

            if ((Account.petsList != null) && (Account.petsList.Count != 0))
            {
                foreach (Pet pet in Account.petsList)
                {
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.SubItems[0].Text = I18N.GetText((int)pet.Datas.Fields["nameId"]);
                    listViewItem.SubItems.Add(pet.Informations.UID.ToString());

                    if (pet.FoodList.Count != 0)
                        listViewItem.SubItems.Add(I18N.GetText((int)pet.FoodList[0].Datas.Fields["nameId"]) + " (" + pet.FoodList[0].Informations.Quantity + ")");
                    else
                        listViewItem.SubItems.Add("Aucune (0)");

                    if (pet.NextMeal.Year != 1)
                    {
                        DateTime nextMeal = new DateTime(pet.NextMeal.Year, pet.NextMeal.Month, pet.NextMeal.Day,
                            pet.NextMeal.Hour, pet.NextMeal.Minute, 0);

                        listViewItem.SubItems.Add(nextMeal.ToShortDateString() + " " + nextMeal.ToShortTimeString());
                    }
                    else
                        listViewItem.SubItems.Add("Pas de prochain repas.");

                    listViewItem.SubItems.Add(pet.Effect);

                    AddItem(listViewItem, listViewPets);
                }
            }
        }

        public void ModLabel(string content, Label lab)
        {
            if (lab.InvokeRequired)
            {
                Invoke(new DelegLabel(ModLabel), content, lab);
                return;
            }
            lab.Text = content;
        }

        private void CommandeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string text = CommandeBox.Text;
                CommandeBox.Clear();
                CLIParser parser = new CLIParser(Account);
                List<string> result = parser.Parse(text);
                foreach (string s in result)
                {
                    if (s.Contains("ERROR"))
                        Log(new ErrorTextInformation(s), 0);
                    else
                        Log(new BotTextInformation(s), 0);
                }
            }
            else if (e.KeyCode == Keys.Up && CLIParser.CommandsHistory.Count > 0)
            {
                int index = CLIParser.CommandsHistory.IndexOf(CommandeBox.Text);
                if (index != -1 && index != 0)
                    CommandeBox.Text = CLIParser.CommandsHistory[index - 1];
                else
                    CommandeBox.Text = CLIParser.CommandsHistory[CLIParser.CommandsHistory.Count - 1];
            }
            else if (e.KeyCode == Keys.Down && CLIParser.CommandsHistory.Count > 0)
            {
                int index = CLIParser.CommandsHistory.IndexOf(CommandeBox.Text);
                if (index != -1 && index != CLIParser.CommandsHistory.Count - 1)
                    CommandeBox.Text = CLIParser.CommandsHistory[index + 1];
                else
                    CommandeBox.Text = "";
            }
        }

        public void ActualizeMap()
        {
            BeginInvoke(new MethodInvoker(MapView.Items.Clear));
            foreach (BlueSheep.Core.Map.Elements.InteractiveElement e in Account.MapData.InteractiveElements.Keys)
            {
                BlueSheep.Core.Map.Elements.StatedElement element = Account.MapData.StatedElements.Find(s => s.Id == e.Id);
                string type = Strings.Unknown + " (" + e.TypeId + ")";
                switch (e.TypeId)
                {
                    case 16:
                        type = type.Replace(Strings.Unknown, "Zaap");
                        break;
                    case 106:
                        type = type.Replace(Strings.Unknown, "Zaapi");
                        break;
                    case 105:
                        type = type.Replace(Strings.Unknown, "Poubelle");
                        break;
                    case 120:
                        type = type.Replace(Strings.Unknown, "Enclos");
                        break;
                    case -1:
                        type = type.Replace(Strings.Unknown, "Porte/Escalier ?");
                        break;
                    case 119:
                        type = type.Replace(Strings.Unknown, "Livre d'artisans");
                        break;
                }
                string cellId = "?";
                if (element != null)
                    cellId = Convert.ToString(element.CellId);

                AddItem(new ListViewItem(new string[] { Convert.ToString(e.Id), cellId, type }), MapView);
            }
            foreach (GameRolePlayNpcInformations n in Account.MapData.Npcs)
            {
                AddItem(new ListViewItem(new string[] { Convert.ToString(n.NpcId), I18N.GetText((int)n.ContextualId), "?", "NPC" }), MapView);
            }
            foreach (GameRolePlayCharacterInformations c in Account.MapData.Players)
            {
                AddItem(new ListViewItem(new string[] { Convert.ToString("[CID]:" + c.ContextualId + "[ACCID]:" + c.AccountId), c.Name, Convert.ToString(c.Disposition.CellId), "Player" }), MapView);
            }
        }

        public void ActualizeJobs()
        {
            if (JobsTabP.InvokeRequired)
                Invoke(new MethodInvoker(ActualizeJobs));
            else
            {
                JobsTabP.TabPages.Clear();
                foreach (Job j in Account.Jobs)
                {
                    JobsTabP.TabPages.Add(j.Name);
                    SadikTabControl t = new SadikTabControl();
                    t.TabPages.Add("Configuration");
                    t.TabPages.Add("Statistiques");
                    JobUC uc = new JobUC(this, j);
                    //JobsUC.Add(uc);
                    //TODO Militão: Job Module
                    JobsTabP.TabPages[JobsTabP.TabCount - 1].Controls.Add(uc);
                    t.Dock = DockStyle.Fill;
                    foreach (int i in j.Skills)
                    {
                        DataClass d = GameData.GetDataObject(D2oFileEnum.Skills, i);
                        if ((int)d.Fields["gatheredRessourceItem"] == -1)
                            continue;
                        if (j.Level >= (int)d.Fields["levelMin"])
                        {
                            string name = I18N.GetText((int)d.Fields["nameId"]);
                            int rid = (int)d.Fields["interactiveId"];
                            string typename = I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Interactives, rid).Fields["nameId"]);
                            uc.g.Rows.Add(name, typename, rid);
                        }
                    }

                    foreach (int i in j.Skills)
                    {
                        DataClass d = GameData.GetDataObject(D2oFileEnum.Skills, i);
                        if (j.Level > (int)d.Fields["levelMin"])
                        {
                            string name = I18N.GetText((int)d.Fields["nameId"]);
                            foreach (int c in (ArrayList)d.Fields["craftableItemIds"])
                            {
                                string rname = "Unknown";
                                DataClass data = GameData.GetDataObject(D2oFileEnum.Items, c);
                                if (data != null)
                                {
                                    rname = I18N.GetText((int)data.Fields["nameId"]);
                                    uc.gg.Rows.Add(name, rname, c);
                                }
                            }
                        }
                    }
                    uc.g.AutoResizeColumns();
                    uc.g.Columns[2].Visible = false;
                    uc.gg.AutoResizeColumns();
                    uc.gg.Columns[2].Visible = false;
                    uc.Show();
                }
            }
        }

        private void LVItems_ColumnClick(object sender, EventArgs e)
        {
            // Call the sort method to manually sort.
            LVItems.Sort();
        }

        private void ForbidMonsterBt_Click(object sender, EventArgs e)
        {
            //if (Fight == null)
            //{
            //    MessageBox.Show("Veuillez choisir une IA avant de régler les restrictions");
            //    return;
            //}
            if (MonsterTextBox.Text.Length > 0)
            {
                ListViewItem l = new ListViewItem(new string[] { MonsterTextBox.Text, (string)ComparateurBox.SelectedItem, Convert.ToString(NUDRestrictions.Value), "Interdit" });
                MonstersRestrictionsView.Items.Add(l);
            }
            MonstersRestrictionsView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void ForceMonstersBt_Click(object sender, EventArgs e)
        {
            //if (Fight == null)
            //{
            //    MessageBox.Show("Veuillez choisir une IA avant de régler les restrictions");
            //    return;
            //}
            if (MonsterTextBox.Text.Length > 0)
            {
                ListViewItem l = new ListViewItem(new string[] { MonsterTextBox.Text, (string)ComparateurBox.SelectedItem, Convert.ToString(NUDRestrictions.Value), "Obligatoire" });
                MonstersRestrictionsView.Items.Add(l);
            }
            MonstersRestrictionsView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        }

        public void ActualizeFightStats(Dictionary<string, int> winLose, Dictionary<DateTime, int> xpwon)
        {
            if (WinLoseFightPie.InvokeRequired)
            {
                Invoke(new DelegGatherPie(ActualizeFightStats), winLose, xpwon);
                return;
            }
#if __MonoCS__

#else
            if (WinLoseFightPie.Titles.Count < 1)
                WinLoseFightPie.Titles.Add("Résultats des combats");
#endif
            WinLoseFightPie.Series.Clear();
            WinLoseFightPie.ChartAreas[0].BackColor = Color.Transparent;
            Series series1 = new Series
            {
                Name = "series1",
                IsVisibleInLegend = false,
                Color = System.Drawing.Color.DeepSkyBlue,
                ChartType = SeriesChartType.Pie
            };
            WinLoseFightPie.Series.Add(series1);
            int i = 0;
            foreach (KeyValuePair<string, int> pair in winLose)
            {
                series1.Points.Add(pair.Value);
                var p1 = series1.Points[i];
                p1.AxisLabel = pair.Key + " (" + pair.Value + ")";
                p1.LegendText = pair.Key;
                i += 1;
            }
            XpBarsChart.Series.Clear();
#if __MonoCS__

#else
            if (XpBarsChart.Titles.Count < 1)
                XpBarsChart.Titles.Add("Experience gagnée");
#endif
            foreach (KeyValuePair<DateTime, int> p in xpwon)
            {
                Series series = new Series(p.Key.ToShortDateString());
                series.Name = p.Key.ToShortDateString();
                series.IsVisibleInLegend = true;
                series.ChartType = SeriesChartType.Column;
                series.Points.Add(p.Value);
                XpBarsChart.Series.Add(series);
            }
            WinLoseFightPie.Invalidate();
            XpBarsChart.Invalidate();
        }

        private void MonsterTextBox_GetFocus(object sender, EventArgs e)
        {
            if (MonsterTextBox.Text == "Entrez le nom du monstre...")
                MonsterTextBox.Text = "";
        }

        private void MonstersRestrictionView_Supp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && MonstersRestrictionsView.SelectedItems.Count > 0)
            {
                for (int i = 0; i < MonstersRestrictionsView.Items.Count; i++)
                {
                    if (MonstersRestrictionsView.Items[i].Selected)
                        MonstersRestrictionsView.Items.RemoveAt(i);
                }
            }
        }
        #endregion

        #region Methodes Publics

        public void Wait(int min, int max)
        {
            Random Random = new Random();
            int Temps = Random.Next(min, max);
            double endwait = Environment.TickCount + Temps;
            while (Environment.TickCount < endwait)
            {
                System.Threading.Thread.Sleep(1);
                Application.DoEvents();
            }
        }

        public void GetNextMeal()
        {
            Account.GetNextMeal();
        }

        public void StartFeeding()
        {
            Account.Running.Init();
        }

        public void Init()
        {
            Account.Init();
        }

        public bool PerformGather()
        {
            return Account.PerformGather();
        }

        public void SetNextMeal()
        {
            Account.SetNextMeal();
        }

        public bool VerifGroup(List<string> monsters)
        {
            if (MonstersRestrictionsView.InvokeRequired)
                return (bool)Invoke(new DelegVerifGroup(VerifGroup), monsters);
            if (MonstersRestrictionsView.Items.Count <= 0)
                return true;
            foreach (ListViewItem i in MonstersRestrictionsView.Items)
            {
                switch (i.SubItems[3].Text)
                {
                    case "Interdit":
                        switch (i.SubItems[1].Text)
                        {
                            case ">":
                                if (monsters.FindAll(f => i.SubItems[0].Text == f).Count > Convert.ToInt32(i.SubItems[2].Text))
                                    return false;
                                else
                                    continue;
                            case "<":
                                if (monsters.FindAll(f => i.SubItems[0].Text == f).Count < Convert.ToInt32(i.SubItems[2].Text))
                                    return false;
                                else
                                    continue;
                            case "=":
                                if (monsters.FindAll(f => i.SubItems[0].Text == f).Count == Convert.ToInt32(i.SubItems[2].Text))
                                    return false;
                                else
                                    continue;
                            default:
                                continue;
                        }
                    case "Obligatoire":
                        switch (i.SubItems[1].Text)
                        {
                            case ">":
                                if (!(monsters.FindAll(f => i.SubItems[0].Text == f).Count > Convert.ToInt32(i.SubItems[2].Text)))
                                    return false;
                                else
                                    continue;
                            case "<":
                                if (!(monsters.FindAll(f => i.SubItems[0].Text == f).Count < Convert.ToInt32(i.SubItems[2].Text)))
                                    return false;
                                else
                                    continue;
                            case "=":
                                if (!(monsters.FindAll(f => i.SubItems[0].Text == f).Count == Convert.ToInt32(i.SubItems[2].Text)))
                                    return false;
                                else
                                    continue;
                            default:
                                continue;
                        }
                }
            }
            return true;

        }
        #endregion

        #region Methodes privées
        private void Connect()
        {
            Account.Connect();
        }

        private static int GetRandomTime()
        {
            Random random = new Random();

            return random.Next(500, 1250);
        }
        #endregion



        private void AutoDelAddBt_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LVItems.Items.Count; i++)
            {
                if (LVItems.Items[i].Selected)
                {
                    ListViewItem item = new ListViewItem(new string[] { LVItems.Items[i].SubItems[2].Text, Strings.AutoDelete });
                    //Account.GestItemsUC.LVGestItems.Items.Add(item);
                    //TODO Militão: Add Items module
                }
            }
        }

        private void RegenAddBt_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LVItems.Items.Count; i++)
            {
                if (LVItems.Items[i].Selected)
                {
                    ListViewItem item = new ListViewItem(new string[] { LVItems.Items[i].SubItems[2].Text, LVItems.Items[i].SubItems[3].Text });
                    //RegenUC.LVItems.Items.Add(item);
                    //TODO Militão: Add regen module
                }
            }
        }

        private void RelaunchPath_CheckedChanged(object sender, EventArgs e)
        {
            if (RelaunchPath.Checked)
            {
                RelaunchPath.Text = "✔ " + Strings.RestartThePathToReconnect;
            }
            else
            {
                RelaunchPath.Text = "✘ " + Strings.RestartThePathToReconnect;
            }
            Account.Config.ConfigRecover.SaveConfig();
        }

        private void sadikCheckbox1_CheckedChanged_1(object sender)
        {
            if (sadikCheckbox1.Checked)
            {
                PlaceTimer.Interval = 10000;
                PlaceTimer.Start();
            }
            else
            {
                PlaceTimer.Stop();
            }
        }

        private void sadikButton3_Click(object sender, EventArgs e)
        {

            ExchangeRequestOnShopStockMessage packetshop = new ExchangeRequestOnShopStockMessage();
            Account.SocketManager.Send(packetshop);

        }

        public void addItemToShop()
        {
            if (LVItemBag.InvokeRequired)
            {
                Invoke(new Callback(addItemToShop));
                return;
            }
            for (int i = 0; i < LVItemBag.Items.Count; i++)
            {
                if (LVItemBag.Items[i].Selected)
                {
                    try
                    {
                        ExchangeObjectMovePricedMessage msg = new ExchangeObjectMovePricedMessage();
                        msg.ObjectUID = (uint)Account.Inventory.GetItemFromName(LVItemBag.Items[i].SubItems[2].Text).UID;
                        msg.Quantity = Account.Inventory.GetItemFromName(LVItemBag.Items[i].SubItems[2].Text).Quantity;
                        msg.Price = Convert.ToUInt64(numericUpDown1.Value);
                        Account.SocketManager.Send(msg);
                        Log(new ActionTextInformation(Strings.AdditionOf + Account.Inventory.GetItemFromName(LVItemBag.Items[i].SubItems[2].Text).Name + "(x " + Account.Inventory.GetItemFromName(LVItemBag.Items[i].SubItems[2].Text).Quantity + ") " + Strings.InTheStoreAtThePriceOf + " : " + msg.Price + " " + Strings.Kamas), 2);
                        LeaveDialogRequestMessage packetleave = new LeaveDialogRequestMessage();
                        Wait(2000, 2000);
                        Account.SocketManager.Send(packetleave);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void PlaceTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Account.State == Status.Fighting)
                {
                    Log(new ErrorTextInformation(Strings.UnableToSwitchToMerchantModeInAFight + " >.<"), 2);
                }
                if (Account.SocketManager.State == SocketState.Connected)
                {
                    ExchangeShowVendorTaxMessage taxpacket = new ExchangeShowVendorTaxMessage();
                    Account.SocketManager.Send(taxpacket);
                    ExchangeStartAsVendorMessage ventepacket = new ExchangeStartAsVendorMessage();
                    Account.SocketManager.Send(ventepacket);
                    //Thread.Sleep(500);
                    Log(new BotTextInformation(Strings.MerchantModeActivationTest), 1);
                    if (Account.SocketManager.State == SocketState.Closed)
                    {
                        Account.SocketManager.DisconnectFromGUI();
                        PlaceTimer.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool NeedToAddItem()
        {
            if (LVItemBag.InvokeRequired)
                return (bool)Invoke(new BoolCallback(NeedToAddItem));
            else
                return (LVItemBag.SelectedItems != null);
        }

        private void BtnActualize_Click(object sender, EventArgs e)
        {
            ExchangeRequestOnShopStockMessage packetshop = new ExchangeRequestOnShopStockMessage();
            Account.SocketManager.Send(packetshop);
            LeaveDialogRequestMessage packetleave = new LeaveDialogRequestMessage();
            Account.SocketManager.Send(packetleave);
        }

        public void actualizeshop(List<ObjectItemToSell> sell)
        {
            BeginInvoke(new MethodInvoker(LVItemShop.Items.Clear));

            foreach (ObjectItemToSell i in sell)
            {
                BlueSheep.Core.Inventory.Item item = new BlueSheep.Core.Inventory.Item(i.Effects.ToList(), i.ObjectGID, 0, (int)i.Quantity, (int)i.ObjectUID);
                string[] row1 = { item.GID.ToString(), item.UID.ToString(), item.Name, item.Quantity.ToString(), item.Type, i.ObjectPrice.ToString() };
                System.Windows.Forms.ListViewItem li = new System.Windows.Forms.ListViewItem(row1);
                li.ToolTipText = item.Description;
                AddItem(li, LVItemShop);
            }
        }
    }
}

