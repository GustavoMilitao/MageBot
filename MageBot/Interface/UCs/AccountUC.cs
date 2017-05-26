using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;
using MageBot.Interface.UCs;
using System.IO;
using MageBot.Core.Account;
using MageBot.Core.Pets;
using Util.Util.I18n.Strings;
using MageBot.Protocol.Types.Game.Data.Items;
using MageBot.Core.Job;
using MageBot.Core.Engine.Network;
using Util.Util.Text.Log;
using MageBot.Core.Network;
using MageBot.Util.Enums.Internal;
using MageBot.Core.Misc;
using MageBot.DataFiles.Data.I18n;
using MageBot.Core.Engine.Common;
using MageBot.Protocol.Types.Game.Context.Roleplay;
using MageBot.DataFiles.Data.D2o;
using MageBot.Core;
using MageBot.Protocol.Messages.Game.Inventory.Exchanges;
using MageBot.Protocol.Messages.Game.Dialog;
using MageBot.Core.Fight;
using MageBot.Util.Enums.EnumHelper;
using System.Threading;

namespace MageBot.Interface
{
    public partial class AccountUC : MetroFramework.Controls.MetroUserControl
    {
        /// <summary>
        /// Main UC
        /// </summary>

        #region Properties
        public Account Account { get; set; }
        public List<JobUC> JobsUC { get; set; }
        public HeroicUC HeroicUC { get; set; }
        public GestItemsUC GestItemsUC { get; set; }
        public CaracUC CaracUC { get; set; }
        public RegenUC RegenUC { get; set; }
        public FloodUC FloodUC { get; set; }
        public MetroFramework.Forms.MetroForm m_ParentForm { get; set; }
        #endregion

        #region Delegates
        private delegate void SetLogsCallback(TextInformation textInformations, int levelVerbose);
        private delegate void SetLicenceCallback(bool response, string text);
        private delegate void DelegBar(int Bar, int Max, int value, string text);
        private delegate void DelegListView(ListViewItem delta, ListView gamma);
        private delegate void DelegListViewResize(ListView LV);
        private delegate void DelegLabel(string text, Label lab);
        private delegate void DelegBool(bool param1);
        private delegate void DelegGatherPie(Dictionary<string, int> ressources, Dictionary<DateTime, int> xp);
        private delegate bool DelegVerifGroup(List<string> monsters);
        private delegate bool BoolCallback();
        private delegate void Callback();
        #endregion

        #region Constructors

        public AccountUC(Account account, MetroFramework.Forms.MetroForm form = null)
        {
            InitializeComponent();
            //MonsterTextBox.KeyUp += (s, e) =>
            //{
            //    IntelliSense.AutoCompleteTextBox(MonsterTextBox, lstPopup, IntelliSense.MonstersList, e);
            //};
            //TODO Militão 2.0: Refact this monster text box auto complete
            if (form != null)
                m_ParentForm = form;
            Account = account;
            FillAccountEvents();
            // Jobs
            JobsUC = new List<JobUC>();

            //Heroic mode
            HeroicUC = new HeroicUC(this);
            MiscPage.TabPages[3].Controls.Add(HeroicUC);
            HeroicUC.Show();

            //Items management
            GestItemsUC = new GestItemsUC(this);
            tabPage9.Controls.Add(GestItemsUC);
            GestItemsUC.Show();

            //Carac
            CaracUC = new CaracUC(this);
            StatsPage.Controls.Add(CaracUC);
            CaracUC.Show();

            //Regen
            RegenUC = new RegenUC(this);
            RegenPage.Controls.Add(RegenUC);
            RegenUC.Show();

            //Flood
            FloodUC = new FloodUC(this);
            tabPage2.Controls.Add(FloodUC);
            FloodUC.Show();

            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MageBot", "Accounts", Account.AccountName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

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
            FillAccountInitialSettings();
        }

        public AccountUC()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

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
            Account.ConfigRecover.SaveConfig();
        }

        private void DeleteConfigBt_Click(object sender, EventArgs e)
        {
            Account.ConfigRecover.DeleteConfig();
        }

        private void DeleteItem_Click(object sender, EventArgs e)
        {
            //Delete an item from inventory
            if (Account.State == Status.Fighting)
            {
                Account.Log(new ErrorTextInformation("It's impossible to delete an item in fight ^^"), 0);
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
                Account.Log(new ErrorTextInformation("It's impossible to drop an item in fight ^^"), 0);
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
                Account.Log(new ErrorTextInformation("It's impossible to use an item in fight ^^"), 0);
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
                Account.Log(new ErrorTextInformation("It's impossible to equip an item in fight ^^"), 0);
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
                Account.Disconnect();
            }
            else
            {
                //Init();
                TryReconnect(2);
            }
        }

        private void checkBoxBegin_CheckedChanged(object sender)
        {
            Account.Config.Begin = checkBoxBegin.Checked;
            if (checkBoxBegin.Checked)
            {
                Account.StartFeeding();
            }
            else
            {
                Account.Log(new BotTextInformation("Pet feeding stopped"), 3);
                Account.Running = null;
            }
        }

        private void LoadPathBt_Click(object sender, EventArgs e)
        {
            PathChoiceForm frm = new PathChoiceForm(this);
            frm.Show();
        }

        private void LaunchPathBt_Click(object sender, EventArgs e)
        {
            LaunchPath();
        }

        private void StopPathBt_Click(object sender, EventArgs e)
        {
            if (Account.Path != null)
            {
                Account.Path.StopPath();
                Account.Log(new BotTextInformation("Path stopped"), 1);
            }
        }

        private void ChoiceIABt_Click(object sender, EventArgs e)
        {
            IAChoice frm = new IAChoice(this);
            frm.ShowDialog();
        }

        private void StartWaitingBt_Click(object sender, EventArgs e)
        {
            if (!Account.Config.WaitingForTheSale)
            {
                Account.Config.WaitingForTheSale = true;
                Account.House = new HouseBuy(Account);
                Account.Log(new BotTextInformation("Waiting for the sale of a house..."), 1);
            }
            else
                Account.Log(new BotTextInformation("Waiting for the sale already on..."), 1);
        }

        private void ParcourirBt_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SearcherLogBox.Text = saveFileDialog1.FileName;
                Account.Config.HouseSearcherLogPath = saveFileDialog1.FileName;
            }
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
                        Account.Log(new ErrorTextInformation(s), 0);
                    else
                        Account.Log(new BotTextInformation(s), 0);
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

        private void LVItems_ColumnClick(object sender, EventArgs e)
        {
            // Call the sort method to manually sort.
            LVItems.Sort();
        }

        private void ForbidMonsterBt_Click(object sender, EventArgs e)
        {
            if (MonsterTextBox.Text.Length > 0)
            {
                Account.Config.MonsterRestrictions.Add(
                    new Core.Fight.MonsterRestrictions()
                    {
                        MonsterName = MonsterTextBox.Text,
                        Operator = ConvertOperator((string)ComparateurBox.SelectedItem),
                        Quantity = (int)NUDRestrictions.Value,
                        RestrictionLevel = RestrictionLevel.Forbidden
                    });
                ListViewItem l = new ListViewItem(new string[] { MonsterTextBox.Text, (string)ComparateurBox.SelectedItem, Convert.ToString(NUDRestrictions.Value), Strings.Forbidden });
                MonstersRestrictionsView.Items.Add(l);
            }
            MonstersRestrictionsView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void ForceMonstersBt_Click(object sender, EventArgs e)
        {
            Account.Config.MonsterRestrictions.Add(
                    new Core.Fight.MonsterRestrictions()
                    {
                        MonsterName = MonsterTextBox.Text,
                        Operator = ConvertOperator((string)ComparateurBox.SelectedItem),
                        Quantity = (int)NUDRestrictions.Value,
                        RestrictionLevel = RestrictionLevel.Required
                    });
            if (MonsterTextBox.Text.Length > 0)
            {
                ListViewItem l = new ListViewItem(new string[] { MonsterTextBox.Text, (string)ComparateurBox.SelectedItem, Convert.ToString(NUDRestrictions.Value), Strings.Required });
                MonstersRestrictionsView.Items.Add(l);
            }
            MonstersRestrictionsView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        }

        private void MonsterTextBox_GetFocus(object sender, EventArgs e)
        {
            MonsterTextBox.Text = String.Empty;
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

        private void AutoDelAddBt_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LVItems.Items.Count; i++)
            {
                if (LVItems.Items[i].Selected)
                {
                    ListViewItem item = new ListViewItem(new string[] { LVItems.Items[i].SubItems[2].Text, Strings.AutoDelete });
                    GestItemsUC.LVGestItems.Items.Add(item);
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
                    RegenUC.LVItems.Items.Add(item);
                    Account.Config.RegenItems.Add(Account.Inventory.GetItemFromUID(Convert.ToInt32(LVItems.Items[i].SubItems[1].Text)));
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
            Account.ConfigRecover.SaveConfig();
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
            AddItemToShop();
            ActualizeShopAndBagItems();
        }

        private void PlaceTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Account.State == Status.Fighting)
                {
                    Account.Log(new ErrorTextInformation(Strings.UnableToSwitchToMerchantModeInAFight + " >.<"), 2);
                }
                if (Account.SocketManager.State == SocketState.Connected)
                {
                    ExchangeShowVendorTaxMessage taxpacket = new ExchangeShowVendorTaxMessage();
                    Account.SocketManager.Send(taxpacket);
                    ExchangeStartAsVendorMessage ventepacket = new ExchangeStartAsVendorMessage();
                    Account.SocketManager.Send(ventepacket);
                    //Account.Wait(500);
                    Account.Log(new BotTextInformation(Strings.MerchantModeActivationTest), 1);
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

        private void BtnActualize_Click(object sender, EventArgs e)
        {
            ActualizeShopAndBagItems();
        }

        private void ActualizeShopAndBagItems()
        {
            ExchangeRequestOnShopStockMessage packetshop = new ExchangeRequestOnShopStockMessage();
            Account.SocketManager.Send(packetshop);
            LeaveDialogRequestMessage packetleave = new LeaveDialogRequestMessage();
            Account.SocketManager.Send(packetleave);
        }

        private void IsLockingFight_CheckedChanged(object sender)
        {
            Account.Config.LockingFights = IsLockingFight.Checked;
            Account.LockPerformed = false;
        }

        private void WithItemSetBox_CheckedChanged(object sender)
        {
            Account.Config.StartFightWithItemSet = WithItemSetBox.Checked;
            Account.Config.StartFightWithItemSet = WithItemSetBox.Checked;
            Account.Config.PresetStartUpId = (byte)PresetStartUpD.Value;
            Account.Config.PresetEndUpId = (sbyte)PresetEndUpD.Value;
        }

        private void PresetStartUpD_ValueChanged(object sender, EventArgs e)
        {
            Account.Config.PresetStartUpId = (byte)PresetStartUpD.Value;
        }

        private void PresetEndUpD_ValueChanged(object sender, EventArgs e)
        {
            Account.Config.PresetEndUpId = (sbyte)PresetEndUpD.Value;
        }

        private void NUDTimeoutFight_ValueChanged(object sender, EventArgs e)
        {
            Account.Config.BotSpeed = (int)NUDTimeoutFight.Value;
        }

        private void SaveConfig_Click_1(object sender, EventArgs e)
        {
            Account.ConfigRecover.SaveConfig();
            MessageBox.Show("Configuration saved!");
        }

        private void DeleteConfigBt_Click_1(object sender, EventArgs e)
        {
            Account.ConfigRecover.DeleteConfig();
            MessageBox.Show("Configuration deleted!");
        }

        private void DebugMode_CheckedChanged(object sender)
        {
            Account.Config.DebugMode = DebugMode.Checked;
        }

        private void LogCb_CheckedChanged(object sender)
        {
            Account.Config.LogConsoleToText = LogCb.Checked;
        }

        private void NUDVerbose_ValueChanged(object sender, EventArgs e)
        {
            Account.Config.VerboseLevel = (int)NUDVerbose.Value;
        }

        private void RegenChoice_ValueChanged(object sender, EventArgs e)
        {
            Account.Config.RegenChoice = (int)RegenChoice.Value;
        }

        private void nudMinMonstersNumber_ValueChanged(object sender, EventArgs e)
        {
            Account.Config.MinMonstersNumber = (int)nudMinMonstersNumber.Value;
        }

        private void nudMinMonstersLevel_ValueChanged(object sender, EventArgs e)
        {
            Account.Config.MinMonstersLevel = (int)nudMinMonstersLevel.Value;
        }

        private void nudMaxMonstersNumber_ValueChanged(object sender, EventArgs e)
        {
            Account.Config.MaxMonstersNumber = (int)nudMaxMonstersNumber.Value;
        }

        private void nudMaxMonstersLevel_ValueChanged(object sender, EventArgs e)
        {
            Account.Config.MaxMonstersLevel = (int)nudMaxMonstersLevel.Value;
        }

        private void PhraseADire_TextChanged(object sender, EventArgs e)
        {
            Account.Config.SentenceToSay = PhraseADire.Text;
        }

        private void HouseSearcherBox_CheckedChanged(object sender)
        {
            Account.Config.HouseSearcherEnabled = HouseSearcherBox.Checked;
        }

        private void MaxPrice_ValueChanged(object sender, EventArgs e)
        {
            Account.Config.MaxPriceHouse = (ulong)MaxPrice.Value;
        }

        private void LockForParty_CheckedChanged(object sender)
        {
            Account.Config.LockingForGroupOnly = LockForParty.Checked;
            Account.LockPerformed = false;
        }

        private void LockSpectator_CheckedChanged(object sender)
        {
            Account.Config.LockingSpectators = LockSpectator.Checked;
            Account.LockPerformed = false;
        }

        private void AskForHelp_CheckedChanged(object sender)
        {
            Account.Config.AskForHelp = AskForHelp.Checked;
            Account.LockPerformed = false;
        }

        #endregion

        #region Public methods

        public void Actualizeshop(List<ObjectItemToSell> sell)
        {
            BeginInvoke(new MethodInvoker(LVItemShop.Items.Clear));

            foreach (ObjectItemToSell i in sell)
            {
                Core.Inventory.Item item = new Core.Inventory.Item(i.Effects.ToList(), i.ObjectGID, 0, (int)i.Quantity, (int)i.ObjectUID);
                string[] row1 = { item.GID.ToString(), item.UID.ToString(), item.Name, item.Quantity.ToString(), item.Type, i.ObjectPrice.ToString() };
                ListViewItem li = new ListViewItem(row1);
                li.ToolTipText = item.Description;
                AddItem(li, LVItemShop);
            }
            ResizeGrid(LVItemShop);
        }

        public void AddItemToShop()
        {
            if (LVItemBag.InvokeRequired)
            {
                Invoke(new Callback(AddItemToShop));
                return;
            }
            for (int i = 0; i < LVItemBag.Items.Count; i++)
            {
                if (LVItemBag.Items[i].Selected)
                {
                    try
                    {
                        ExchangeObjectMovePricedMessage msg = new ExchangeObjectMovePricedMessage();
                        Core.Inventory.Item item = Account.Inventory.GetItemFromUID(Convert.ToInt32(LVItemBag.Items[i].SubItems[1].Text));
                        msg.ObjectUID = (uint)item.UID;
                        msg.Quantity = Account.Inventory.GetItemFromName(LVItemBag.Items[i].SubItems[2].Text).Quantity;
                        msg.Price = Convert.ToUInt64(numericUpDown1.Value);
                        Account.SocketManager.Send(msg);
                        Account.Log(new ActionTextInformation(Strings.AdditionOf + Account.Inventory.GetItemFromName(LVItemBag.Items[i].SubItems[2].Text).Name + "(x " + Account.Inventory.GetItemFromName(LVItemBag.Items[i].SubItems[2].Text).Quantity + ") " + Strings.InTheStoreAtThePriceOf + " : " + msg.Price + " " + Strings.Kamas), 2);
                        LeaveDialogRequestMessage packetleave = new LeaveDialogRequestMessage();
                        Account.Wait(2000);
                        Account.SocketManager.Send(packetleave);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        public void UpdateStatus()
        {
            string nstatus = String.Empty;
            switch (Account.State)
            {
                case Status.None:
                    nstatus = Strings.Connected;
                    break;
                case Status.Exchanging:
                    nstatus = "Exchanging";
                    break;
                case Status.Fighting:
                    nstatus = "Fighting";
                    break;
                case Status.Gathering:
                    nstatus = "Harvesting";
                    break;
                case Status.Moving:
                    nstatus = "Moving";
                    break;
                case Status.Speaking:
                    nstatus = "Speaking";
                    break;
                case Status.Teleporting:
                    nstatus = "Teleporting";
                    break;
                case Status.Regenerating:
                    nstatus = Strings.Regenerating;
                    break;
                case Status.Disconnected:
                    nstatus = Strings.Disconnected;
                    break;
                case Status.Busy:
                    nstatus = "Busy";
                    break;
            }
            nstatus = Core.Engine.Constants.Translate.GetTranslation(nstatus);
            Invoke(new DelegLabel(ModLabel), nstatus, StatusLb);
        }

        public void ActualizeInventory()
        {
            BeginInvoke(new MethodInvoker(LVItems.Items.Clear));
            foreach (MageBot.Core.Inventory.Item i in Account.Inventory.Items)
            {
                string[] row1 = { i.GID.ToString(), i.UID.ToString(), i.Name, i.Quantity.ToString(), i.Type.ToString(), i.Price.ToString() };
                System.Windows.Forms.ListViewItem li = new System.Windows.Forms.ListViewItem(row1);
                li.ToolTipText = i.Description;
                AddItem(li, LVItems);
            }
            ResizeGrid(LVItems);
            RegenUC.RefreshQuantity();

            BeginInvoke(new MethodInvoker(LVItemBag.Items.Clear));
            foreach (MageBot.Core.Inventory.Item i in Account.Inventory.Items)
            {
                string[] row1 = { i.GID.ToString(), i.UID.ToString(), i.Name, i.Quantity.ToString(), i.Type.ToString(), i.Price.ToString() };
                System.Windows.Forms.ListViewItem li = new System.Windows.Forms.ListViewItem(row1);
                li.ToolTipText = i.Description;
                AddItem(li, LVItemBag);
            }
            ResizeGrid(LVItemBag);
        }

        public void ActualizeFamis()
        {
            if (labelNextMeal.InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(ActualizeFamis));
                return;
            }
            if (Account.NextMeal.Year != 1)
                Invoke(new DelegLabel(ModLabel), "Next meal in : " + Account.NextMeal.ToShortTimeString(), labelNextMeal);
            else
                Invoke(new DelegLabel(ModLabel), "No next meal", labelNextMeal);

            Invoke(new DelegLabel(ModLabel), Account.Safe != null ? "Safe: Yes " : " Safe: No", labelSafe);

            if (listViewPets.InvokeRequired)
                BeginInvoke(new MethodInvoker(listViewPets.Items.Clear));
            else
                listViewPets.Items.Clear();

            if ((Account.PetsList != null) && (Account.PetsList.Count != 0))
            {
                foreach (Pet pet in Account.PetsList)
                {
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.SubItems[0].Text = I18N.GetText((int)pet.Datas.Fields["nameId"]);
                    listViewItem.SubItems.Add(pet.Informations.UID.ToString());

                    if (pet.FoodList.Count != 0)
                        listViewItem.SubItems.Add(I18N.GetText((int)pet.FoodList[0].Datas.Fields["nameId"]) + " (" + pet.FoodList[0].Informations.Quantity + ")");
                    else
                        listViewItem.SubItems.Add("None (0)");

                    if (pet.NextMeal.Year != 1)
                    {
                        DateTime nextMeal = new DateTime(pet.NextMeal.Year, pet.NextMeal.Month, pet.NextMeal.Day,
                            pet.NextMeal.Hour, pet.NextMeal.Minute, 0);

                        listViewItem.SubItems.Add(nextMeal.ToShortDateString() + " " + nextMeal.ToShortTimeString());
                    }
                    else
                        listViewItem.SubItems.Add("No next meal.");

                    listViewItem.SubItems.Add(pet.Effect);

                    AddItem(listViewItem, listViewPets);
                    //if (listViewPets.Items.Count != 0)
                    //    listViewPets.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            }
            ResizeGrid(listViewPets);
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

        public void ModifBar(int Bar, int Max, int value, string text)
        {
            if (VitaBar.InvokeRequired || XpBar.InvokeRequired ||
                PodsBar.InvokeRequired || KamasLabel.InvokeRequired)
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

        public void InitMITM()
        {
            Account.SocketManager = new SocketManager(Account);
            Account.SocketManager.InitMITM();

        }

        public void TryReconnect(int secondes)
        {
            Account.TryReconnect(secondes);
        }

        public void GetNextMeal()
        {
            Account.GetNextMeal();
        }

        public void StartFeeding()
        {
            Account.Running = new Running(Account);
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

        public void TryFeeding()
        {
            Account.Running = new Running(Account);
            Account.Running.Init();
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

        public void ActualizeMap()
        {
            BeginInvoke(new MethodInvoker(MapView.Items.Clear));
            var join = Account.MapData.InteractiveElements.Values.Join
                (Account.MapData.StatedElements.Values,
                 interactive => (int)interactive.Id,
                 stated => stated.Id,
                 (interactive, stated) => new
                 {
                     Id = interactive.Id,
                     Name = interactive.Name,
                     TypeId = interactive.TypeId,
                     Type = interactive.Type,
                     IsUsable = interactive.IsUsable,
                     EnabledSkills = interactive.EnabledSkills,
                     DisabledSkills = interactive.DisabledSkills,
                     CellId = stated.CellId,
                     State = stated.State
                 }).ToList().OrderBy(a => a.CellId);
            foreach (var e in join)
            {
                AddItem(new ListViewItem(new string[] { Convert.ToString(e.Id), e.Name, e.CellId.ToString(), e.Type }), MapView);
            }
            foreach (GameRolePlayNpcInformations n in Account.MapData.Npcs.Values)
            {
                AddItem(new ListViewItem(new string[] { Convert.ToString(n.NpcId), I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Npcs, n.NpcId).Fields["nameId"]), Convert.ToString(n.Disposition.CellId), "NPC" }), MapView);
            }
            foreach (GameRolePlayCharacterInformations c in Account.MapData.Players.Values)
            {
                AddItem(new ListViewItem(new string[] { Convert.ToString("[CID]:" + c.ContextualId + "[ACCID]:" + c.AccountId), c.Name, Convert.ToString(c.Disposition.CellId), "Player" }), MapView);
            }
            ResizeGrid(MapView);
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
                    JobUC uc = new JobUC(this, j);
                    JobsUC.Add(uc);
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
                        if (j.Level >= (int)d.Fields["levelMin"])
                        {
                            string name = I18N.GetText((int)d.Fields["nameId"]);
                            foreach (int c in (ArrayList)d.Fields["craftableItemIds"])
                            {
                                string rname = "Unknown";
                                DataClass data = GameData.GetDataObject(D2oFileEnum.Items, c);
                                if (data != null && j.Level >= (int)data.Fields["level"])
                                {
                                    rname = I18N.GetText((int)data.Fields["nameId"]);
                                    uc.gg.Rows.Add(name, rname, c);
                                }
                            }
                        }
                    }
                    uc.g.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    uc.g.Columns[2].Visible = false;
                    uc.gg.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    uc.gg.Columns[2].Visible = false;
                    uc.Show();
                }
            }
        }

        public void Enable(bool param1)
        {
            if (InvokeRequired)
                Invoke(new DelegBool(Enable), param1);
            else
                Enabled = param1;
        }

        #endregion

        #region Private methods
        private void Connect()
        {
            if (Account.Config.IsSocket)
                Account.Init();
            else if (Account.Config.IsMITM)
                Account.InitMITM();
        }

        private void Log(TextInformation text, int levelVerbose)
        {
            if (IsDisposed == true)
                return;
            if ((int)NUDVerbose.Value < levelVerbose)
                return;
            if (LogConsole.InvokeRequired)
                Invoke(new SetLogsCallback(Log), text, levelVerbose);
            else
            {

                text.Text = Core.Engine.Constants.Translate.GetTranslation(text.Text);
                text.Text = "[" + DateTime.Now.ToLongTimeString() +
                    "] (" + text.Category + ") " + text.Text;
                if (LogCb.Checked)
                    using (StreamWriter fileWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MageBot\Logs" + DateTime.Now.ToShortDateString().Replace("/", "-") + "_" + Account.CharacterBaseInformations.Name + ".txt", true))
                        fileWriter.WriteLine(text.Text);
                if (text.Category == "Debug" && !DebugMode.Checked)
                    return;

                int startIndex = LogConsole.TextLength;

                LogConsole.AppendText(text.Text + "\r\n");
                LogConsole.Select(LogConsole.Text.Length, 0);
                LogConsole.ScrollToCaret();

                LogConsole.SelectionStart = startIndex;
                LogConsole.SelectionLength = text.Text.Length;
                LogConsole.SelectionColor = text.Color;
            }
        }

        private Operator ConvertOperator(string oper)
        {
            switch (oper)
            {
                case ">": return Operator.More;
                case "<": return Operator.Less;
                case ">=": return Operator.MoreEqual;
                case "<=": return Operator.LessEqual;
                case "=": return Operator.Equal;
                case "<>": return Operator.Different;
                default: return Operator.None;
            }
        }

        private void FillAccountEvents()
        {
            Account.LogChanged += Account_LogChanged;
            Account.InfBarsChanged += Account_InfBarsChanged;
            Account.ActualizePets += Account_ActualizePets;
            Account.ActualizeMap += Account_ActualizeMap;
            Account.ActualizeInventory += Account_ActualizeInventory;
            Account.ActualizeShop += Account_ActualizeShop;
            Account.ActualizeJobs += Account_ActualizeJobs;
            Account.LoggerClear += Account_loggerClear;
            Account.StatusChanged += Account_StatusChanged;
            Account.AccountRestart += Account_AccountRestart;
            Account.ApplicationWait += Account_ApplicationWait;
            Account.ConfigRecovered += Account_ConfigRecovered;
            Account.UpdateStats += Account_UpdateStats;
            //TODO: Fill other modules events
        }

        private void FillAccountInitialSettings()
        {
            Account.Config.VerboseLevel = (int)NUDVerbose.Value;
            Account.Config.RegenChoice = (int)RegenChoice.Value;
            Account.Config.BotSpeed = (int)NUDTimeoutFight.Value;
            Account.Config.MaxMonstersNumber = (int)nudMaxMonstersNumber.Value;
            Account.Config.MaxMonstersLevel = (int)nudMaxMonstersLevel.Value;
            Account.Config.MaxPriceHouse = (ulong)MaxPrice.Value;
            Account.Config.PresetStartUpId = (byte)PresetStartUpD.Value;
            Account.Config.PresetEndUpId = (sbyte)PresetEndUpD.Value;
            Account.Config.BotSpeed = (int)NUDTimeoutFight.Value;
            Account.Config.FloodInterval = 60;
            Account.Config.AutoDeletionTime = (int)GestItemsUC.NUDAutoDeletion.Value;
            Account.Config.DisconnectWhenRun = true;
            Account.Config.ItemToUseWhenRun = PotionEnum.MemoryPotion;
            //TODO : Fill other modules initial settings
        }

        private void ResizeGrid(ListView grid)
        {
            if (grid.InvokeRequired)
                Invoke(new DelegListViewResize(ResizeGrid), grid);
            else
            {
                foreach (ColumnHeader ch in grid.Columns)
                {
                    ch.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            }
        }

        private void Wait(int milisec)
        {
            Random Random = new Random();
            double endwait = Environment.TickCount + milisec;
            while (Environment.TickCount < endwait)
            {
                System.Threading.Thread.Sleep(1);
                Application.DoEvents();
            }
        }

        private void LaunchPath()
        {
            if (Account.Path != null)
            {
                if (Account.MyGroup != null)
                {
                    Account.MyGroup.Path = Account.Path;
                    Account.MyGroup.Path.Start();
                }
                else
                {
                    Account.Path.Start();
                }
            }
            else
                Account.Log(new ErrorTextInformation("No Path loaded"), 3);
        }

        #region Account Object events

        private void Account_ApplicationWait(object sender, EventArgs e)
        {
            ApplicationWaitEventArgs args = (ApplicationWaitEventArgs)e;
            Wait(args.Milisec);
        }

        private void Account_AccountRestart(object sender, EventArgs e)
        {
            AccountConfig accConf = Account.Config;
            string userName = Account.AccountName;
            string pass = Account.AccountPassword;
            Account = null;
            Account = new Account(userName, pass);
            FillAccountEvents();
            FillAccountInitialSettings();
            Account.Config = accConf;
            Account.Init();
        }

        private void Account_StatusChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void Account_loggerClear(object sender, EventArgs e)
        {
            if (LogConsole.InvokeRequired)
                Invoke(new MethodInvoker(LogConsole.Clear));
            else
                LogConsole.Clear();
        }

        private void Account_ActualizeJobs(object sender, EventArgs e)
        {
            ActualizeJobs();
        }

        private void Account_ActualizeShop(object sender, EventArgs e)
        {
            ActualizeShopItemsEventArgs args = (ActualizeShopItemsEventArgs)e;
            Actualizeshop(args.ObjectsInfos);
        }

        private void Account_ActualizeInventory(object sender, EventArgs e)
        {
            ActualizeInventory();
        }

        private void Account_ActualizeMap(object sender, EventArgs e)
        {
            ActualizeMap();
        }

        private void Account_ActualizePets(object sender, EventArgs e)
        {
            ActualizeFamis();
        }

        private void Account_InfBarsChanged(object sender, EventArgs e)
        {
            foreach (KeyValuePair<int, DataBar> d in Account.InfBars)
            {
                ModifBar(d.Key, d.Value.Max, d.Value.Value, d.Value.Text);
            }
        }

        private void Account_LogChanged(object sender, EventArgs e)
        {
            LogEventArgs args = (LogEventArgs)e;
            Log(args.Text, args.VerboseLevel);
        }

        private void Account_ConfigRecovered(object sender, EventArgs e)
        {
            DebugMode.Checked = Account.Config.DebugMode;
            LogCb.Checked = Account.Config.LogConsoleToText;
            nudMinMonstersLevel.Value = Account.Config.MinMonstersLevel;
            nudMaxMonstersLevel.Value = Account.Config.MaxMonstersLevel;
            nudMinMonstersNumber.Value = Account.Config.MinMonstersNumber;
            nudMaxMonstersNumber.Value = Account.Config.MaxMonstersNumber;
            MonstersRestrictionsView.Clear();
            foreach (MonsterRestrictions mr in Account.Config.MonsterRestrictions)
            {
                string[] row = { mr.MonsterName,
                                 mr.Operator.Description(),
                                 Convert.ToString(mr.Quantity),
                                 mr.RestrictionLevel.Description() };
                MonstersRestrictionsView.Items.Add(new ListViewItem(row));
            }
            RegenChoice.Value = Account.Config.RegenChoice;
            WithItemSetBox.Checked = Account.Config.EndFightWithItemSet || Account.Config.StartFightWithItemSet;
            PresetEndUpD.Value = Account.Config.PresetEndUpId;
            PresetStartUpD.Value = Account.Config.PresetStartUpId;
            NUDTimeoutFight.Value = Account.Config.BotSpeed;
            IsLockingFight.Checked = Account.Config.LockingFights;
            IsLockingFight.Checked = Account.Config.LockingFights;
            LockForParty.Checked = Account.Config.LockingForGroupOnly;
            AskForHelp.Checked = Account.Config.AskForHelp;
            LockSpectator.Checked = Account.Config.LockingSpectators;
            checkBoxBegin.Checked = Account.Config.Begin;
            MaxPrice.Value = Account.Config.MaxPriceHouse;
            PhraseADire.Text = Account.Config.SentenceToSay;
            HouseSearcherBox.Checked = Account.Config.HouseSearcherEnabled;
            SearcherLogBox.Text = Account.Config.HouseSearcherLogPath;
            sadikCheckbox2.Checked = Account.Config.RelaunchPath;
            if (!String.IsNullOrEmpty(Account.Config.PreLoadedAI))
            {
                Account.FightParser = new FightParser(Account, Account.Config.PreLoadedAI, Account.Config.PreLoadedAIName);
                Account.Log(new BotTextInformation("AI loaded : " + Account.Config.PreLoadedAIName), 0);
                if (NomIA.InvokeRequired)
                    Invoke(new Callback(() => NomIA.Text = Account.FightParser.Name));
                Account.Fight = new BFight(Account, Account.FightParser, Account.FightData);
            }
            if (!String.IsNullOrEmpty(Account.Config.PreLoadedPath))
            {
                Account.Path = new Core.Path.PathManager(Account, Account.Config.PreLoadedPath, Account.Config.PreLoadedPathName);
                Account.Log(new BotTextInformation("Path loaded : " + Account.Config.PreLoadedPathName), 0);
                PathDownBt.Text = Account.Path.PathName;
            }
            if (Account.Config.WaitingForTheSale)
                Account.House = new HouseBuy(Account);
            CaracUC.FillRecoveredConfig();
            FloodUC.FillRecoveredConfig();
            GestItemsUC.FillRecoveredConfig();
            HeroicUC.FillRecoveredConfig();

            //TODO: Set other modules config by recovered config

        }

        private void Account_UpdateStats(object sender, EventArgs e)
        {
            if (CaracUC != null)
                CaracUC.Init();
        }


        #endregion

        #endregion

        private void metroTabPage9_Click(object sender, EventArgs e)
        {

        }

        private void sadikCheckbox2_CheckedChanged(object sender)
        {
            Account.Config.RelaunchPath = sadikCheckbox2.Checked;
        }
    }
}

