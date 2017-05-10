using BlueSheep.Common.Types;
using BlueSheep.Core.Fight;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlueSheep.Core.Map;
using BlueSheep.Core.Path;
using BlueSheep.Core.Misc;
using BlueSheep.Engine.Constants;
using BlueSheep.Engine.Enums;
using BlueSheep.Engine.Types;
using BlueSheep.Engine.Network;
using BlueSheep.Common.Protocol.Types.Game.Interactive;
using BlueSheep.Common.Protocol.Types.Game.Character.Choice;
using BlueSheep.Engine.Frame;
using BlueSheep.Common.Protocol.Types.Game.Data.Items;
using BlueSheep.Common.Protocol.Types.Game.Character.Characteristic;
using System.IO;
using BlueSheep.Properties.I18n.Strings;
using BlueSheep.Util.Text.Log;
using BlueSheep.Common.Data.D2o;

namespace BlueSheep.Core.Account
{
    public class Account
    {
        #region Properties
        public string AccountName { get; set; }
        public string AccountPassword { get; set; }
        public Thread ConnectionThread { get; set; }
        public Timer TimerConnectionThread { get; set; }
        public DateTime NextMeal { get; set; }
        public bool IsMaster { get; set; }
        public bool IsSlave { get; set; }
        public BFight Fight { get; set; }
        public FightData FightData { get; set; }
        public Map.Map Map { get; set; }
        public MapData MapData { get; set; }
        public Inventory.Inventory Inventory { get; set; }
        public List<Spell> Spells { get; set; }
        public PathManager Path { get; set; }
        public Group MyGroup { get; set; }
        public HouseBuy House { get; set; } = null;
        public Npc.Npc Npc { get; set; }
        public Flood Flood { get; set; }
        public List<Job.Job> Jobs { get; set; }
        public bool Busy { get; set; }
        public Gather Gather { get; set; }
        public HumanCheck HumanCheck { get; set; }
        public bool IsMITM { get; set; }
        public Status state { get; set; }
        public ConfigManager ConfigManager { get; set; }
        public FightParser FightParser { get; set; }
        public WatchDog WatchDog { get; set; }
        public SocketManager SocketManager { get; set; }
        public string Ticket { get; set; }
        public List<Pet> PetsModifiedList { get; set; }
        public List<Pet> petsList { get; set; }
        public InteractiveElement Safe { get; set; }
        public CharacterBaseInformations CharacterBaseInformations { get; set; }
        public int Sequence { get; set; }
        public LatencyFrame LatencyFrame { get; set; }
        public Pods Pods { get; set; }
        public List<ObjectItem> SafeItems { get; set; }
        public Running Running { get; set; }
        public Queue<int> LastPacketID { get; set; }
        public int LastPacket { get; set; }
        public int MapID { get; set; }
        public CharacterCharacteristicsInformations CharacterStats { get; set; }
        public Queue<Tuple<TextInformation, int>> InformationQueue { get; set; }
        public int MinMonstersNumber { get; set; }
        public int MaxMonstersNumber { get; set; }
        public int MinMonstersLevel { get; set; }
        public int MaxMonstersLevel { get; set; }
        public List<MonsterRestrictions> MonsterRestrictions { get; set; }
        public Timer WaitTime { get; set; }
        public bool CanExecuteAction { get; set; }
        public Dictionary<int, DataBar> InfBars { get; set; }
        public bool DebugMode { get; set; }
        public DateTime NextMealP { get; set; }
        public int MaxPodsPercent { get; set; }
        public bool Enabled { get; set; }
        public ulong MaxPriceHouse { get; set; }
        public string HouseSearcherLogPath { get; set; }
        public bool LockingFights { get; set; }
        public bool RelaunchPath { get; set; }
        #endregion

        public Account(string username, string password, bool socket)
        {
            AccountName = username;
            AccountPassword = password;
            PetsModifiedList = new List<Pet>();
            IsMITM = !socket;
            NextMeal = new DateTime();
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
            Spells = new List<Spell>();
            Npc = new Npc.Npc(this);
            Jobs = new List<Job.Job>();
            Gather = new Gather(this);
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlueSheep", "Accounts", AccountName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            ConfigManager = new ConfigManager(this);
            Flood = new Flood(this);
            FightData = new FightData(this);
            MapData = new MapData(this);
            WatchDog = new WatchDog(this);
            InformationQueue = new Queue<Tuple<TextInformation, int>>();
            MonsterRestrictions = new List<Core.Fight.MonsterRestrictions>();
            CanExecuteAction = true;
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

                if (NextMeal.Year == 1)
                {
                    NextMeal = pet.NextMeal;
                    continue;
                }

                if (pet.NextMeal <= NextMeal)
                    NextMeal = pet.NextMeal;
            }
        }

        public void GetNextMeal()
        {
            NextMealP = new DateTime();

            if (NextMealP.Year == 1)
            {
                NextMealP = new DateTime(NextMeal.Year, NextMeal.Month, NextMeal.Day, NextMeal.Hour,
                    NextMeal.Minute, 0);
            }

            else if (NextMeal <= NextMealP)
            {
                NextMealP = new DateTime(NextMeal.Year, NextMeal.Month, NextMeal.Day, NextMeal.Hour,
                    NextMeal.Minute, 0);
            }

            if (NextMealP.Year != 1)
            {
                NextMealP = NextMealP.AddMinutes(2);

                DateTime difference = new DateTime((NextMealP - DateTime.Now).Ticks);

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
            this.state = state;
        }

        public bool AllowedGroup(List<string> monsters)
        {
            if (MonsterRestrictions.Count > 0)
            {
                foreach (MonsterRestrictions i in MonsterRestrictions)
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

        public void CanExecAction(object state)
        {
            CanExecuteAction = true;
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
        private void Connect()
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
