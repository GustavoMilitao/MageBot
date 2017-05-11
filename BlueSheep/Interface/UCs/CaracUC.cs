using System;
using System.Windows.Forms;
using BlueSheep.Util.Text.Log;
using BlueSheep.Common.Data.D2o;
using System.Collections;
using BlueSheep.Protocol.Messages.Game.Context.Roleplay.Stats;

namespace BlueSheep.Interface.UCs
{
    public partial class CaracUC : MetroFramework.Controls.MetroUserControl
    {
        #region Fields
        AccountUC account;
        private int m_count;
        #endregion

        private delegate void DelegLabel(string text, Label lab);

        #region Constructors
        public CaracUC(AccountUC Account)
        {
            InitializeComponent();
            account = Account;
        }
        #endregion

        #region Public Methods
        public void Init()
        {
            Invoke(new DelegLabel(ModLabel), Convert.ToString(account.CharacterStats.Vitality.Base + account.CharacterStats.Vitality.Additionnal + account.CharacterStats.Vitality.ObjectsAndMountBonus), VitaLb);
            Invoke(new DelegLabel(ModLabel), Convert.ToString(account.CharacterStats.Wisdom.Base + account.CharacterStats.Wisdom.Additionnal + account.CharacterStats.Wisdom.ObjectsAndMountBonus), WisdomLb);
            Invoke(new DelegLabel(ModLabel), Convert.ToString(account.CharacterStats.Strength.Base + account.CharacterStats.Strength.Additionnal + account.CharacterStats.Strength.ObjectsAndMountBonus), StreLb);
            Invoke(new DelegLabel(ModLabel), Convert.ToString(account.CharacterStats.Intelligence.Base + account.CharacterStats.Intelligence.Additionnal + account.CharacterStats.Intelligence.ObjectsAndMountBonus), InteLb);
            Invoke(new DelegLabel(ModLabel), Convert.ToString(account.CharacterStats.Chance.Base + account.CharacterStats.Chance.Additionnal + account.CharacterStats.Chance.ObjectsAndMountBonus), LuckLb);
            Invoke(new DelegLabel(ModLabel),  Convert.ToString(account.CharacterStats.Agility.Base + account.CharacterStats.Agility.Additionnal + account.CharacterStats.Agility.ObjectsAndMountBonus), AgiLb);
            Invoke(new DelegLabel(ModLabel), Convert.ToString(account.CharacterStats.ActionPoints.Base + account.CharacterStats.ActionPoints.Additionnal + account.CharacterStats.ActionPoints.ObjectsAndMountBonus), APLb);
            Invoke(new DelegLabel(ModLabel), Convert.ToString(account.CharacterStats.MovementPoints.Base + account.CharacterStats.MovementPoints.Additionnal + account.CharacterStats.MovementPoints.ObjectsAndMountBonus),MpLb);
            Invoke(new DelegLabel(ModLabel), Convert.ToString(account.CharacterStats.StatsPoints), AvailabPtLb);
            //GetBoost(10);
        }

        

        public void UpAuto()
        {
            if (VitaRb.Checked)
            {
                while (Convert.ToInt32(AvailabPtLb.Text) != 0)
                {
                    UpStat(11, 1);
                }
            }
            else
                account.Log(new ErrorTextInformation("L'auto-up ne gère pas les paliers et a donc été désactivé pour des raisons de sécurité (ban 2h sinon)."), 0);
            //else if (WisRb.Checked)
            //{
            //    while (Convert.ToInt32(AvailabPtLb.Text) % 3 == 0)
            //        UpStat(12, 3);
            //}
            //else if (StreRb.Checked)
            //{
            //    while (Convert.ToInt32(AvailabPtLb.Text) != 0)
            //        UpStat(10, 1);
            //}
            //else if (InteRb.Checked)
            //{
            //    while (Convert.ToInt32(AvailabPtLb.Text) != 0)
            //        UpStat(15, 1);
            //}
            //else if (LuckRb.Checked)
            //{
            //    while (Convert.ToInt32(AvailabPtLb.Text) != 0)
            //        UpStat(13, 1);
            //}
            //else if (AgiRb.Checked)
            //{
            //    while (Convert.ToInt32(AvailabPtLb.Text) != 0)
            //        UpStat(14, 1);
            //}
                

        }
        #endregion

        #region Interface Methods
        private void sadikButton1_Click(object sender, EventArgs e)
        {
            UpStat(11,1);
        }

        private void sadikButton2_Click(object sender, EventArgs e)
        {
            UpStat(12,3);
        }

        private void sadikButton3_Click(object sender, EventArgs e)
        {
            UpStat(10,1);
        }

        private void sadikButton4_Click(object sender, EventArgs e)
        {
            UpStat(15,1);
        }

        private void sadikButton5_Click(object sender, EventArgs e)
        {
            UpStat(13, 1);
        }

        private void sadikButton6_Click(object sender, EventArgs e)
        {
            UpStat(14, 1);
        }
        #endregion

        #region Private Methods
        private void UpStat(int statId, int boost)
        {
            StatsUpgradeRequestMessage msg = new StatsUpgradeRequestMessage(false, (byte)statId, (ushort)boost);
            account.SocketManager.Send(msg);          
        }

        private void ModLabel(string content, Label lab)
        {
            lab.Text = content;
        }

        public int GetBoost(int statId)
        {
            DataClass d = GameData.GetDataObject(D2oFileEnum.Breeds, account.CharacterBaseInformations.Breed);
            switch (statId)
            {
                case 10:
                    ArrayList o = (ArrayList)d.Fields["statsPointsForStrength"];
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
            }

            return 1;
        }

        #endregion

        
    }
}
