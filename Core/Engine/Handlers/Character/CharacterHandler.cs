using BlueSheep.Common.Data;
using BlueSheep.Common.Data.D2o;
using BlueSheep.Util.IO;
using BlueSheep.Util.Text.Log;
using BlueSheep.Util.Enums.EnumHelper;
using System;
using BlueSheep.Core.Fight;
using BotForgeAPI.Protocol.Messages;
using BotForgeAPI.Network.Messages;
using BotForgeAPI.Protocol.Enums;
using BotForgeAPI.Protocol.Types;
using System.Linq;
using BotForge.Core.Game.Characteristics;
using BotForge.Core.Game.Map.Actors;
using Core.Engine.Types;

namespace BlueSheep.Engine.Handlers.Character
{
    class CharacterHandler
    {
        delegate void AddMemberCallBack(string MemberName);

        #region Public methods
        [MessageHandler(typeof(CharactersListMessage))]
        public static void CharactersListMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            //CharactersListMessage charactersListMessage = (CharactersListMessage)message;

            ////packetDatas = packetDatas.ToList().SkipWhile(a => a == 0).ToArray();

            //using (BigEndianReader reader = new BigEndianReader(packetDatas))
            //{
            //    charactersListMessage.Deserialize(reader);
            //}
            //foreach(CharacterBaseInformations info in charactersListMessage.Characters)
            //{
            //    var a = account.Game.Map.Data.Players.Where(ch => ch.Id == info.ObjectId).FirstOrDefault();
            //    ((BotForge.Core.Game.Map.Actors.PlayedCharacter)a).Look
            //}

            ////MainForm.ActualMainForm.ActualizeAccountInformations();

            //if (!account.Config.IsMITM)
            //{
            //    CharacterSelectionMessage characterSelectionMessage = new CharacterSelectionMessage((ulong)account.CharacterBaseInformations.ObjectID);
            //    account.SocketManager.Send(characterSelectionMessage);
            //}
            //TODO Militão 2.0: Descubrir qual campo preencher nesse ponto

        }

        [MessageHandler(typeof(CharacterSelectedSuccessMessage))]
        public static void CharacterSelectedSuccessMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            //CharacterSelectedSuccessMessage characterSelectedSuccessMessage = (CharacterSelectedSuccessMessage)message;

            //using (BigEndianReader reader = new BigEndianReader(packetDatas))
            //{
            //    characterSelectedSuccessMessage.Deserialize(reader);
            //}

            //account.CharacterBaseInformations = characterSelectedSuccessMessage.Infos;

            //account.Log(new BotTextInformation(account.CharacterBaseInformations.Name + " de niveau " + account.CharacterBaseInformations.Level + " est connecté."), 1);
            ////account.Log(new BotTextInformation("Breed: " + ((BreedEnum)account.CharacterBaseInformations.Breed).Description() + " Sex: " + ((Sex)Convert.ToInt32(account.CharacterBaseInformations.Sex)).Description()), 1);
            //account.ModifBar(7, 0, 0, account.AccountName + " - " + account.CharacterBaseInformations.Name);
            //account.ModifBar(8, 0, 0, Convert.ToString(account.CharacterBaseInformations.Level));

            ////MainForm.ActualMainForm.ActualizeAccountInformations();
            //TODO Militão 2.0: Descubrir qual campo preencher nesse ponto
        }

        [MessageHandler(typeof(CharacterSelectedErrorMessage))]
        public static void CharacterSelectedErrorMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            account.Logger.Log("Erreur lors de la sélection du personnage.", BotForgeAPI.Logger.LogEnum.Connection);
            //account.TryReconnect(30);
        }

        [MessageHandler(typeof(CharacterStatsListMessage))]
        public static void CharacterStatsListMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            CharacterStatsListMessage msg = (CharacterStatsListMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            (account.Game.Character.Stats as PlayerStats).Update(msg.Stats);
            PopulateBars(account, msg);
        }

        [MessageHandler(typeof(SpellListMessage))]
        public static void SpellListMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            SpellListMessage msg = (SpellListMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            (account.Game.Character.Spells as SpellsBook).Update(msg);
        }

        [MessageHandler(typeof(CharacterSelectedForceMessage))]
        public static void CharacterSelectedForceMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            CharacterSelectedForceMessage msg = (CharacterSelectedForceMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            CharacterSelectedForceReadyMessage nmsg = new CharacterSelectedForceReadyMessage();
            account.Network.Send(nmsg);
        }

        [MessageHandler(typeof(CharacterLevelUpMessage))]
        public static void CharacterLevelUpMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            CharacterLevelUpMessage msg = (CharacterLevelUpMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.ModifBar(8, 0, 0, Convert.ToString(msg.NewLevel));
            account.Logger.Log("Level up ! New level : " + Convert.ToString(msg.NewLevel), BotForgeAPI.Logger.LogEnum.Bot);
            if (account.Settings.IsUppingStat)
                account.Game.Character.Upgrade(account.Settings.AutoUpStat);
        }

        [MessageHandler(typeof(AchievementFinishedMessage))]
        public static void AchievementFinishedTreatment(Message message, byte[] packetDatas, Account account)
        {
            AchievementFinishedMessage msg = (AchievementFinishedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            DataClass d = GameData.GetDataObject(D2oFileEnum.Achievements, msg.ObjectId);
            account.Logger.Log("Success unlocked : " + I18N.GetText((int)d.Fields["nameId"]), BotForgeAPI.Logger.LogEnum.TextInformationMessage);
            AchievementRewardRequestMessage nmsg = new AchievementRewardRequestMessage(-1);
            account.Network.Send(nmsg);
        }

        [MessageHandler(typeof(CharacterExperienceGainMessage))]
        public static void CharacterExperienceGainMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            CharacterExperienceGainMessage msg = (CharacterExperienceGainMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            (account.Game.Character.Stats as PlayerStats).Update(msg);
            //TODO Militão 2.0: Update Charts
        }

        [MessageHandler(typeof(StatsUpgradeResultMessage))]
        public static void StatsUpgradeResultMessageTreatment(Message message, byte[] packetDatas, Account account)
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
        public static void PlayerStatusUpdateMessageTreatment(Message message, byte[] packetDatas, Account account)
        {
            PlayerStatusUpdateMessage msg = (PlayerStatusUpdateMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }

            if (msg.PlayerId == account.Game.Character.Id)
            {
                switch (msg.Status.StatusId)
                {
                    case 10:
                        account.Logger.Log("Available status enabled",BotForgeAPI.Logger.LogEnum.TextInformationMessage);
                        break;
                    case 20:
                        account.Logger.Log("Absent status enabled.", BotForgeAPI.Logger.LogEnum.TextInformationMessage);
                        PlayerStatusUpdateRequestMessage nmsg = new PlayerStatusUpdateRequestMessage(new PlayerStatus(10));
                        account.Network.Send(nmsg);
                        break;
                    case 40:
                        account.Logger.Log("Solo status enabled.", BotForgeAPI.Logger.LogEnum.TextInformationMessage);
                        break;
                    case 30:
                        account.Logger.Log("Private status activated.", BotForgeAPI.Logger.LogEnum.TextInformationMessage);
                        break;

                }
            }
        }

        #endregion

        #region Private Methods
        private static void PopulateBars(Account account, CharacterStatsListMessage msg)
        {
            uint percent = (msg.Stats.LifePoints / msg.Stats.MaxLifePoints) * 100;
            string text = msg.Stats.LifePoints + "/" + msg.Stats.MaxLifePoints + "(" + percent + "%)";
            account.ModifBar(2, (int)msg.Stats.MaxLifePoints, (int)msg.Stats.LifePoints, "Vitalité");
            double i = msg.Stats.Experience - msg.Stats.ExperienceLevelFloor;
            double j = msg.Stats.ExperienceNextLevelFloor - msg.Stats.ExperienceLevelFloor;
            try
            {
                int xppercent = (int)(Math.Round(i / j, 2) * 100);
            }
            catch (Exception)
            {
            }
            account.ModifBar(1, (int)msg.Stats.ExperienceNextLevelFloor - (int)msg.Stats.ExperienceLevelFloor, (int)msg.Stats.Experience - (int)msg.Stats.ExperienceLevelFloor, "Experience");
            account.ModifBar(4, 0, 0, msg.Stats.Kamas.ToString());
        }
        #endregion
    }
}
