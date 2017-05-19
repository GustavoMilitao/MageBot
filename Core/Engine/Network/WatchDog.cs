using MageBot.Core.Account;
using MageBot.Util.Enums.Internal;
using Util.Util.Text.Log;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MageBot.Core.Engine.Network
{
    public class WatchDog
    {
        #region Fields
        private Account.Account m_Account;
        private DateTime PathAction;
        private Thread m_PathDog;
        #endregion

        #region Constructors
        public WatchDog(Account.Account account)
        {
            m_Account = account;
        }
        #endregion

        #region Public Methods
        public void StartPathDog()
        {
            m_PathDog = new Thread(new ThreadStart(PathDog));
            m_PathDog.Start();
        }

        public void StopPathDog()
        {
            if (m_PathDog != null)
                m_PathDog.Abort();
        }

        public void Update()
        {
            PathAction = DateTime.Now;
        }
        #endregion

        #region Private Methods
        private async void PathDog()
        {
            DateTime now = PathAction;
            int endwait = Environment.TickCount + 10000;
            await Task.Delay(endwait);
            DateTime after = PathAction;
            if (DateTime.Compare(now, after) == 0 && CheckState())
            {
                m_Account.Log(new DebugTextInformation("[WatchDog] Relaunch path"), 0);
                m_Account.Path.ParsePath();
                StartPathDog();
            }
            else
            {
                m_Account.Log(new DebugTextInformation("[WatchDog] Nothing to do."), 0);
                StartPathDog();
            }
        }

        private bool CheckState()
        {
            return (m_Account.State == Status.None ||
                m_Account.State == Status.Moving ||
                m_Account.State == Status.Busy);
        }
        #endregion
    }
}
