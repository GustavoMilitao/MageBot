using MageBot.Core.Account;
using MageBot.Core.Fight;
using MageBot.Core.Engine.Constants;
using Util.Util.Text.Log;
using MageBot.Core.Engine.Constants;
using MageBot.DataFiles.Data.D2o;
using MageBot.DataFiles.Data.D2p;
using MageBot.DataFiles.Data.I18n;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace CoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckMageBotDatas();
            Account conta = new Account("kaiodotapro", "cefet123");
            conta.Config.DebugMode = true;
            Thread t = new Thread(() => GetMessageLogFromAccountQueue(conta));
            t.Start();
            conta.Connect();
            conta.Config.FightParser = new FightParser(conta, @"C:\Users\Sara\AppData\Roaming\MageBot.IAs\IATest.txt", "IA Teste");
            conta.Fight = new BFight(conta, conta.Config.FightParser, conta.FightData);
            conta.Config.MinMonstersLevel = 0;
            conta.Config.MaxMonstersLevel = 1000;
            conta.Config.MinMonstersNumber = 0;
            conta.Config.MaxMonstersNumber = 8;
            //conta.Config.LockingSpectators = true;
            conta.Config.LockingFights = true;
            Thread.Sleep(20000);
            conta.Config.AutoRelaunchFight = true;
            if (!conta.Fight.SearchFight().Result)
                conta.Map.ChangeMap();
        }
    


    private static void GetMessageLogFromAccountQueue(Account conta)
    {
        Tuple<TextInformation, int> queueItem;
        TextInformation text;
        int verboseLevel;
        while (true)
        {
            Thread.Sleep(1);
            if (conta.InformationQueue.Count > 0)
            {
                queueItem = conta.InformationQueue.Dequeue();
                text = queueItem.Item1;
                verboseLevel = queueItem.Item2;

                text.Text = Translate.GetTranslation(text.Text);
                text.Text = "[" + DateTime.Now.ToLongTimeString() +
                    "] (" + text.Category + ") " + text.Text;
                if (text is DebugTextInformation)
                {
                    if (conta.Config.DebugMode)
                    {
                        Console.WriteLine(text.Text);
                    }
                }
                else
                {
                    Console.WriteLine(text.Text);
                }
            }
        }
    }

    private static void CheckMageBotDatas()
    {
        // Create the MageBot.needed folders
        string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string blueSheepPath = Path.Combine(applicationDataPath, "MageBot");
        if (!Directory.Exists(blueSheepPath))
            Directory.CreateDirectory(blueSheepPath);
        if (!Directory.Exists(Path.Combine(blueSheepPath, "Accounts")))
            Directory.CreateDirectory(Path.Combine(blueSheepPath, "Accounts")).Attributes = FileAttributes.Normal;
        if (!Directory.Exists(Path.Combine(blueSheepPath, "Groups")))
            Directory.CreateDirectory(Path.Combine(blueSheepPath, "Groups")).Attributes = FileAttributes.Normal;
        if (!Directory.Exists(Path.Combine(blueSheepPath, "Temp")))
            Directory.CreateDirectory(Path.Combine(blueSheepPath, "Temp")).Attributes = FileAttributes.Normal;
        if (!Directory.Exists(Path.Combine(blueSheepPath, "Paths")))
            Directory.CreateDirectory(Path.Combine(blueSheepPath, "Paths")).Attributes = FileAttributes.Normal;
        if (!Directory.Exists(Path.Combine(blueSheepPath, "IAs")))
            Directory.CreateDirectory(Path.Combine(blueSheepPath, "IAs")).Attributes = FileAttributes.Normal;
        if (!Directory.Exists(Path.Combine(blueSheepPath, "Logs")))
            Directory.CreateDirectory(Path.Combine(blueSheepPath, "Logs")).Attributes = FileAttributes.Normal;

        string bsConfPath = Path.Combine(blueSheepPath, "bs.conf");
        if (File.Exists(bsConfPath))
        {
            StreamReader sr = new StreamReader(bsConfPath);
            string path = sr.ReadLine();
        }


        FileInfo fileInfo = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MageBot.Logs.txt");
        fileInfo.Delete();
        using (fileInfo.Create())
        {
        }

        //fileInfo = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MageBot.Packets.txt");
        //fileInfo.Delete();
        //using (fileInfo.Create())
        //{
        //}


        I18NFileAccessor i18NFileAccessor = new I18NFileAccessor();

        if (File.Exists(@"C:\Program Files (x86)\Dofus2\app\data\i18n\i18n_fr.d2i"))
        {
            string path = @"C:\Program Files (x86)\Dofus2\app\data\i18n\i18n_fr.d2i";
            i18NFileAccessor.Init(path);
            I18N i18N = new I18N(i18NFileAccessor);
            GameData.Init(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
                + @"\Dofus2\app\data\common");
            MapsManager.Init(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
                + @"\Dofus2\app\content\maps");
        }
        else if (File.Exists(bsConfPath))
        {
            List<string> PaysList = new List<string>();
            PaysList.AddRange(new List<string>() { "fr", "en", "ja", "es", "de", "pt" });
            foreach (string pays in PaysList)
            {
                string combinedPath = Path.Combine(@"E:\Jogos\Dofus", "app", "data", "i18n", "i18n_" + pays + ".d2i");
                if (File.Exists(combinedPath))
                {
                    i18NFileAccessor.Init(combinedPath);
                    break;
                }
            }
            I18N i18N = new I18N(i18NFileAccessor);
            GameData.Init(Path.Combine(@"E:\Jogos\Dofus", "app", "data", "common"));
            MapsManager.Init(Path.Combine(@"E:\Jogos\Dofus", "app", "content", "maps"));
        }
        //else
        //{
        //    i18NFileAccessor.Init(Path.Combine(ActualMainForm.DofusPath, "app", "data", "i18n", "i18n_fr.d2i"));
        //    I18N i18N = new I18N(i18NFileAccessor);
        //    GameData.Init(@"D:\Dofus2\app\data\common");
        //    MapsManager.Init(@"D:\Dofus2\app\content\maps");
        //}
        IntelliSense.InitMonsters();
        IntelliSense.InitItems();
        IntelliSense.InitServers();
    }

}
}
