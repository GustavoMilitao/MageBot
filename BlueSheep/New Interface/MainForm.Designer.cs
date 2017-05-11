namespace BlueSheep.New_Interface
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AccountsBt = new System.Windows.Forms.ToolStripButton();
            this.GroupsBt = new System.Windows.Forms.ToolStripButton();
            this.LanguageChoice = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AccountsBt,
            this.GroupsBt,
            this.LanguageChoice});
            this.toolStrip1.Location = new System.Drawing.Point(20, 60);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(984, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // AccountsBt
            // 
            this.AccountsBt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AccountsBt.Image = global::BlueSheep.Resources.account;
            this.AccountsBt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AccountsBt.Name = "AccountsBt";
            this.AccountsBt.Size = new System.Drawing.Size(23, 22);
            this.AccountsBt.Text = "toolStripButton1";
            // 
            // GroupsBt
            // 
            this.GroupsBt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GroupsBt.Image = global::BlueSheep.Resources.group;
            this.GroupsBt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GroupsBt.Name = "GroupsBt";
            this.GroupsBt.Size = new System.Drawing.Size(23, 22);
            this.GroupsBt.Text = "toolStripButton2";
            // 
            // LanguageChoice
            // 
            this.LanguageChoice.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.LanguageChoice.Name = "LanguageChoice";
            this.LanguageChoice.Size = new System.Drawing.Size(121, 25);
            this.LanguageChoice.TextChanged += new System.EventHandler(this.toolStripComboBox1_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "BlueSheep 2.0";
            this.TransparencyKey = System.Drawing.Color.Empty;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton AccountsBt;
        private System.Windows.Forms.ToolStripButton GroupsBt;
        private System.Windows.Forms.ToolStripComboBox LanguageChoice;
    }
}