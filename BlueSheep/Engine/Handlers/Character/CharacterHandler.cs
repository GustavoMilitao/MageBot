using BlueSheep.Common.Data;
using BlueSheep.Common.Data.D2o;
using BlueSheep.Common.IO;
using BlueSheep.Common.Protocol.Messages.Game.Achievement;
using BlueSheep.Common.Protocol.Messages.Game.Character.Choice;
using BlueSheep.Common.Protocol.Messages.Game.Character.Stats;
using BlueSheep.Common.Protocol.Messages.Game.Character.Status;
using BlueSheep.Common.Protocol.Messages.Game.Context.Roleplay.Stats;
using BlueSheep.Common.Protocol.Messages.Game.Inventory.Spells;
using BlueSheep.Common.Protocol.Types.Game.Character.Status;
using BlueSheep.Common.Protocol.Types.Game.Data.Items;
using BlueSheep.Common.Types;
using BlueSheep.Common;
using BlueSheep.Interface;
using BlueSheep.Interface.Text;
using BlueSheep.Util.Enums.Char;
using BlueSheep.Util.Enums.EnumHelper;
using System;
using BlueSheep.Common.Enums;

namespace BlueSheep.Engine.Handlers.Character
{
    class CharacterHandler
    {
        delegate void AddMemberCallBack(string MemberName);

        #region Public methods
        [MessageHandler(typeof(CharactersListMessage))]
        public static void CharactersListMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            CharactersListMessage charactersListMessage = (CharactersListMessage)message;

            //packetDatas = packetDatas.ToList().SkipWhile(a => a == 0).ToArray();

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                charactersListMessage.Deserialize(reader);
            }

            account.CharacterBaseInformations = charactersListMessage.Characters[0];
        
            //MainForm.ActualMainForm.ActualizeAccountInformations();

            if (!account.IsMITM)
            {
                CharacterSelectionMessage characterSelectionMessage = new CharacterSelectionMessage((ulong)account.CharacterBaseInformations.ObjectID);
                account.SocketManager.Send(characterSelectionMessage);
            }
            
        }

        [MessageHandler(typeof(CharacterSelectedSuccessMessage))]
        public static void CharacterSelectedSuccessMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            CharacterSelectedSuccessMessage characterSelectedSuccessMessage = (CharacterSelectedSuccessMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                characterSelectedSuccessMessage.Deserialize(reader);
            }

            account.CharacterBaseInformations = characterSelectedSuccessMessage.Infos;

            account.Log(new BotTextInformation(account.CharacterBaseInformations.Name + " de niveau "+ account.CharacterBaseInformations.Level + " est connecté."),1);
            account.Log(new BotTextInformation("Breed: "+ ((BreedEnum)account.CharacterBaseInformations.Breed).Description() + " Sex: " + ((Sex)Convert.ToInt32(account.CharacterBaseInformations.Sex)).Description()), 1);
            account.ModifBar(7,0,0, account.AccountName + " - " + account.CharacterBaseInformations.Name);
            account.ModifBar(8, 0, 0, Convert.ToString(account.CharacterBaseInformations.Level));
            
            //MainForm.ActualMainForm.ActualizeAccountInformations();
        }

        [MessageHandler(typeof(CharacterSelectedErrorMessage))]
        public static void CharacterSelectedErrorMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            account.Log(new ConnectionTextInformation("Erreur lors de la sélection du personnage."),0);
            account.TryReconnect(30);
        }

        [MessageHandler(typeof(CharacterStatsListMessage))]
        public static void CharacterStatsListMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            CharacterStatsListMessage msg = (CharacterStatsListMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (!account.ConfigManager.Restored)
                account.ConfigManager.RecoverConfig();
            account.CharacterStats = msg.Stats;
            account.CaracUC.Init();
            if (account.MyGroup != null)
            {
                if (((GroupForm)account.ParentForm).InvokeRequired)
                {
                    AddMemberCallBack d = new AddMemberCallBack(((GroupForm)account.ParentForm).AddMember);
                    ((GroupForm)account.ParentForm).Invoke(d, account.CharacterBaseInformations.Name);
                }
            }
            //if (account.MyGroup != null)
            //{
            //    ((GroupForm)account.ParentForm).AddMember(account.CharacterBaseInformations.Name);
            //}
            uint percent = (msg.Stats.LifePoints / msg.Stats.MaxLifePoints) * 100;
            string text = msg.Stats.LifePoints + "/" + msg.Stats.MaxLifePoints + "(" + percent + "%)";
            account.ModifBar(2, (int)msg.Stats.MaxLifePoints, (int)msg.Stats.LifePoints, "Vitalité");
            double i = msg.Stats.Experience - msg.Stats.ExperienceLevelFloor;
            double j = msg.Stats.ExperienceNextLevelFloor - msg.Stats.ExperienceLevelFloor;
            try
            {
                int xppercent = (int)(Math.Round(i / j,2) * 100);
            }
            catch (Exception ex)
            {

            }
            account.ModifBar(1, (int)msg.Stats.ExperienceNextLevelFloor - (int)msg.Stats.ExperienceLevelFloor, (int)msg.Stats.Experience - (int)msg.Stats.ExperienceLevelFloor, "Experience");
            account.ModifBar(4, 0, 0, msg.Stats.Kamas.ToString());
        }
        
        [MessageHandler(typeof(SpellListMessage))]
        public static void SpellListMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            SpellListMessage msg = (SpellListMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }

            account.Spells.Clear();
            foreach (SpellItem spell in msg.Spells)
                account.Spells.Add(new Spell(spell.SpellId, spell.SpellLevel));
        }

        [MessageHandler(typeof(CharacterSelectedForceMessage))]
        public static void CharacterSelectedForceMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            CharacterSelectedForceMessage msg = (CharacterSelectedForceMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
                CharacterSelectedForceReadyMessage nmsg = new CharacterSelectedForceReadyMessage();
                account.SocketManager.Send(nmsg);
            
        }

        [MessageHandler(typeof(CharacterLevelUpMessage))]
        public static void CharacterLevelUpMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            CharacterLevelUpMessage msg = (CharacterLevelUpMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.ModifBar(8, 0, 0, Convert.ToString(msg.NewLevel));
            account.Log(new BotTextInformation("Level up ! New level : " + Convert.ToString(msg.NewLevel)), 3);
            account.CaracUC.UpAuto();
        }

        [MessageHandler(typeof(AchievementFinishedMessage))]
        public static void AchievementFinishedTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            AchievementFinishedMessage msg = (AchievementFinishedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            DataClass d = GameData.GetDataObject(D2oFileEnum.Achievements, (int)msg.ObjectId);
            account.Log(new ActionTextInformation("Succès débloqué : " + I18N.GetText((int)d.Fields["nameId"])),3);
                AchievementRewardRequestMessage nmsg = new AchievementRewardRequestMessage(-1);
                account.SocketManager.Send(nmsg);
            
        }

        [MessageHandler(typeof(CharacterExperienceGainMessage))]
        public static void CharacterExperienceGainMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            CharacterExperienceGainMessage msg = (CharacterExperienceGainMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.Log(new ActionTextInformation("Experience gagnée : + " + msg.ExperienceCharacter + " points d'expérience"), 4);
            if (account.CharacterStats != null)
            {
                account.CharacterStats.Experience += msg.ExperienceCharacter;

                double i = account.CharacterStats.Experience - account.CharacterStats.ExperienceLevelFloor;
                double j = account.CharacterStats.ExperienceNextLevelFloor - account.CharacterStats.ExperienceLevelFloor;
                try
                {
                    int xppercent = (int)((i / j) * 100);
                }
                catch (Exception ex)
                {

                }
                account.ModifBar(1, (int)account.CharacterStats.ExperienceNextLevelFloor - (int)account.CharacterStats.ExperienceLevelFloor, (int)account.CharacterStats.Experience - (int)account.CharacterStats.ExperienceLevelFloor, "Experience");
                if (account.Fight != null)
                {
                    account.FightData.xpWon[DateTime.Today] += (int)msg.ExperienceCharacter;
                }
            }
        }

        [MessageHandler(typeof(StatsUpgradeResultMessage))]
        public static void StatsUpgradeResultMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            StatsUpgradeResultMessage msg = (StatsUpgradeResultMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            //if (msg.result == 1)
            //{
            //    //account.CaracUC.DecreaseAvailablePoints(msg.nbCharacBoost);
            //    account.Log(new BotTextInformation("Caractéristique augmentée."),0);
            //}
            //else
            //    account.Log(new ErrorTextInformation("Echec de l'up de caractéristique."), 0);
                
        }

        [MessageHandler(typeof(PlayerStatusUpdateMessage))]
        public static void PlayerStatusUpdateMessageTreatment(Message message, byte[] packetDatas, AccountUC account)
        {
            PlayerStatusUpdateMessage msg = (PlayerStatusUpdateMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (msg.PlayerId == account.CharacterBaseInformations.ObjectID)
            {
                switch (msg.Status.StatusId)
                {
                    case 10:
                        account.Log(new ActionTextInformation("Statut disponible activé."), 3);
                        break;
                    case 20:
                        account.Log(new ActionTextInformation("Statut absent activé."), 3);
                        PlayerStatusUpdateRequestMessage nmsg = new PlayerStatusUpdateRequestMessage(new PlayerStatus(10));
                        account.SocketManager.Send(nmsg);
                        break;
                    case  40:
                        account.Log(new ActionTextInformation("Statut solo activé."), 3);
                        break;
                    case 30:
                        account.Log(new ActionTextInformation("Statut privé activé."), 3);
                        break;

                }
            }
        }
        
        #endregion
    }
}
