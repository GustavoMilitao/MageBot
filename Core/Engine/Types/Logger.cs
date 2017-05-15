using BotForgeAPI.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Engine.Types
{
    public class Logger : ILogger
    {
        public event EventHandler<CommandEventArgs> CommandTyped;

        public void Error(Exception ex)
        {
            throw ex;
        }

        public void Log(string text, LogEnum loggerEnum)
        {
            Console.WriteLine(text);
        }
    }
}
