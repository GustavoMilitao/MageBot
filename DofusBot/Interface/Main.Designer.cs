namespace DofusBot.Interface
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.accountPasswdLabel = new System.Windows.Forms.Label();
            this.accountNameTextBox = new System.Windows.Forms.TextBox();
            this.accountPasswdTextBox = new System.Windows.Forms.TextBox();
            this.connectionButton = new System.Windows.Forms.Button();
            this.accountNameLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // accountPasswdLabel
            // 
            this.accountPasswdLabel.AutoSize = true;
            this.accountPasswdLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountPasswdLabel.Location = new System.Drawing.Point(58, 47);
            this.accountPasswdLabel.Name = "accountPasswdLabel";
            this.accountPasswdLabel.Size = new System.Drawing.Size(92, 14);
            this.accountPasswdLabel.TabIndex = 1;
            this.accountPasswdLabel.Text = "Mot de passe";
            // 
            // accountNameTextBox
            // 
            this.accountNameTextBox.Location = new System.Drawing.Point(156, 19);
            this.accountNameTextBox.Name = "accountNameTextBox";
            this.accountNameTextBox.Size = new System.Drawing.Size(207, 21);
            this.accountNameTextBox.TabIndex = 2;
            // 
            // accountPasswdTextBox
            // 
            this.accountPasswdTextBox.Location = new System.Drawing.Point(156, 45);
            this.accountPasswdTextBox.Name = "accountPasswdTextBox";
            this.accountPasswdTextBox.PasswordChar = '*';
            this.accountPasswdTextBox.Size = new System.Drawing.Size(207, 21);
            this.accountPasswdTextBox.TabIndex = 3;
            // 
            // connectionButton
            // 
            this.connectionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.connectionButton.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectionButton.ForeColor = System.Drawing.Color.White;
            this.connectionButton.Location = new System.Drawing.Point(417, 12);
            this.connectionButton.Name = "connectionButton";
            this.connectionButton.Size = new System.Drawing.Size(178, 78);
            this.connectionButton.TabIndex = 10;
            this.connectionButton.Text = "Connexion";
            this.connectionButton.UseVisualStyleBackColor = false;
            this.connectionButton.Click += new System.EventHandler(this.ConnectionButton_Click);
            // 
            // accountNameLabel
            // 
            this.accountNameLabel.AutoSize = true;
            this.accountNameLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountNameLabel.Location = new System.Drawing.Point(42, 21);
            this.accountNameLabel.Name = "accountNameLabel";
            this.accountNameLabel.Size = new System.Drawing.Size(108, 14);
            this.accountNameLabel.TabIndex = 0;
            this.accountNameLabel.Text = "Nom de Compte";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.accountNameLabel);
            this.groupBox1.Controls.Add(this.accountPasswdLabel);
            this.groupBox1.Controls.Add(this.accountNameTextBox);
            this.groupBox1.Controls.Add(this.accountPasswdTextBox);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(387, 78);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Identifiants";
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.logTextBox.Location = new System.Drawing.Point(12, 96);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(583, 243);
            this.logTextBox.TabIndex = 13;
            this.logTextBox.Text = "";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(608, 349);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.connectionButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "DofusBot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label accountPasswdLabel;
        private System.Windows.Forms.TextBox accountNameTextBox;
        private System.Windows.Forms.TextBox accountPasswdTextBox;
        private System.Windows.Forms.Button connectionButton;
        private System.Windows.Forms.Label accountNameLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox logTextBox;
    }
}

