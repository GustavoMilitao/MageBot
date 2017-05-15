using BlueSheep.Common.Data;
using BlueSheep.Common.Data.D2o;
using BlueSheep.Data.D2p;
using BlueSheep.Engine.Treatment;
using BlueSheep.Util.Text.Log;
using BotForge.Core.Constants;
using BotForge.Core.Server;
using BotForgeAPI.Network;
using BotForgeAPI.Protocol.Messages;
using Core.Engine.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Account conta = new Account("kaiodotapro", "cefet123");
            CoreTest t = new CoreTest(conta);
            conta.Logger = new Logger();
            conta.IsFullSocket = true;
            conta.Settings.LocalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlueSheep");
            conta.LastPacketID = new Queue<int>();
            t.Test();
        }
    }
}
