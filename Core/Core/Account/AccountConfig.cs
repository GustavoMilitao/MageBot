using MageBot.Core.Char;
using MageBot.Core.Fight;
using MageBot.Core.Inventory;
using MageBot.Core.Misc;
using MageBot.Core.Path;
using MageBot.Protocol.Enums;
using MageBot.Util.Enums.Internal;
using System;
using System.Collections.Generic;

namespace MageBot.Core.Account
{
    [Serializable()]
    public class AccountConfig
    {
        #region General
        public int VerboseLevel;
        public bool IsMaster { get; set; }
        public bool IsSlave { get; set; }
        public bool DebugMode { get; set; }
        public bool IsMITM { get; set; }
        public bool IsSocket { get; set; }
        public bool Enabled { get; set; }
        public bool Restored { get; set; }
        public bool Begin { get; set; }
        public int BotSpeed { get; set; }
        public bool LogConsoleToText { get; set; }
        #endregion

        #region flood
        public bool AddRandomingSmiley { get; set; }
        public bool AddRandomingNumber { get; set; }
        public bool FloodSaveInMemory { get; set; }
        public bool FloodInCommerceChannel { get; set; }
        public bool FloodInRecruitmentChannel { get; set; }
        public bool FloodInGeneralChannel { get; set; }
        public bool FloodInPrivateChannel { get; set; }
        public int FloodInterval { get; set; }
        public string FloodContent { get; set; }
        #endregion

        #region Path
        public bool RelaunchPath { get; set; }
        public string PreLoadedPath { get; set; }
        public string PreLoadedPathName { get; set; }
        #endregion

        #region Inventory
        public int MaxPodsPercent { get; set; }
        public List<Item> ItemsToAutoDelete { get; set; }
        public List<Item> ItemsToStayOnCharacter { get; set; }
        public List<Item> ItemsToGetFromBank { get; set; }
        public bool ListeningToExchange { get; set; }
        public int AutoDeletionTime { get; set; }
        #endregion

        #region Fight
        public int RegenChoice { get; set; }
        public List<Item> RegenItems { get; set; }
        public int MinMonstersNumber { get; set; }
        public int MaxMonstersNumber { get; set; }
        public int MinMonstersLevel { get; set; }
        public int MaxMonstersLevel { get; set; }
        public bool LockingFights { get; set; }
        public bool LockingSpectators { get; set; }
        public bool LockingForGroupOnly { get; set; }
        public bool AskForHelp { get; set; }
        public bool StartFightWithItemSet { get; set; }
        public byte PresetStartUpId { get; set; }
        public bool EndFightWithItemSet { get; set; }
        public sbyte PresetEndUpId { get; set; }
        public List<MonsterRestrictions> MonsterRestrictions { get; set; }
        public bool LockPerformed { get; set; }
        public string PreLoadedAI { get; set; }
        public string PreLoadedAIName { get; set; }
        #endregion

        #region House
        public ulong MaxPriceHouse { get; set; }
        public string HouseSearcherLogPath { get; set; }
        public bool HouseSearcherEnabled { get; set; }
        public string SentenceToSay { get; set; }
        public bool WaitingForTheSale { get; set; }
        #endregion

        #region Char
        public BoostableCharacteristicEnum? CaracToAutoUp { get; set; }
        public bool HeroicModeOn { get; set; }
        public bool AgroConditionsSet { get; set; }
        public bool RunConditionsSet { get; set; }
        public long MinLevelRun { get; set; }
        public long MaxLevelRun { get; set; }
        public List<string> AlliancesNameRun { get; set; }
        public List<string> AlliancesNameAgro { get; set; }
        public bool DisconnectWhenRun { get; set; }
        public bool UseItemWhenRun { get; set; }
        public PotionEnum? ItemToUseWhenRun { get; set; }
        public long MinLevelAgro { get; set; }
        public long MaxLevelAgro { get; set; }
        #endregion

        public AccountConfig()
        {
            ItemsToAutoDelete = new List<Item>();
            ItemsToGetFromBank = new List<Item>();
            ItemsToStayOnCharacter = new List<Item>();
            MonsterRestrictions = new List<MonsterRestrictions>();
            Enabled = true;
        }
    }
}
