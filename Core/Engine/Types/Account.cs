using System.Collections.Generic;
using BlueSheep.Core.Frame;
using BlueSheep.Engine.Treatment;

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
        #endregion

        #endregion

        public Account(string username, string password, string nickname = ""): base(username, password, nickname)
        {
            initBars();
        }

        private void initBars()
        {
            InfBars = new Dictionary<int, DataBar>();
            InfBars.Add(1, new DataBar() { Text = "Experience" });
            InfBars.Add(2, new DataBar() { Text = "Life" });
            InfBars.Add(3, new DataBar() { Text = "Pods" });
            InfBars.Add(4, new DataBar() { Text = "Kamas" });
            InfBars.Add(5, new DataBar() { Text = "Pos" });
            InfBars.Add(7, new DataBar() { Text = "ParentForm" });
            InfBars.Add(8, new DataBar() { Text = "Level" });
            InfBars.Add(9, new DataBar() { Text = "Subscribe" });
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

    }
}
