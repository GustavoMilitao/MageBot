using MageBot.Core.Char;
using MageBot.Core.Fight;
using MageBot.Core.Misc;
using MageBot.Core.Path;
using System;
using System.Collections.Generic;

namespace MageBot.Core.Account
{
    [Serializable()]
    public class AccountConfig
    {
        #region General
        public Account Account { get; set; }
        public int VerboseLevel;
        public bool IsMaster { get; set; }
        public bool IsSlave { get; set; }
        public bool DebugMode { get; set; }
        public bool IsMITM { get; set; }
        public bool IsSocket { get; set; }
        public bool Enabled { get; set; }
        public ConfigManager ConfigRecover { get; set; }
        public bool Begin { get; set; }
        public int BotSpeed { get; set; }
        #endregion

        #region pet
        public DateTime NextMeal { get; set; }
        public DateTime NextMealP { get; set; } //TODO Militão: What is this?
        #endregion

        #region flood
        public Flood Flood { get; set; }
        #endregion

        #region Path
        public PathManager Path { get; set; }
        public bool RelaunchPath { get; set; }
        #endregion

        #region Inventory
        public int MaxPodsPercent { get; set; }
        #endregion

        #region Fight
        public Regen.Regen RegenConfig { get; set; }
        public int MinMonstersNumber { get; set; }
        public int MaxMonstersNumber { get; set; }
        public int MinMonstersLevel { get; set; }
        public int MaxMonstersLevel { get; set; }
        public bool LockingFights { get; set; }
        public bool LockingSpectators { get; set; }
        public bool LockingForGroupOnly { get; set; }
        public List<MonsterRestrictions> MonsterRestrictions { get; set; }
        public FightParser FightParser { get; set; }
        public bool AutoRelaunchFight { get; set; }
        public bool LockPerformed { get; set; }
        #endregion

        #region House
        public ulong MaxPriceHouse { get; set; }
        public string HouseSearcherLogPath { get; set; }
        #endregion

        #region Char
        public Character CharacterConfig { get; set; }
        public Heroic.Heroic HeroicConfig { get; set; }
        #endregion

        public AccountConfig(Account account)
        {
            Account = account;
            ConfigRecover = new ConfigManager(Account);
            Flood = new Flood(Account);
            RegenConfig = new Regen.Regen(Account);
            CharacterConfig = new Character(Account);
            HeroicConfig = new Heroic.Heroic(Account);
            MonsterRestrictions = new List<MonsterRestrictions>();
        }

        public AccountConfig()
        { }

    }
}
