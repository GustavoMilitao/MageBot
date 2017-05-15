using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueSheep.New_Interface
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string version)
        {
            Text = "BlueSheep " + version;
            InitializeComponent();
        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(LanguageChoice.Text);
            //AccountsBt.Text = Strings.Accounts;
            //GroupsBt.Text = Strings.Groups;
        }
    }
}
