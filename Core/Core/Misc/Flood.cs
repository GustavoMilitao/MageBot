using MageBot.Util.IO;
using Util.Util.Text.Log;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Threading;
using MageBot.Protocol.Types.Game.Context.Roleplay;
using MageBot.Protocol.Messages.Game.Chat;
using System.Xml.Serialization;

namespace MageBot.Core.Misc
{
    [Serializable()]
    public class Flood
    {
        #region Fields
        Account.Account Account { get; set; }
        public Dictionary<string, long> ListOfPlayersWithLevel { get; set; }
        public bool FloodStarted { get; set; }
        public int MessageCount { get; set; }
        public int PMCount { get; set; }
        #endregion

        #region Constructors
        public Flood(Account.Account Account)
        {
            this.Account = Account;
            ReadListAdvancedFloodFromDisk();
        }
        #endregion

        #region Public Methods
        public void StartFlood()
        {
            Thread t = null;
            if (Account.Config.FloodInCommerceChannel)
            {
                t = new Thread(() => StartFlooding(5, Account.Config.AddRandomingSmiley, Account.Config.AddRandomingNumber, Account.Config.FloodContent, Account.Config.FloodInterval));
                t.Start();
            }
            if (Account.Config.FloodInRecruitmentChannel)
            {
                t = new Thread(() => StartFlooding(6, Account.Config.AddRandomingSmiley, Account.Config.AddRandomingNumber, Account.Config.FloodContent, Account.Config.FloodInterval));
                t.Start();
            }
            if (Account.Config.FloodInGeneralChannel)
            {
                t = new Thread(() => StartFlooding(0, Account.Config.AddRandomingSmiley, Account.Config.AddRandomingNumber, Account.Config.FloodContent, Account.Config.FloodInterval));
                t.Start();
            }
        }

        public void StartPrivateFloodOrInfoFromListTo(List<GameRolePlayCharacterInformations> listaPlayers, string To = "")
        {
            foreach (var elem in listaPlayers)
            {
                try
                {
                    long level = (long)Math.Abs((elem.AlignmentInfos.CharacterPower - elem.ContextualId));
                    Account.Config.FloodContent = Account.Config.FloodContent.Replace("%name%", elem.Name).Replace("%level%", level.ToString());
                    if (String.IsNullOrEmpty(To))
                    {
                        SendPrivateTo(elem.Name, Account.Config.FloodContent);
                    }
                    else
                    {
                        SendPrivateTo(To, Account.Config.FloodContent);
                    }
                }
                catch (Exception)
                {
                    if (String.IsNullOrEmpty(To))
                    {
                        Account.Log(new ErrorTextInformation("Impossible to send flood to : " + elem.Name), 3);
                    }
                    else
                    {
                        Account.Log(new ErrorTextInformation("Impossible to send informations of : " + elem.Name + " to " + To), 3);
                    }
                }
            }
        }

        public void SendMessage(int channel, string content)
        {
            using (BigEndianWriter writer = new BigEndianWriter())
            {
                ChatClientMultiMessage msg = new ChatClientMultiMessage(content, (byte)channel);
                msg.Serialize(writer);
                writer.Content = Account.HumanCheck.Hash_function(writer.Content);
                msg.Pack(writer);
                Account.SocketManager.Send(writer.Content);
                Account.Log(new DebugTextInformation("[SND] 861 (ChatClientMultiMessage)"), 0);
                increase(false);
            }
        }

        public void SendPrivateTo(GameRolePlayCharacterInformations infos, string content = "")
        {
            if (content == String.Empty)
            {
                content = Account.Config.FloodContent;
                long level = (long)Math.Abs((infos.AlignmentInfos.CharacterPower - infos.ContextualId));
                content = content.Replace("%name%", infos.Name).Replace("%level%", Convert.ToString(level));
            }
            if (Account.Config.AddRandomingSmiley)
                content = addRandomSmiley(content);
            if (Account.Config.AddRandomingNumber)
                content = addRandomNumber(content);
            SendPrivateTo(infos.Name, content);
            increase(true);
        }

        public void SendPrivateTo(string name, string content)
        {
            if (mods.Contains(name))
            {
                Account.Log(new ErrorTextInformation("[Flood] Error sending private to : " + name + " (Moderator)"), 0);
                return;
            }
            using (BigEndianWriter writer = new BigEndianWriter())
            {
                ChatClientPrivateMessage msg = new ChatClientPrivateMessage(content, name);
                msg.Serialize(writer);
                writer.Content = Account.HumanCheck.Hash_function(writer.Content);
                msg.Pack(writer);
                Account.SocketManager.Send(writer.Content);
                Account.Log(new PrivateTextInformation("à " + name + " : " + content), 1);
                Account.Log(new DebugTextInformation("[SND] 851 (ChatClientPrivateMessage)"), 0);
            }
        }

        public void SaveNameInDisk()
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MageBot", "Accounts", Account.AccountName, "Flood");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            try
            {
                StreamWriter sw = new StreamWriter(path + @"\Players.txt");
                foreach(KeyValuePair<string, long> player in ListOfPlayersWithLevel)
                {
                    sw.WriteLine(player.Key + "," + player.Value);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                Account.Log(new ErrorTextInformation("[ADVANCED FLOOD] Unable to add the player."), 5);
                Account.Log(new ErrorTextInformation(ex.ToString()), 5);
            }
        }

        public void increase(bool pm)
        {
            if (pm)
                PMCount++;
            else
                MessageCount++;
        }

        #endregion

        #region Private Methods
        private void ReadListAdvancedFloodFromDisk()
        {
            ListOfPlayersWithLevel = new Dictionary<string, long>();
            string pathPlayers = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MageBot", "Accounts", Account.AccountName, "Flood");
            if (!Directory.Exists(pathPlayers))
                Directory.CreateDirectory(pathPlayers);
            if (File.Exists(pathPlayers + @"\Players.txt"))
            {
                var sr = new StreamReader(pathPlayers + @"\Players.txt");
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] parsed = line.Split(',');
                    if (parsed.Length > 1)
                        ListOfPlayersWithLevel.Add(parsed[0], int.Parse(parsed[1]));
                    else
                    {
                        sr.Close();
                        File.Delete(pathPlayers + @"\Players.txt");
                        return;
                    }
                }
                sr.Close();
                Account.Log(new DebugTextInformation("[ADVANCED FLOOD] Players loaded."), 5);
            }
        }

        private string addRandomSmiley(string content)
        {
            int randomIndex = new Random().Next(0, 8);
            string nCon = content + " " + smileys[randomIndex];
            return nCon;
        }

        private string addRandomNumber(string content)
        {
            int randomIndex = new Random().Next(0, 500);
            string nCon = content + " " + randomIndex.ToString();
            return nCon;
        }

        private void StartFlooding(int channel, bool useSmiley, bool useNumbers, string content, int interval)
        {
            FloodStarted = true;
            string ncontent = content;
            while (FloodStarted)
            {
                if (useSmiley)
                    ncontent = addRandomSmiley(content);
                if (useNumbers)
                    ncontent = addRandomNumber(ncontent);
                SendMessage(channel, ncontent);
                Account.Wait(interval * 1000);
            }
        }
        #endregion

        #region Enums
        public static readonly IList<String> smileys = new ReadOnlyCollection<string>
        (new List<String> { ":)", ";)", "=)", ":D", ":p", "=p", ":d", "=d", "=P" });

        private static readonly IList<String> mods = new ReadOnlyCollection<string>
        (new List<String> { "[Japlo]" ,"[Lobeline]","[Eknelis]" ,"[Miaidaouh]", "[Alkalino]", "[Seekah]","[Taikorg]",
            "[Lytimelle]","[Gowolik]","[Diospyros]", "[TobliK]","[Simeth]","[Gazviv]", "[Prag-Matik]","[Maatastrea]",
            "[Griffinx]", "[Selvetarm]", "[Jial]", "[Haeo-Lien]", "[VeniVidi]", "[Falgoryn]","[Ayuzal]", "[Pad-Panikk]",
            "[Portgas]", "[Arkansyelle]","[Padalgarath]", "[Semitam]", "[Latnac]", "[Fumikiri]", "[Saskhya]", "[Vandavarya]",
            "[Modorak]", "[Yesht]", "[Alikaric]", "[Enyden]" });
        #endregion
    }
}
