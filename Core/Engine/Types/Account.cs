﻿using System.Collections.Generic;
using BlueSheep.Core.Frame;
using BlueSheep.Engine.Treatment;
using BotForge.Core.Game.Fight;
using BotForgeAPI.Protocol.Messages;
using BotForgeAPI.Protocol.Enums;
using BotForge.Core.Game.Map.Actors;
using BotForgeAPI.Game.Map.Actors;

namespace Core.Engine.Types
{
    public class Account : BotForge.Core.Account.Account
    {
        #region Properties

        #region Internal code use
        public Queue<int> LastPacketID { get; set; }
        public int LastPacket { get; set; }
        public LatencyFrame LatencyFrame { get; set; }
        public ushort Sequence { get; set; }
        public Dictionary<int, DataBar> InfBars { get; set; }
        public Treatment Treatment { get; set; }
        public bool LockingSpectators { get; set; }
        public bool LockPerformed { get; set; }
        public bool StartFightWithItemSet { get; set; }
        public bool EndFightWithItemSet { get; set; }
        public byte PresetStartUpId { get; set; }
        public INpc TalkingNPC { get; set; }
        public string DofusPath { get; set; }
        #endregion

        #endregion

        public Account(string username, string password, string nickname = ""): base(username, password, nickname)
        {
            InitBars();
        }

        private void InitBars()
        {
            InfBars = new Dictionary<int, DataBar>
            {
                { 1, new DataBar() { Text = "Experience" } },
                { 2, new DataBar() { Text = "Life" } },
                { 3, new DataBar() { Text = "Pods" } },
                { 4, new DataBar() { Text = "Kamas" } },
                { 5, new DataBar() { Text = "Pos" } },
                { 7, new DataBar() { Text = "ParentForm" } },
                { 8, new DataBar() { Text = "Level" } },
                { 9, new DataBar() { Text = "Subscribe" } }
            };
        }

        public void ModifBar(int bar, int max, int value, string text)
        {
            if (InfBars.ContainsKey(bar))
            {
                InfBars[bar].Max = max;
                InfBars[bar].Value = value;
                InfBars[bar].Text = text;
            }

        }

        public void LockFightForSpectators()
        {
            Network.Send(new GameFightOptionToggleMessage((sbyte)FightOptionsEnum.FIGHT_OPTION_SET_SECRET));
        }

    }
}