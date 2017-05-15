using BlueSheep.Common.Data;
using BlueSheep.Common.Data.D2o;
using BlueSheep.Data.D2p;
using BlueSheep.Engine.Constants;
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
using System.Threading.Tasks;

namespace CoreTest
{
    public class CoreTest
    {
        Account Conta { get; set; }

        public CoreTest(Account accountInformations)
        {
            Conta = accountInformations;
        }

        public void Test()
        {
            CheckBlueSheepDatas();
            Connection c = new Connection(Conta, BotForge.Core.Constants.GameConstants.DServerIp, BotForge.Core.Constants.GameConstants.DServerPort);
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
        }

        private void CheckBlueSheepDatas()
        {
            // Create the BlueSheep needed folders
            string applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string localPath = Conta.Settings.LocalPath;/*Path.Combine(applicationDataPath, "BlueSheep");*/
            if (!Directory.Exists(localPath))
                Directory.CreateDirectory(localPath);
            if (!Directory.Exists(Path.Combine(localPath, "Accounts")))
                Directory.CreateDirectory(Path.Combine(localPath, "Accounts")).Attributes = FileAttributes.Normal;
            if (!Directory.Exists(Path.Combine(localPath, "Groups")))
                Directory.CreateDirectory(Path.Combine(localPath, "Groups")).Attributes = FileAttributes.Normal;
            if (!Directory.Exists(Path.Combine(localPath, "Temp")))
                Directory.CreateDirectory(Path.Combine(localPath, "Temp")).Attributes = FileAttributes.Normal;
            if (!Directory.Exists(Path.Combine(localPath, "Paths")))
                Directory.CreateDirectory(Path.Combine(localPath, "Paths")).Attributes = FileAttributes.Normal;
            if (!Directory.Exists(Path.Combine(localPath, "IAs")))
                Directory.CreateDirectory(Path.Combine(localPath, "IAs")).Attributes = FileAttributes.Normal;
            if (!Directory.Exists(Path.Combine(localPath, "Logs")))
                Directory.CreateDirectory(Path.Combine(localPath, "Logs")).Attributes = FileAttributes.Normal;

            string bsConfPath = Path.Combine(localPath, "bs.conf");
            if (File.Exists(bsConfPath))
            {
                StreamReader sr = new StreamReader(bsConfPath);
                string path = sr.ReadLine();
                if (Directory.Exists(Path.Combine(path, "app", "content", "maps")))
                    Conta.DofusPath = path;
                //else
                //{
                //    sr.Close();
                //    DofusPathForm frm = new DofusPathForm(ActualMainForm);
                //    frm.ShowDialog();
                //}
                // TODO Militão 2.0: Crate an interface to get dofus path

            }
            //else
            //{
            //    DofusPathForm frm = new DofusPathForm(ActualMainForm);
            //    frm.ShowDialog();
            //}
            // TODO Militão 2.0: Crate an interface to get dofus path


            FileInfo fileInfo = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BlueSheep\Logs.txt");
            fileInfo.Delete();
            using (fileInfo.Create())
            {
            }

            //fileInfo = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BlueSheep\Packets.txt");
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
                    string combinedPath = Path.Combine(Conta.DofusPath, "app", "data", "i18n", "i18n_" + pays + ".d2i");
                    if (File.Exists(combinedPath))
                    {
                        i18NFileAccessor.Init(combinedPath);
                        break;
                    }
                }
                I18N i18N = new I18N(i18NFileAccessor);
                GameData.Init(Path.Combine(Conta.DofusPath, "app", "data", "common"));
                MapsManager.Init(Path.Combine(Conta.DofusPath, "app", "content", "maps"));
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
            //IntelliSense.InitServers();
        }

    }
}
