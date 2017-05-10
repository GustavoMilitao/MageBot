namespace BlueSheep.Interface
{
    partial class GroupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupForm));
            this.AccountTabs = new BlueSheep.Interface.SadikTabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.MasterChoice1 = new BlueSheep.Interface.SadikCombo();
            this.sadikLabel1 = new BlueSheep.Interface.SadikLabel();
            this.MasterChoice = new BlueSheep.Interface.SadikCombo();
            this.sadikLabel2 = new BlueSheep.Interface.SadikLabel();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // AccountTabs
            // 
            this.AccountTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AccountTabs.Font = new System.Drawing.Font("Verdana", 8F);
            this.AccountTabs.ItemSize = new System.Drawing.Size(0, 30);
            this.AccountTabs.Location = new System.Drawing.Point(20, 60);
            this.AccountTabs.Name = "AccountTabs";
            this.AccountTabs.SelectedIndex = 0;
            this.AccountTabs.Size = new System.Drawing.Size(935, 464);
            this.AccountTabs.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.MasterChoice1);
            this.tabPage2.Controls.Add(this.sadikLabel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(701, 419);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Configuration";
            // 
            // MasterChoice1
            // 
            this.MasterChoice1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.MasterChoice1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MasterChoice1.Font = new System.Drawing.Font("Verdana", 8F);
            this.MasterChoice1.FormattingEnabled = true;
            this.MasterChoice1.ItemHeight = 20;
            this.MasterChoice1.Location = new System.Drawing.Point(0, 0);
            this.MasterChoice1.Name = "MasterChoice1";
            this.MasterChoice1.Size = new System.Drawing.Size(189, 26);
            this.MasterChoice1.TabIndex = 3;
            // 
            // sadikLabel1
            // 
            this.sadikLabel1.AutoSize = true;
            this.sadikLabel1.Font = new System.Drawing.Font("Verdana", 8F);
            this.sadikLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.sadikLabel1.Location = new System.Drawing.Point(20, 17);
            this.sadikLabel1.Name = "sadikLabel1";
            this.sadikLabel1.Size = new System.Drawing.Size(161, 13);
            this.sadikLabel1.TabIndex = 1;
            this.sadikLabel1.Text = "Choix du chef de groupe : ";
            // 
            // MasterChoice
            // 
            this.MasterChoice.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.MasterChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MasterChoice.Font = new System.Drawing.Font("Verdana", 8F);
            this.MasterChoice.FormattingEnabled = true;
            this.MasterChoice.ItemHeight = 20;
            this.MasterChoice.Location = new System.Drawing.Point(709, 48);
            this.MasterChoice.Name = "MasterChoice";
            this.MasterChoice.Size = new System.Drawing.Size(243, 26);
            this.MasterChoice.TabIndex = 1;
            // 
            // sadikLabel2
            // 
            this.sadikLabel2.AutoSize = true;
            this.sadikLabel2.Font = new System.Drawing.Font("Verdana", 8F);
            this.sadikLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.sadikLabel2.Location = new System.Drawing.Point(706, 22);
            this.sadikLabel2.Name = "sadikLabel2";
            this.sadikLabel2.Size = new System.Drawing.Size(85, 13);
            this.sadikLabel2.TabIndex = 2;
            this.sadikLabel2.Text = "Master choice";
            // 
            // GroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 544);
            this.Controls.Add(this.sadikLabel2);
            this.Controls.Add(this.MasterChoice);
            this.Controls.Add(this.AccountTabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GroupForm";
            this.Text = "Groupe";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SadikTabControl AccountTabs;
        private System.Windows.Forms.TabPage tabPage2;
        private SadikLabel sadikLabel1;
        private SadikCombo MasterChoice1;
        private SadikCombo MasterChoice;
        private SadikLabel sadikLabel2;
    }
}