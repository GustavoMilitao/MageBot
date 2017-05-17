using System;
using System.Windows.Forms;
using System.Collections;
using BlueSheep.Util.Text.Log;
using Core.Core;

namespace BlueSheep.Interface.UCs
{
    public partial class CaracUC : MetroFramework.Controls.MetroUserControl
    {
        #region Fields
        private AccountUC accUserControl { get; set; }
        #endregion

        private delegate void DelegLabel(string text, Label lab);

        #region Constructors
        public CaracUC(AccountUC Account)
        {
            InitializeComponent();
            accUserControl = Account;
            accUserControl.Account.Config.CharacterConfig = new Character(accUserControl.Account);
        }
        #endregion

        #region Public Methods
        public void Init()
        {
            Invoke(new DelegLabel(ModLabel), Convert.ToString(accUserControl.Account.CharacterStats.Vitality.Base + accUserControl.Account.CharacterStats.Vitality.Additionnal + accUserControl.Account.CharacterStats.Vitality.ObjectsAndMountBonus), VitaLb);
            Invoke(new DelegLabel(ModLabel), Convert.ToString(accUserControl.Account.CharacterStats.Wisdom.Base + accUserControl.Account.CharacterStats.Wisdom.Additionnal + accUserControl.Account.CharacterStats.Wisdom.ObjectsAndMountBonus), WisdomLb);
            Invoke(new DelegLabel(ModLabel), Convert.ToString(accUserControl.Account.CharacterStats.Strength.Base + accUserControl.Account.CharacterStats.Strength.Additionnal + accUserControl.Account.CharacterStats.Strength.ObjectsAndMountBonus), StreLb);
            Invoke(new DelegLabel(ModLabel), Convert.ToString(accUserControl.Account.CharacterStats.Intelligence.Base + accUserControl.Account.CharacterStats.Intelligence.Additionnal + accUserControl.Account.CharacterStats.Intelligence.ObjectsAndMountBonus), InteLb);
            Invoke(new DelegLabel(ModLabel), Convert.ToString(accUserControl.Account.CharacterStats.Chance.Base + accUserControl.Account.CharacterStats.Chance.Additionnal + accUserControl.Account.CharacterStats.Chance.ObjectsAndMountBonus), LuckLb);
            Invoke(new DelegLabel(ModLabel),  Convert.ToString(accUserControl.Account.CharacterStats.Agility.Base + accUserControl.Account.CharacterStats.Agility.Additionnal + accUserControl.Account.CharacterStats.Agility.ObjectsAndMountBonus), AgiLb);
            Invoke(new DelegLabel(ModLabel), Convert.ToString(accUserControl.Account.CharacterStats.ActionPoints.Base + accUserControl.Account.CharacterStats.ActionPoints.Additionnal + accUserControl.Account.CharacterStats.ActionPoints.ObjectsAndMountBonus), APLb);
            Invoke(new DelegLabel(ModLabel), Convert.ToString(accUserControl.Account.CharacterStats.MovementPoints.Base + accUserControl.Account.CharacterStats.MovementPoints.Additionnal + accUserControl.Account.CharacterStats.MovementPoints.ObjectsAndMountBonus),MpLb);
            Invoke(new DelegLabel(ModLabel), Convert.ToString(accUserControl.Account.CharacterStats.StatsPoints), AvailabPtLb);
        }

        public void UpAuto()
        {
            accUserControl.Account.Config.CharacterConfig.UpAuto();
        }

        public void DecreaseAvailablePoints(int n)
        {
            AvailabPtLb.Text = Convert.ToString(Convert.ToInt32(AvailabPtLb.Text) - n);
        }
        #endregion

        #region Interface Methods
        private void sadikButton1_Click(object sender, EventArgs e)
        {
            accUserControl.Account.Config.CharacterConfig.UpStat(Protocol.Enums.BoostableCharacteristicEnum.BOOSTABLE_CHARAC_VITALITY);
        }

        private void sadikButton2_Click(object sender, EventArgs e)
        {
            accUserControl.Account.Config.CharacterConfig.UpStat(Protocol.Enums.BoostableCharacteristicEnum.BOOSTABLE_CHARAC_WISDOM);
        }

        private void sadikButton3_Click(object sender, EventArgs e)
        {
            accUserControl.Account.Config.CharacterConfig.UpStat(Protocol.Enums.BoostableCharacteristicEnum.BOOSTABLE_CHARAC_STRENGTH);
        }

        private void sadikButton4_Click(object sender, EventArgs e)
        {
            accUserControl.Account.Config.CharacterConfig.UpStat(Protocol.Enums.BoostableCharacteristicEnum.BOOSTABLE_CHARAC_INTELLIGENCE);
        }

        private void sadikButton5_Click(object sender, EventArgs e)
        {
            accUserControl.Account.Config.CharacterConfig.UpStat(Protocol.Enums.BoostableCharacteristicEnum.BOOSTABLE_CHARAC_CHANCE);
        }

        private void sadikButton6_Click(object sender, EventArgs e)
        {
            accUserControl.Account.Config.CharacterConfig.UpStat(Protocol.Enums.BoostableCharacteristicEnum.BOOSTABLE_CHARAC_AGILITY);
        }
        #endregion

        #region Private Methods

        private void ModLabel(string content, Label lab)
        {
            lab.Text = content;
        }

        #endregion

        
    }
}
