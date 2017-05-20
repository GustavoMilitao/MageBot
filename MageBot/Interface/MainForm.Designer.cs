using Util.Util.I18n.Strings;
using System.Threading;

namespace MageBot.Interface
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AccountsBt = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.GroupsBt = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.LanguageChoice = new System.Windows.Forms.ToolStripComboBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Account.ico");
            this.imageList.Images.SetKeyName(1, "Add Account.ico");
            this.imageList.Images.SetKeyName(2, "Delete Account.ico");
            this.imageList.Images.SetKeyName(3, "Home.ico");
            this.imageList.Images.SetKeyName(4, "Manager.ico");
            this.imageList.Images.SetKeyName(5, "Empty.ico");
            this.imageList.Images.SetKeyName(6, "Begin.ico");
            this.imageList.Images.SetKeyName(7, "Delete.ico");
            this.imageList.Images.SetKeyName(8, "Stop.ico");
            this.imageList.Images.SetKeyName(9, "1391210883_5892.ico");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AccountsBt,
            this.toolStripSeparator1,
            this.GroupsBt,
            this.toolStripSeparator2,
            this.LanguageChoice});
            this.toolStrip1.Location = new System.Drawing.Point(20, 60);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(760, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // AccountsBt
            // 
            this.AccountsBt.Image = global::MageBot.Resources.account;
            this.AccountsBt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AccountsBt.Name = "AccountsBt";
            this.AccountsBt.Size = new System.Drawing.Size(77, 22);
            this.AccountsBt.Text = "Accounts";
            this.AccountsBt.ToolTipText = "Account management";
            this.AccountsBt.Click += new System.EventHandler(this.AccountsBt_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // GroupsBt
            // 
            this.GroupsBt.Image = global::MageBot.Resources.group;
            this.GroupsBt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GroupsBt.Name = "GroupsBt";
            this.GroupsBt.Size = new System.Drawing.Size(65, 22);
            this.GroupsBt.Text = "Groups";
            this.GroupsBt.Click += new System.EventHandler(this.GroupsBt_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // LanguageChoice
            // 
            this.LanguageChoice.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.LanguageChoice.AutoCompleteCustomSource.AddRange(new string[] {
            "fr-FR",
            "en-US",
            "es-ES",
            "pt-BR"});
            this.LanguageChoice.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.LanguageChoice.Items.AddRange(new object[] {
            "fr-FR",
            "en-US",
            "es-ES",
            "pt-BR"});
            this.LanguageChoice.MaxDropDownItems = 2;
            this.LanguageChoice.Name = "LanguageChoice";
            this.LanguageChoice.Size = new System.Drawing.Size(140, 25);
            this.LanguageChoice.Text = "Change the language";
            this.LanguageChoice.SelectedIndexChanged += new System.EventHandler(this.LanguageChoice_SelectedTextChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "MageBot is now minimized in the system tray";
            this.notifyIcon.BalloonTipTitle = "[Info]";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "MageBot";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 700);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "MageBot";
            this.TransparencyKey = System.Drawing.Color.Empty;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton AccountsBt;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton GroupsBt;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripComboBox LanguageChoice;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

