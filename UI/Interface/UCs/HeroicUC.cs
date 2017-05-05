namespace BlueSheep.Interface
{
    using System;
    using System.ComponentModel;

    public class HeroicUC : MetroFramework.Controls.MetroUserControl
    {
        /// <summary>
        /// Represents the heroic mode tab of the main accountUC.
        /// </summary>

        #region Fields
        private IContainer components = null;
        private TextBox AllianceTxtBAgro;
        private SadikButton addAllianceAgro;
        private SadikLabel sadikLabel2;
        private NumericUpDown NUDLvlAgroMin;
        private NumericUpDown NUDLvlAgroMax;
        private SadikCheckbox sadikCheckbox1;
        public ListView LViewAgro;
        private GroupBox groupBox3;
        private SadikButton addAllianceRun;
        private TextBox AllianceTxtBRun;
        private SadikCombo UsedItem;
        private ListView LViewRun;
        private SadikLabel sadikLabel3;
        private SadikLabel sadikLabel4;
        private NumericUpDown NUDLvlRunMin;
        private NumericUpDown NUDLvlRunMax;
        private SadikRadioButton Disconnecting;
        private SadikRadioButton UsingItem;
        private SadikCheckbox sadikCheckbox2;
        private SadikLabel sadikLabel1;
        private GroupBox groupBox2;
        private SadikTabControl sadikTabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private AccountUC account;
        private int[] SubAreaId = new int[] { 0x5f, 0x60, 0x61, 0x62, 0xad, 0x65 };
        #endregion

        #region Constructors
        public HeroicUC(AccountUC Account)
        {
            InitializeComponent();
            account = Account;
        }
        #endregion

        #region Public Methods
        public void AnalysePacket(BlueSheep.Engine.Types.Message msg, byte[] packetdatas)
        {
            
            using (BigEndianReader reader = new BigEndianReader(packetdatas))
            {
                msg.Deserialize(reader);
            }
            switch ((int)msg.ProtocolID)
            {
                case 226:
                    MapComplementaryInformationsDataMessage packet = (MapComplementaryInformationsDataMessage)msg;
                    //if (this.GoAnalyser((int)packet.SubAreaId))
                    //{
                    foreach (GameRolePlayActorInformations informations in packet.actors)
                    {
                        GameRolePlayCharacterInformations infos;
                        if (!(informations is GameRolePlayCharacterInformations))
                            continue;
                        else
                            infos = (GameRolePlayCharacterInformations)informations;
                        if (GoAgro(infos))
                        {
                            Agression(informations.contextualId);
                        }
                        if (IsGoingToRun(infos))
                        {
                            if (Disconnecting.Checked)
                            {
                                account.SocketManager.DisconnectFromGUI();
                            }
                            else if (UsingItem.Checked && (UsedItem.Text.Length > 0))
                            {
                                Run();
                            }
                        }
                    }

                    break;
                case 5632:
                    GameRolePlayShowActorMessage npacket = (GameRolePlayShowActorMessage)msg;
                    GameRolePlayCharacterInformations infoCharacter = npacket.informations as GameRolePlayCharacterInformations;
                    if (GoAgro(infoCharacter))
                    {
                        Agression(infoCharacter.contextualId);
                    }
                    if (IsGoingToRun(infoCharacter))
                    {
                        if (Disconnecting.Checked)
                        {
                            account.SocketManager.DisconnectFromGUI();
                        }
                        else if (UsingItem.Checked && (UsedItem.Text.Length > 0))
                        {
                            Run();
                        }
                    }
                    break;

            }
        }
#endregion

        #region Private Methods
        private void Agression(int targetid)
        {
            GameRolePlayPlayerFightRequestMessage packet = new GameRolePlayPlayerFightRequestMessage
                            {
                                friendly = false,
                                targetCellId = -1,
                                targetId = targetid
                            };

                account.SocketManager.Send(packet);
        }

        private void Run()
        {
            account.Inventory.UseItem(SwitchUid(UsedItem.SelectedText));
        }

        private bool GoAgro(GameRolePlayCharacterInformations infoCharacter)
        {
            if (!sadikCheckbox1.Checked)
                return false;
            long num = Math.Abs((long)(infoCharacter.alignmentInfos.characterPower - infoCharacter.contextualId));
            bool flag = ((sadikCheckbox1.Checked && (infoCharacter.name != account.CharacterBaseInformations.name)) && (num >= NUDLvlAgroMin.Value) && (num <= NUDLvlAgroMax.Value));
            if (((LViewAgro.Items.Count > 0) && (infoCharacter.humanoidInfo.options[1] != null)) && flag)
            {
                HumanOptionAlliance alliance = infoCharacter.humanoidInfo.options[1] as HumanOptionAlliance;
                return (flag && ContainslistView(LViewAgro, alliance.allianceInformations.allianceName));
            }
            return flag;
        }

        //public bool GoAnalyser(int id)
        //{
        //    return ((!this.SubAreaId.Contains<int>(id) && (this.account.Game.Map.Id != 0x24138)) && (this.account.Game.Map.Id != 0x23423));
        //}

        private bool IsGoingToRun(GameRolePlayCharacterInformations infoCharacter)
        {
            if (!sadikCheckbox2.Checked)
                return false;
            if (infoCharacter.humanoidInfo.options[1] == null)
            {
                return false;
            }
            long num = Math.Abs((long)(infoCharacter.alignmentInfos.characterPower - infoCharacter.contextualId));
            bool flag = ((sadikCheckbox2.Checked && (infoCharacter.name != account.CharacterBaseInformations.name)) && (num >= NUDLvlRunMin.Value) && (num <= NUDLvlRunMax.Value));
            if (((LViewRun.Items.Count > 0) && (infoCharacter.humanoidInfo.options[1] != null)) && flag)
            {
                HumanOptionAlliance alliance = infoCharacter.humanoidInfo.options[1] as HumanOptionAlliance;
                return (flag && ContainslistView(LViewRun, alliance.allianceInformations.allianceName));
            }
            return flag;
        }

        private int SwitchUid(string nameItem)
        {
            switch (nameItem)
            {
                case "Potion de Rappel":
                    return account.Inventory.GetItemFromGID(0x224).UID;

                case "Potion de cité :  Bonta":
                    return account.Inventory.GetItemFromGID(0x1b35).UID;

                case "Potion de cité:  Brâkmar":
                    return account.Inventory.GetItemFromGID(0x1b34).UID;
            }
            return 0;
        }
        #endregion

        #region UI Methods
        private void addAllianceAgro_Click(object sender, EventArgs e)
        {
            if ((AllianceTxtBAgro.Text.Length > 0) && (LViewAgro.Items.Count < 50))
            {
                LViewAgro.Items.Add(AllianceTxtBAgro.Text);
                AllianceTxtBAgro.Text = "";
            }
        }

        private void addAllianceRun_Click(object sender, EventArgs e)
        {
            if ((AllianceTxtBRun.Text.Length > 0) && (LViewRun.Items.Count < 50))
            {
                LViewRun.Items.Add(AllianceTxtBRun.Text);
                AllianceTxtBRun.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (LViewAgro.SelectedItems.Count > 0)
            {
                LViewAgro.Items.Remove(LViewAgro.SelectedItems[0]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (LViewRun.SelectedItems.Count > 0)
            {
                LViewRun.Items.Remove(LViewRun.SelectedItems[0]);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            AllianceTxtBAgro = new System.Windows.Forms.TextBox();
            addAllianceAgro = new BlueSheep.Interface.SadikButton();
            sadikLabel2 = new BlueSheep.Interface.SadikLabel();
            NUDLvlAgroMin = new System.Windows.Forms.NumericUpDown();
            NUDLvlAgroMax = new System.Windows.Forms.NumericUpDown();
            sadikCheckbox1 = new BlueSheep.Interface.SadikCheckbox();
            LViewAgro = new System.Windows.Forms.ListView();
            groupBox3 = new System.Windows.Forms.GroupBox();
            addAllianceRun = new BlueSheep.Interface.SadikButton();
            AllianceTxtBRun = new System.Windows.Forms.TextBox();
            UsedItem = new BlueSheep.Interface.SadikCombo();
            LViewRun = new System.Windows.Forms.ListView();
            sadikLabel3 = new BlueSheep.Interface.SadikLabel();
            sadikLabel4 = new BlueSheep.Interface.SadikLabel();
            NUDLvlRunMin = new System.Windows.Forms.NumericUpDown();
            NUDLvlRunMax = new System.Windows.Forms.NumericUpDown();
            Disconnecting = new BlueSheep.Interface.SadikRadioButton();
            UsingItem = new BlueSheep.Interface.SadikRadioButton();
            sadikCheckbox2 = new BlueSheep.Interface.SadikCheckbox();
            sadikLabel1 = new BlueSheep.Interface.SadikLabel();
            groupBox2 = new System.Windows.Forms.GroupBox();
            sadikTabControl1 = new BlueSheep.Interface.SadikTabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(NUDLvlAgroMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(NUDLvlAgroMax)).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(NUDLvlRunMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(NUDLvlRunMax)).BeginInit();
            groupBox2.SuspendLayout();
            sadikTabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // AllianceTxtBAgro
            // 
            AllianceTxtBAgro.Location = new System.Drawing.Point(18, 68);
            AllianceTxtBAgro.Name = "AllianceTxtBAgro";
            AllianceTxtBAgro.Size = new System.Drawing.Size(286, 20);
            AllianceTxtBAgro.TabIndex = 30;
            // 
            // addAllianceAgro
            // 
            addAllianceAgro.ButtonStyle = BlueSheep.Interface.SadikButton.Style.Blue;
            addAllianceAgro.Cursor = System.Windows.Forms.Cursors.Hand;
            addAllianceAgro.Font = new System.Drawing.Font("Verdana", 8F);
            addAllianceAgro.Image = null;
            addAllianceAgro.Location = new System.Drawing.Point(18, 94);
            addAllianceAgro.Name = "addAllianceAgro";
            addAllianceAgro.RoundedCorners = false;
            addAllianceAgro.Size = new System.Drawing.Size(286, 22);
            addAllianceAgro.TabIndex = 29;
            addAllianceAgro.Text = "Ajouter alliance/guilde";
            addAllianceAgro.Click += new System.EventHandler(addAllianceAgro_Click);
            // 
            // sadikLabel2
            // 
            sadikLabel2.AutoSize = true;
            sadikLabel2.Font = new System.Drawing.Font("Verdana", 8F);
            sadikLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            sadikLabel2.Location = new System.Drawing.Point(228, 47);
            sadikLabel2.Name = "sadikLabel2";
            sadikLabel2.Size = new System.Drawing.Size(14, 13);
            sadikLabel2.TabIndex = 20;
            sadikLabel2.Text = "à";
            // 
            // NUDLvlAgroMin
            // 
            NUDLvlAgroMin.Location = new System.Drawing.Point(171, 45);
            NUDLvlAgroMin.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            NUDLvlAgroMin.Name = "NUDLvlAgroMin";
            NUDLvlAgroMin.Size = new System.Drawing.Size(51, 20);
            NUDLvlAgroMin.TabIndex = 18;
            // 
            // NUDLvlAgroMax
            // 
            NUDLvlAgroMax.Location = new System.Drawing.Point(248, 45);
            NUDLvlAgroMax.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            NUDLvlAgroMax.Name = "NUDLvlAgroMax";
            NUDLvlAgroMax.Size = new System.Drawing.Size(44, 20);
            NUDLvlAgroMax.TabIndex = 17;
            // 
            // sadikCheckbox1
            // 
            sadikCheckbox1.Checked = false;
            sadikCheckbox1.Font = new System.Drawing.Font("Verdana", 8F);
            sadikCheckbox1.Location = new System.Drawing.Point(18, 19);
            sadikCheckbox1.Name = "sadikCheckbox1";
            sadikCheckbox1.Size = new System.Drawing.Size(210, 20);
            sadikCheckbox1.TabIndex = 16;
            sadikCheckbox1.Text = "Activer";
            // 
            // LViewAgro
            // 
            LViewAgro.Location = new System.Drawing.Point(18, 122);
            LViewAgro.Name = "LViewAgro";
            LViewAgro.Size = new System.Drawing.Size(286, 93);
            LViewAgro.TabIndex = 15;
            LViewAgro.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(addAllianceRun);
            groupBox3.Controls.Add(AllianceTxtBRun);
            groupBox3.Controls.Add(UsedItem);
            groupBox3.Controls.Add(LViewRun);
            groupBox3.Controls.Add(sadikLabel3);
            groupBox3.Controls.Add(sadikLabel4);
            groupBox3.Controls.Add(NUDLvlRunMin);
            groupBox3.Controls.Add(NUDLvlRunMax);
            groupBox3.Controls.Add(Disconnecting);
            groupBox3.Controls.Add(UsingItem);
            groupBox3.Controls.Add(sadikCheckbox2);
            groupBox3.Location = new System.Drawing.Point(6, 6);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(374, 233);
            groupBox3.TabIndex = 18;
            groupBox3.TabStop = false;
            groupBox3.Text = "Fuite";
            // 
            // addAllianceRun
            // 
            addAllianceRun.ButtonStyle = BlueSheep.Interface.SadikButton.Style.Blue;
            addAllianceRun.Cursor = System.Windows.Forms.Cursors.Hand;
            addAllianceRun.Font = new System.Drawing.Font("Verdana", 8F);
            addAllianceRun.Image = null;
            addAllianceRun.Location = new System.Drawing.Point(215, 132);
            addAllianceRun.Name = "addAllianceRun";
            addAllianceRun.RoundedCorners = false;
            addAllianceRun.Size = new System.Drawing.Size(148, 26);
            addAllianceRun.TabIndex = 28;
            addAllianceRun.Text = "Ajouter alliance/guilde";
            addAllianceRun.Click += new System.EventHandler(addAllianceRun_Click);
            // 
            // AllianceTxtBRun
            // 
            AllianceTxtBRun.Location = new System.Drawing.Point(12, 132);
            AllianceTxtBRun.Name = "AllianceTxtBRun";
            AllianceTxtBRun.Size = new System.Drawing.Size(197, 20);
            AllianceTxtBRun.TabIndex = 27;
            // 
            // UsedItem
            // 
            UsedItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            UsedItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            UsedItem.Font = new System.Drawing.Font("Verdana", 8F);
            UsedItem.FormattingEnabled = true;
            UsedItem.ItemHeight = 20;
            UsedItem.Items.AddRange(new object[] {
            "Potion de cité : Bonta",
            "Potion de cité : Brâkmar",
            "Potion de rappel"});
            UsedItem.Location = new System.Drawing.Point(155, 94);
            UsedItem.Name = "UsedItem";
            UsedItem.Size = new System.Drawing.Size(208, 26);
            UsedItem.TabIndex = 26;
            // 
            // LViewRun
            // 
            LViewRun.Location = new System.Drawing.Point(9, 164);
            LViewRun.Name = "LViewRun";
            LViewRun.Size = new System.Drawing.Size(359, 63);
            LViewRun.TabIndex = 25;
            LViewRun.UseCompatibleStateImageBehavior = false;
            // 
            // sadikLabel3
            // 
            sadikLabel3.AutoSize = true;
            sadikLabel3.Font = new System.Drawing.Font("Verdana", 8F);
            sadikLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            sadikLabel3.Location = new System.Drawing.Point(212, 47);
            sadikLabel3.Name = "sadikLabel3";
            sadikLabel3.Size = new System.Drawing.Size(14, 13);
            sadikLabel3.TabIndex = 24;
            sadikLabel3.Text = "à";
            // 
            // sadikLabel4
            // 
            sadikLabel4.AutoSize = true;
            sadikLabel4.Font = new System.Drawing.Font("Verdana", 8F);
            sadikLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            sadikLabel4.Location = new System.Drawing.Point(15, 47);
            sadikLabel4.Name = "sadikLabel4";
            sadikLabel4.Size = new System.Drawing.Size(140, 13);
            sadikLabel4.TabIndex = 23;
            sadikLabel4.Text = "Personnages de niveau";
            // 
            // NUDLvlRunMin
            // 
            NUDLvlRunMin.Location = new System.Drawing.Point(161, 45);
            NUDLvlRunMin.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            NUDLvlRunMin.Name = "NUDLvlRunMin";
            NUDLvlRunMin.Size = new System.Drawing.Size(45, 20);
            NUDLvlRunMin.TabIndex = 22;
            // 
            // NUDLvlRunMax
            // 
            NUDLvlRunMax.Location = new System.Drawing.Point(232, 45);
            NUDLvlRunMax.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            NUDLvlRunMax.Name = "NUDLvlRunMax";
            NUDLvlRunMax.Size = new System.Drawing.Size(41, 20);
            NUDLvlRunMax.TabIndex = 21;
            // 
            // Disconnecting
            // 
            Disconnecting.Checked = false;
            Disconnecting.Font = new System.Drawing.Font("Verdana", 8F);
            Disconnecting.Location = new System.Drawing.Point(9, 74);
            Disconnecting.Name = "Disconnecting";
            Disconnecting.Size = new System.Drawing.Size(140, 20);
            Disconnecting.TabIndex = 2;
            Disconnecting.Text = "Déconnecter";
            // 
            // UsingItem
            // 
            UsingItem.Checked = false;
            UsingItem.Font = new System.Drawing.Font("Verdana", 8F);
            UsingItem.Location = new System.Drawing.Point(9, 100);
            UsingItem.Name = "UsingItem";
            UsingItem.Size = new System.Drawing.Size(140, 20);
            UsingItem.TabIndex = 1;
            UsingItem.Text = "Prendre une potion";
            // 
            // sadikCheckbox2
            // 
            sadikCheckbox2.Checked = false;
            sadikCheckbox2.Font = new System.Drawing.Font("Verdana", 8F);
            sadikCheckbox2.Location = new System.Drawing.Point(18, 19);
            sadikCheckbox2.Name = "sadikCheckbox2";
            sadikCheckbox2.Size = new System.Drawing.Size(140, 20);
            sadikCheckbox2.TabIndex = 0;
            sadikCheckbox2.Text = "Activer";
            // 
            // sadikLabel1
            // 
            sadikLabel1.AutoSize = true;
            sadikLabel1.Font = new System.Drawing.Font("Verdana", 8F);
            sadikLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            sadikLabel1.Location = new System.Drawing.Point(15, 47);
            sadikLabel1.Name = "sadikLabel1";
            sadikLabel1.Size = new System.Drawing.Size(140, 13);
            sadikLabel1.TabIndex = 19;
            sadikLabel1.Text = "Personnages de niveau";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(AllianceTxtBAgro);
            groupBox2.Controls.Add(addAllianceAgro);
            groupBox2.Controls.Add(sadikLabel2);
            groupBox2.Controls.Add(sadikLabel1);
            groupBox2.Controls.Add(NUDLvlAgroMin);
            groupBox2.Controls.Add(NUDLvlAgroMax);
            groupBox2.Controls.Add(sadikCheckbox1);
            groupBox2.Controls.Add(LViewAgro);
            groupBox2.Location = new System.Drawing.Point(29, 18);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(325, 221);
            groupBox2.TabIndex = 17;
            groupBox2.TabStop = false;
            groupBox2.Text = "Agression";
            // 
            // sadikTabControl1
            // 
            sadikTabControl1.Controls.Add(tabPage1);
            sadikTabControl1.Controls.Add(tabPage2);
            sadikTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            sadikTabControl1.Font = new System.Drawing.Font("Verdana", 8F);
            sadikTabControl1.ItemSize = new System.Drawing.Size(0, 30);
            sadikTabControl1.Location = new System.Drawing.Point(0, 0);
            sadikTabControl1.Name = "sadikTabControl1";
            sadikTabControl1.SelectedIndex = 0;
            sadikTabControl1.Size = new System.Drawing.Size(410, 283);
            sadikTabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = System.Drawing.Color.White;
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Location = new System.Drawing.Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(402, 245);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Aggression";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = System.Drawing.Color.White;
            tabPage2.Controls.Add(groupBox3);
            tabPage2.Location = new System.Drawing.Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(402, 245);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Fuite";
            // 
            // HeroicUC
            // 
            Controls.Add(sadikTabControl1);
            Name = "HeroicUC";
            Size = new System.Drawing.Size(410, 283);
            ((System.ComponentModel.ISupportInitialize)(NUDLvlAgroMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(NUDLvlAgroMax)).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(NUDLvlRunMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(NUDLvlRunMax)).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            sadikTabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);

        }

        private bool ContainslistView(ListView listView, string s)
        {
            ListViewItem item = new ListViewItem
            {
                Text = s
            };
            return listView.Items.Contains(item);
        }
        #endregion

    }
}

