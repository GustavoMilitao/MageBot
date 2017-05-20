using MageBot.DataFiles.Data.D2o;
using MageBot.Util.IO;
using MageBot.Protocol.Messages.Game.Achievement;
using MageBot.Protocol.Messages.Game.Character.Choice;
using MageBot.Protocol.Messages.Game.Character.Stats;
using MageBot.Protocol.Messages.Game.Character.Status;
using MageBot.Protocol.Messages.Game.Context.Roleplay.Stats;
using MageBot.Protocol.Messages.Game.Inventory.Spells;
using MageBot.Protocol.Types.Game.Character.Status;
using MageBot.Protocol.Types.Game.Data.Items;
using MageBot.Protocol.Messages;
using Util.Util.Text.Log;
using MageBot.Util.Enums.Char;
using MageBot.Util.Enums.EnumHelper;
using System;
using MageBot.Protocol.Enums;
using MageBot.Core.Fight;

namespace MageBot.Core.Engine.Handlers.Character
{
    class CharacterHandler
    {
        #region Public methods
        [MessageHandler(typeof(CharactersListMessage))]
        public static void CharactersListMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            CharactersListMessage charactersListMessage = (CharactersListMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                charactersListMessage.Deserialize(reader);
            }

            account.CharacterBaseInformations = charactersListMessage.Characters[0];

            if (!account.Config.IsMITM)
            {
                CharacterSelectionMessage characterSelectionMessage = new CharacterSelectionMessage((ulong)account.CharacterBaseInformations.ObjectID);
                account.SocketManager.Send(characterSelectionMessage);
            }

        }

        [MessageHandler(typeof(CharacterSelectedSuccessMessage))]
        public static void CharacterSelectedSuccessMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            CharacterSelectedSuccessMessage characterSelectedSuccessMessage = (CharacterSelectedSuccessMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                characterSelectedSuccessMessage.Deserialize(reader);
            }

            account.CharacterBaseInformations = characterSelectedSuccessMessage.Infos;

            account.Log(new BotTextInformation(account.CharacterBaseInformations.Name + " de niveau " + account.CharacterBaseInformations.Level + " est connecté."), 1);
            account.Log(new BotTextInformation("Breed: " + ((BreedEnum)account.CharacterBaseInformations.Breed).Description() + " Sex: " + ((Sex)Convert.ToInt32(account.CharacterBaseInformations.Sex)).Description()), 1);
            account.ModifBar(7, 0, 0, account.AccountName + " - " + account.CharacterBaseInformations.Name);
            account.ModifBar(8, 0, 0, Convert.ToString(account.CharacterBaseInformations.Level));
        }

        [MessageHandler(typeof(CharacterSelectedErrorMessage))]
        public static void CharacterSelectedErrorMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            account.Log(new ConnectionTextInformation("Erreur lors de la sélection du personnage."), 0);
            account.TryReconnect(30);
        }

        [MessageHandler(typeof(CharacterStatsListMessage))]
        public static void CharacterStatsListMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            CharacterStatsListMessage msg = (CharacterStatsListMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            if (!account.Config.Restored)
                account.ConfigRecover.RecoverConfig();
            account.CharacterStats = msg.Stats;
            uint percent = (msg.Stats.LifePoints / msg.Stats.MaxLifePoints) * 100;
            string text = msg.Stats.LifePoints + "/" + msg.Stats.MaxLifePoints + "(" + percent + "%)";
            account.ModifBar(2, (int)msg.Stats.MaxLifePoints, (int)msg.Stats.LifePoints, "Life");
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
            account.UpdateCharacterStats();
        }

        [MessageHandler(typeof(SpellListMessage))]
        public static void SpellListMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            SpellListMessage msg = (SpellListMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }

            account.Spells.Clear();
            foreach (SpellItem spell in msg.Spells)
                account.Spells.Add(new BSpell(spell.SpellId, spell.SpellLevel));
        }

        [MessageHandler(typeof(CharacterSelectedForceMessage))]
        public static void CharacterSelectedForceMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
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
        public static void CharacterLevelUpMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            CharacterLevelUpMessage msg = (CharacterLevelUpMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.ModifBar(8, 0, 0, Convert.ToString(msg.NewLevel));
            account.Log(new BotTextInformation("Level up ! New level : " + Convert.ToString(msg.NewLevel)), 3);
            account.Character.UpAuto();
        }

        [MessageHandler(typeof(AchievementFinishedMessage))]
        public static void AchievementFinishedTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            AchievementFinishedMessage msg = (AchievementFinishedMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            DataClass d = GameData.GetDataObject(D2oFileEnum.Achievements, msg.ObjectId);
            account.Log(new ActionTextInformation("Succès débloqué : " + MageBot.DataFiles.Data.I18n.I18N.GetText((int)d.Fields["nameId"])), 3);
            AchievementRewardRequestMessage nmsg = new AchievementRewardRequestMessage(-1);
            account.SocketManager.Send(nmsg);

        }

        [MessageHandler(typeof(CharacterExperienceGainMessage))]
        public static void CharacterExperienceGainMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
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
                catch (Exception)
                {

                }
                account.ModifBar(1, (int)account.CharacterStats.ExperienceNextLevelFloor - (int)account.CharacterStats.ExperienceLevelFloor, (int)account.CharacterStats.Experience - (int)account.CharacterStats.ExperienceLevelFloor, "Experience");
            }
        }

        [MessageHandler(typeof(StatsUpgradeResultMessage))]
        public static void StatsUpgradeResultMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            StatsUpgradeResultMessage msg = (StatsUpgradeResultMessage)message;
            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.UpdateCharacterStats();
        }

        [MessageHandler(typeof(PlayerStatusUpdateMessage))]
        public static void PlayerStatusUpdateMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
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
                    case 40:
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
