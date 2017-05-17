using System.Collections.Generic;
using System.Windows.Forms;
using MageBot.Core.Job;
using Util.Util.Text.Log;

namespace MageBot.Interface
{
    public partial class JobUC : MetroFramework.Controls.MetroUserControl
    {
        /// <summary>
        /// Represents the Jobs tab of the main accountUC.
        /// </summary>

        #region Fields
        private AccountUC accUserControl;
        private Job job;
        #endregion

        #region Constructors
        public JobUC(AccountUC Account, Job j , List<TreeNode> nodes = null)
        {
            InitializeComponent();
            accUserControl = Account;
            accUserControl.Account.Jobs = new List<Job>();
            job = j;
            accUserControl.Account.Jobs.Add(job);
            sadikTabControl1.TabPages[0].Controls.Add(g);
            sadikTabControl1.TabPages[1].Controls.Add(gg);
            Dock = DockStyle.Fill;
            g.Columns.Add("SkillName", "Skills");
            g.Columns.Add("RessourceName", "Ressources");
            g.Columns.Add("RessourceId", "Id");
            g.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Select",  HeaderText = "Gather"});
            g.Columns[1].Width = 200;
            g.MultiSelect = false;

            gg.Columns.Add("SkillName", "Skills");
            gg.Columns.Add("RecipeName", "Recettes");
            gg.Columns[1].Width = 200;
            gg.Columns.Add("RecipeId", "Id");
            gg.ReadOnly = true;

            //Engine.Constants.Translate.TranslateUC(this);
        }
        #endregion

        #region Public Methods
        public bool HasRightTool()
        {
            bool h = job.HasRightTool();
            if (!h)
                accUserControl.Log(new ErrorTextInformation("L'outil n'est pas équipé :( "),0);
            return h;
        }
        #endregion
    }
}
