using BlueSheep.Util.IO;
using BlueSheep.Engine.Types;
using BlueSheep.Util.Text.Log;
using BlueSheep.Util.Text.Log.Chat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Threading;
using BlueSheep.Protocol.Types.Game.Context.Roleplay;
using BlueSheep.Protocol.Messages.Game.Chat;

namespace BlueSheep.Core.Misc
{
    public class Flood
    {
        #region Fields
        Account.Account account { get; set; }
        public Dictionary<string, long> ListOfPlayersWithLevel { get; set; }
        public bool AddRandomingSmiley { get; set; }
        public bool AddRandomingNumber { get; set; }
        public bool FloodStarted { get; set; }
        public bool SaveInMemory { get; set; }
        public bool InCommerceChannel { get; set; }
        public bool InRecruitmentChannel { get; set; }
        public bool InGeneralChannel { get; set; }
        public bool InPrivateChannel { get; set; }
        public int MessageCount { get; set; }
        public int PMCount { get; set; }
        public int FloodInterval { get; set; }
        public string FloodContent { get; set; }
        #endregion

        #region Constructors
        public Flood(Account.Account Account)
        {
            account = Account;
            ListOfPlayersWithLevel = new Dictionary<string, long>();
        }
        #endregion

        #region Public Methods
        public void StartFlood(int channel, bool useSmiley, bool useNumbers, string content, int interval)
        {
            Thread t = new Thread(() => StartFlooding(channel, useSmiley, useNumbers, content, interval));
            t.Start();
        }

        public void SendMessage(int channel, string content)
        {
            using (BigEndianWriter writer = new BigEndianWriter())
            {
                ChatClientMultiMessage msg = new ChatClientMultiMessage(content, (byte)channel);
                msg.Serialize(writer);
                writer.Content = account.HumanCheck.hash_function(writer.Content);
                msg.Pack(writer);
                account.SocketManager.Send(writer.Content);
                account.Log(new DebugTextInformation("[SND] 861 (ChatClientMultiMessage)"), 0);
                increase(false);
            }
        }

        public void SendPrivateTo(GameRolePlayCharacterInformations infos, string content = "")
        {
            if (content == String.Empty)
            {
                content = FloodContent;
                long level = (long)Math.Abs((infos.AlignmentInfos.CharacterPower - infos.ContextualId));
                content = content.Replace("%name%", infos.Name).Replace("%level%", Convert.ToString(level));
            }
            if (AddRandomingSmiley)
                content = addRandomSmiley(content);
            if (AddRandomingNumber)
                content = addRandomNumber(content);
            SendPrivateTo(infos.Name, content);
            increase(true);
        }

        public void SendPrivateTo(string name, string content)
        {
            if (mods.Contains(name))
            {
                account.Log(new ErrorTextInformation("[Flood] Error sending private to : " + name + " (Moderator)"), 0);
                return;
            }
            using (BigEndianWriter writer = new BigEndianWriter())
            {
                ChatClientPrivateMessage msg = new ChatClientPrivateMessage(content, name);
                msg.Serialize(writer);
                writer.Content = account.HumanCheck.hash_function(writer.Content);
                msg.Pack(writer);
                account.SocketManager.Send(writer.Content);
                account.Log(new PrivateTextInformation("à " + name + " : " + content), 1);
                account.Log(new DebugTextInformation("[SND] 851 (ChatClientPrivateMessage)"), 0);
            }
        }

        public void SaveNameInMemory(GameRolePlayCharacterInformations infos)
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlueSheep", "Accounts", account.AccountName, "Flood");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            try
            {
                if (ListOfPlayersWithLevel.Count > 0)
                {
                    if (ListOfPlayersWithLevel.Keys.ToList().Find(p => p == infos.Name) != null)
                    {
                        account.Log(new ErrorTextInformation("[ADVANCED FLOOD] Player already loaded !"), 5);
                        return;
                    }
                }
                var swriter = new StreamWriter(path + @"\Players.txt", true);
                long level = (long)Math.Abs((infos.AlignmentInfos.CharacterPower - infos.ContextualId));
                swriter.WriteLine(infos.Name + "," + Convert.ToString(level));
                swriter.Close();
                ListOfPlayersWithLevel.Add(infos.Name, level);
                //account.AccountFlood.AddItem(infos.Name + "," + Convert.ToString(level));
                account.Log(new BotTextInformation("[ADVANCED FLOOD] Player added."), 5);
            }
            catch (Exception ex)
            {
                account.Log(new ErrorTextInformation("[ADVANCED FLOOD] Unable to add the player."), 5);
                account.Log(new ErrorTextInformation(ex.ToString()), 5);
            }
        }
        #endregion

        #region Private Methods
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

        private void increase(bool pm)
        {
            if (pm)
                PMCount++;
            else
                MessageCount++;
        }

        private async void StartFlooding(int channel, bool useSmiley, bool useNumbers, string content, int interval)
        {
            FloodStarted = true;
            string ncontent = content;
            while (FloodStarted == false)
            {
                if (useSmiley == true)
                    ncontent = addRandomSmiley(content);
                if (useNumbers == true)
                    ncontent = addRandomNumber(ncontent);
                SendMessage(channel, ncontent);
                await account.PutTaskDelay(interval * 1000);
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
