using MageBot.Core.Fight;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MageBot.Core.Map;
using MageBot.Core.Misc;
using MageBot.Core.Engine.Constants;
using MageBot.Protocol.Types.Game.Character.Choice;
using MageBot.Protocol.Types.Game.Data.Items;
using MageBot.Protocol.Types.Game.Character.Characteristic;
using MageBot.Core.Frame;
using System.IO;
using Util.Util.I18n.Strings;
using Util.Util.Text.Log;
using MageBot.DataFiles.Data.D2o;
using MageBot.Core.Pets;
using MageBot.Core.Job;
using MageBot.Core.Groups;
using MageBot.Core.Inventory;
using MageBot.Core.Map.Elements;
using MageBot.Core.Network;
using MageBot.Core.Engine.Network;
using MageBot.Util.Enums.Internal;

namespace MageBot.Core.Account
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
        public List<Pet> PetsList { get; set; }
        public InteractiveElement Safe { get; set; }
        public CharacterBaseInformations CharacterBaseInformations { get; set; }
        public Pods Pods { get; set; }
        public List<ObjectItem> SafeItems { get; set; }
        public CharacterCharacteristicsInformations CharacterStats { get; set; }
        #endregion

        #region Internal code use
        public Status State { get; set; }
        public WatchDog WatchDog { get; set; }
        public SocketManager SocketManager { get; set; }
        public LatencyFrame LatencyFrame { get; set; }
        public Running Running { get; set; }
        public Queue<int> LastPacketID { get; set; }
        public Queue<Tuple<TextInformation, int>> InformationQueue { get; set; }
        public Dictionary<int, DataBar> InfBars { get; set; }
        public int Sequence { get; set; }
        public bool Busy { get; set; }
        public int LastPacket { get; set; }
        #endregion

        #region Events to Fill in interface
        public event EventHandler QueueChanged;
        public event EventHandler InfBarsChanged;
        #endregion

        #region Updater events (Fill in interface)
        public event EventHandler ActualizePets;
        public event EventHandler ActualizeAccountInfo;
        public event EventHandler ActualizeMap;
        public event EventHandler ActualizeFightStats;
        public event EventHandler ActualizeInventory;
        public event EventHandler Actualizeshop;
        public event EventHandler ActualizeJobs;
        #endregion

        #region ByPass code use
        public HumanCheck HumanCheck { get; set; }
        public string Ticket { get; set; }
        #endregion

        #region Configurations
        public AccountConfig Config { get; set; }
        #endregion

        #endregion

        #region public methods

        public Account(string username, string password, bool socket = true)
        {
            AccountName = username;
            AccountPassword = password;
            InitBars();
            Config = new AccountConfig(this);
            PetsModifiedList = new List<Pet>();
            Config.IsMITM = !socket;
            Config.IsSocket = socket;
            Config.NextMeal = new DateTime();
            Ticket = string.Empty;
            PetsModifiedList = null;
            PetsList = null;
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

            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MageBot", "Accounts", AccountName);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            FightData = new FightData(this);
            MapData = new MapData(this);
            WatchDog = new WatchDog(this);
            InformationQueue = new Queue<Tuple<TextInformation, int>>();
        }

        public void StartFeeding()
        {
            Running = new Running(this);
            Running.Init();
        }

        public void Log(TextInformation text, int verboseLevel)
        {
            InformationQueue.Enqueue(new Tuple<TextInformation, int>(text, verboseLevel));
            OnChanged();
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
                        //string resourceName = MageBot.DataFiles.Data.I18n.I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Interactives, rid).Fields["nameId"]);
                        // This way we can get resource name
                        resourcesToPerformGather.Add(rid);
                    }
                }
            }
            return Gather.GoGather(resourcesToPerformGather);
        }

        public void SetNextMeal()
        {
            foreach (Pet pet in PetsList)
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
                UpdateInfBars();
            }

        }

        public void TryReconnect(int seconds)
        {
            Log(new ConnectionTextInformation(Strings.AutomaticReconnectionIn + " " + seconds + " " + Strings.Seconds + "."), 0);
            SocketManager.Disconnect("Try Reconnect.");
            TimerConnectionThread = new Timer(Reconnect, null, (int)TimeSpan.FromSeconds(seconds).TotalMilliseconds,
                Timeout.Infinite);
        }

        public void Connect()
        {
            if (TimerConnectionThread != null)
                TimerConnectionThread.Change(Timeout.Infinite, Timeout.Infinite);

            Thread.Sleep(GetRandomTime() /*+ new Random().Next(30000)*/);

            Running = new Running(this);

            if (SocketManager != null && SocketManager.State == SocketState.Connected)
                return;

            Log(new ConnectionTextInformation(Strings.Connection), 0);

            if (SocketManager == null)
                SocketManager = new SocketManager(this);

            SocketManager.Connect(new ConnectionInformations(GameConstants.DofusIP1, GameConstants.DofusPort1, Strings.Identification));

            if (Config.Begin)
                GetNextMeal();
        }

        #endregion

        #region Methodes privées

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
            System.Xml.XmlWriterSettings MySettings = new System.Xml.XmlWriterSettings()
            {
                Indent = true,
                CloseOutput = true,
                OmitXmlDeclaration = true
            };
            System.Xml.XmlWriter MyWriter = System.Xml.XmlWriter.Create(sConfigFilePath, MySettings);
            XmlBuddy.Serialize(MyWriter, obj);
            MyWriter.Flush();
            MyWriter.Close();

        }

        private void InitBars()
        {
            InfBars = new Dictionary<int, DataBar>
            {
                { 1, new DataBar() { Text = "Experience" } },
                { 2, new DataBar() { Text = "Life" } },
                { 3, new DataBar() { Text = "Pods" } },
                { 4, new DataBar() { Text = "Kamas" } },
                { 5, new DataBar() { Text = "Pos" } },
                { 7, new DataBar() { Text = "ParentForm" } },
                { 8, new DataBar() { Text = "Level" } },
                { 9, new DataBar() { Text = "Subscribe" } }
            };
        }

        #endregion

        #region Updaters
        public virtual void UpdatePets()
        {
            ActualizePets?.Invoke(this, EventArgs.Empty);
        }

        public virtual void UpdateAccInfo()
        {
            ActualizeAccountInfo?.Invoke(this, EventArgs.Empty);
        }

        public virtual void UpdateMap()
        {
            ActualizeMap?.Invoke(this, EventArgs.Empty);
        }

        public virtual void UpdateFightStats()
        {
            ActualizeFightStats?.Invoke(this, EventArgs.Empty);
        }

        public virtual void UpdateInventory()
        {
            ActualizeInventory?.Invoke(this, EventArgs.Empty);
        }

        public virtual void UpdateJobs()
        {
            ActualizeJobs?.Invoke(this, EventArgs.Empty);
        }

        public virtual void UpdateShop(List<ObjectItemToSell> objectsInfos)
        {
            Actualizeshop?.Invoke(this, new ActualizeShopItemsEventArgs(objectsInfos));
        }

        public virtual void UpdateInfBars()
        {
            InfBarsChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnChanged()
        {
            QueueChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

    }

    public class ActualizeShopItemsEventArgs : EventArgs
    {
        public List<ObjectItemToSell> ObjectsInfos { get; set; }

        public ActualizeShopItemsEventArgs(List<ObjectItemToSell> objectsInfos)
        {
            ObjectsInfos = objectsInfos;
        }
    }
}
