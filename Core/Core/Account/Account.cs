using BlueSheep.Core.Fight;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlueSheep.Core.Map;
using BlueSheep.Core.Misc;
using BlueSheep.Engine.Constants;
using BlueSheep.Protocol.Types.Game.Character.Choice;
using BlueSheep.Protocol.Types.Game.Data.Items;
using BlueSheep.Protocol.Types.Game.Character.Characteristic;
using BlueSheep.Core.Frame;
using System.IO;
using BlueSheep.Util.I18n.Strings;
using BlueSheep.Util.Text.Log;
using BlueSheep.Common.Data.D2o;
using BlueSheep.Core.Pets;
using BlueSheep.Core.Job;
using BlueSheep.Core.Groups;
using BlueSheep.Core.Inventory;
using BlueSheep.Core.Map.Elements;
using BlueSheep.Core.Network;
using BlueSheep.Engine.Network;
using BlueSheep.Util.Enums.Internal;

namespace BlueSheep.Core.Account
{
    public class Account
    {
        #region Properties

        #region Account entities

        #region Main Informations

        public string AccountName { get; set; }
        public string AccountPassword { get; set; }

        #endregion

        public Thread ConnectionThread { get; set; }
        public Timer TimerConnectionThread { get; set; }
        public BFight Fight { get; set; }
        public FightData FightData { get; set; }
        public Map.Map Map { get; set; }
        public MapData MapData { get; set; }
        public Inventory.Inventory Inventory { get; set; }
        public List<BSpell> Spells { get; set; }
        public Group MyGroup { get; set; }
        public HouseBuy House { get; set; }
        public Npc.Npc Npc { get; set; }
        public List<Job.Job> Jobs { get; set; }
        public Gather Gather { get; set; }
        public List<Pet> PetsModifiedList { get; set; }
        public List<Pet> petsList { get; set; }
        public InteractiveElement Safe { get; set; }
        public CharacterBaseInformations CharacterBaseInformations { get; set; }
        public Pods Pods { get; set; }
        public List<ObjectItem> SafeItems { get; set; }
        public CharacterCharacteristicsInformations CharacterStats { get; set; }
        #endregion

        #region Internal code use
        public bool Busy { get; set; }
        public Status State { get; set; }
        public WatchDog WatchDog { get; set; }
        public SocketManager SocketManager { get; set; }
        public int Sequence { get; set; }
        public LatencyFrame LatencyFrame { get; set; }
        public Running Running { get; set; }
        public Queue<int> LastPacketID { get; set; }
        public int LastPacket { get; set; }
        public Queue<Tuple<TextInformation, int>> InformationQueue { get; set; }
        public Dictionary<int, DataBar> InfBars { get; set; }
        #endregion

        #region ByPass code use
        public HumanCheck HumanCheck { get; set; }
        public string Ticket { get; set; }
        #endregion

        #region Configurations
        public AccountConfig Config { get; set; }
        #endregion

        #endregion

        public Account(string username, string password, bool socket = true)
        {
            initBars();
            Config = new AccountConfig();
            AccountName = username;
            AccountPassword = password;
            PetsModifiedList = new List<Pet>();
            Config.IsMITM = !socket;
            Config.NextMeal = new DateTime();
            Ticket = string.Empty;
            PetsModifiedList = null;
            petsList = null;
            Safe = null;
            CharacterBaseInformations = null;
            Sequence = 0;
            LatencyFrame = null;
            Pods = null;
            SafeItems = new List<ObjectItem>();
            LastPacketID = new Queue<int>();
            Running = null;
            Fight = null;
            Map = new Map.Map(this);
            Inventory = new Inventory.Inventory(this);
            Spells = new List<BSpell>();
            Npc = new Npc.Npc(this);
            Jobs = new List<Job.Job>();
            Gather = new Gather(this);

            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlueSheep", "Accounts", AccountName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            Config.ConfigRecover = new ConfigManager(this);
            Config.Flood = new Flood(this);
            FightData = new FightData(this);
            MapData = new MapData(this);
            WatchDog = new WatchDog(this);
            InformationQueue = new Queue<Tuple<TextInformation, int>>();
            Config.MonsterRestrictions = new List<MonsterRestrictions>();



            ////Heroic mode
            //HeroicUC = new HeroicUC(this);
            //FloodPage.TabPages[3].Controls.Add(HeroicUC);
            //HeroicUC.Show();

            ////Items management
            //GestItemsUC = new GestItemsUC(this);
            //tabPage9.Controls.Add(GestItemsUC);
            //GestItemsUC.Show();

            ////Carac
            //CaracUC = new CaracUC(this);
            //StatsPage.Controls.Add(CaracUC);
            //CaracUC.Show();

            ////Regen
            //RegenUC = new RegenUC(this);
            //RegenPage.Controls.Add(RegenUC);
            //RegenUC.Show();

            ////Flood
            //FloodUC = new FloodUC(this);
            //tabPage2.Controls.Add(FloodUC);
            //FloodUC.Show();
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

        public void StartFeeding()
        {
            Running = new Running(this);
            Running.Init();
        }

        public void Log(TextInformation text, int verboseLevel)
        {
            InformationQueue.Enqueue(new Tuple<TextInformation, int>(text, verboseLevel));
        }

        public void Init()
        {
            ConnectionThread = new Thread(Connect);
            ConnectionThread.Start();
        }

        public void InitMITM()
        {
            SocketManager = new SocketManager(this);
            SocketManager.InitMITM();
        }

        public void Disconnect()
        {
            if (SocketManager.State == SocketState.Connected)
            {
                SocketManager.DisconnectFromGUI();
            }
        }

        public bool PerformGather(List<int> ResourcesToPerformGather)
        {
            return Gather.GoGather(ResourcesToPerformGather);
        }

        public bool PerformGather()
        {
            List<int> resourcesToPerformGather = new List<int>();
            foreach (Job.Job job in Jobs)
            {
                foreach (int i in job.Skills)
                {
                    DataClass d = GameData.GetDataObject(D2oFileEnum.Skills, i);
                    if ((int)d.Fields["gatheredRessourceItem"] != -1 && job.Level >= (int)d.Fields["levelMin"])
                    {
                        int rid = (int)d.Fields["interactiveId"];
                        //string resourceName = I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Interactives, rid).Fields["nameId"]);
                        // This way we can get resource name
                        resourcesToPerformGather.Add(rid);
                    }
                }
            }
            return Gather.GoGather(resourcesToPerformGather);
        }

        public void SetNextMeal()
        {
            foreach (Pet pet in petsList)
            {
                if (pet.NextMeal.Year == 1)
                    continue;

                if (pet.NonFeededForMissingFood)
                    continue;

                if (Config.NextMeal.Year == 1)
                {
                    Config.NextMeal = pet.NextMeal;
                    continue;
                }

                if (pet.NextMeal <= Config.NextMeal)
                    Config.NextMeal = pet.NextMeal;
            }
        }

        public void GetNextMeal()
        {
            Config.NextMealP = new DateTime();

            if (Config.NextMealP.Year == 1)
            {
                Config.NextMealP = new DateTime(Config.NextMeal.Year, Config.NextMeal.Month, Config.NextMeal.Day, Config.NextMeal.Hour,
                    Config.NextMeal.Minute, 0);
            }

            else if (Config.NextMeal <= Config.NextMealP)
            {
                Config.NextMealP = new DateTime(Config.NextMeal.Year, Config.NextMeal.Month, Config.NextMeal.Day, Config.NextMeal.Hour,
                    Config.NextMeal.Minute, 0);
            }

            if (Config.NextMealP.Year != 1)
            {
                Config.NextMealP = Config.NextMealP.AddMinutes(2);

                DateTime difference = new DateTime((Config.NextMealP - DateTime.Now).Ticks);

                Log(new GeneralTextInformation("Prochain repas dans " + difference.Hour + " heure(s) " +
                     difference.Minute + " minute(s)."), 3);
                SocketManager.Disconnect("Wait before next meal.");

                if (TimerConnectionThread == null)
                    TimerConnectionThread = new Timer(Reconnect, null,
                        (int)TimeSpan.FromHours(difference.Hour).TotalMilliseconds +
                        (int)TimeSpan.FromMinutes(difference.Minute).TotalMilliseconds, Timeout.Infinite);
                else
                    TimerConnectionThread.Change((int)TimeSpan.FromHours(difference.Hour).TotalMilliseconds +
                        (int)TimeSpan.FromMinutes(difference.Minute).TotalMilliseconds, Timeout.Infinite);
            }
            else
            {
                Log(new GeneralTextInformation("Aucune nourriture disponible, pas de prochaine connexion."), 1);
            }
        }


        public void SetStatus(Status state)
        {
            this.State = state;
        }

        public bool AllowedGroup(List<string> monsters)
        {
            if (Config.MonsterRestrictions.Count > 0)
            {
                foreach (MonsterRestrictions i in Config.MonsterRestrictions)
                {
                    switch (i.RestrictionLevel)
                    {
                        case Util.Enums.Internal.RestrictionLevel.Forbidden:
                            switch (i.Operator)
                            {
                                case Util.Enums.Internal.Operator.More:
                                    if (monsters.FindAll(f => i.MonsterName == f).Count > Convert.ToInt32(i.Quantity))
                                        return false;
                                    else
                                        continue;
                                case Util.Enums.Internal.Operator.Less:
                                    if (monsters.FindAll(f => i.MonsterName == f).Count < Convert.ToInt32(i.Quantity))
                                        return false;
                                    else
                                        continue;
                                case Util.Enums.Internal.Operator.Equal:
                                    if (monsters.FindAll(f => i.MonsterName == f).Count == Convert.ToInt32(i.Quantity))
                                        return false;
                                    else
                                        continue;
                                default:
                                    continue;
                            }
                        case Util.Enums.Internal.RestrictionLevel.Required:
                            switch (i.Operator)
                            {
                                case Util.Enums.Internal.Operator.More:
                                    if (!(monsters.FindAll(f => i.MonsterName == f).Count > Convert.ToInt32(i.Quantity)))
                                        return false;
                                    else
                                        continue;
                                case Util.Enums.Internal.Operator.Less:
                                    if (!(monsters.FindAll(f => i.MonsterName == f).Count < Convert.ToInt32(i.Quantity)))
                                        return false;
                                    else
                                        continue;
                                case Util.Enums.Internal.Operator.Equal:
                                    if (!(monsters.FindAll(f => i.MonsterName == f).Count == Convert.ToInt32(i.Quantity)))
                                        return false;
                                    else
                                        continue;
                                default:
                                    continue;
                            }
                    }
                }
            }
            return true;
        }

        public async Task PutTaskDelay(int milisec)
        {
            await Task.Delay(milisec);
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

        public void TryReconnect(int seconds)
        {
            Log(new ConnectionTextInformation(Strings.AutomaticReconnectionIn + " " + seconds + " " + Strings.Seconds + "."), 0);
            SocketManager.Disconnect("Try Reconnect.");
            //TODO : Make it an ENUM
            TimerConnectionThread = new Timer(Reconnect, null, (int)TimeSpan.FromSeconds(seconds).TotalMilliseconds,
                Timeout.Infinite);
        }

        #region Methodes privées
        public void Connect()
        {
            if (TimerConnectionThread != null)
                TimerConnectionThread.Change(Timeout.Infinite, Timeout.Infinite);

            Thread.Sleep(GetRandomTime() + new Random().Next(10000));

            Running = new Running(this);

            if (SocketManager != null && SocketManager.State == SocketState.Connected)
                return;

            Log(new ConnectionTextInformation(Strings.Connection), 0);

            if (SocketManager == null)
                SocketManager = new SocketManager(this);

            SocketManager.Connect(new ConnectionInformations("213.248.126.40", 5555, Strings.Identification));
        }

        private void Reconnect(object state)
        {
            Init();
        }

        private static int GetRandomTime()
        {
            Random random = new Random();

            return random.Next(500, 1250);
        }

        private void Serialize<T>(T obj, string sConfigFilePath)
        {

            System.Xml.Serialization.XmlSerializer XmlBuddy = new System.Xml.Serialization.XmlSerializer(typeof(T));
            System.Xml.XmlWriterSettings MySettings = new System.Xml.XmlWriterSettings();
            MySettings.Indent = true;
            MySettings.CloseOutput = true;
            MySettings.OmitXmlDeclaration = true;
            System.Xml.XmlWriter MyWriter = System.Xml.XmlWriter.Create(sConfigFilePath, MySettings);
            XmlBuddy.Serialize(MyWriter, obj);
            MyWriter.Flush();
            MyWriter.Close();

        }
        #endregion

    }
}
