using BlueSheep.Common.Types;
using BlueSheep.Core.Fight;
using BlueSheep.Core.Job;
using BlueSheep.Interface;
using BlueSheep.Util.Text.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BlueSheep.Engine.Types
{
    public class ConfigManager
    {
        #region Fields
        private Core.Account.Account account;
        public bool Restored = false;
        #endregion

        #region Constructors
        public ConfigManager(Core.Account.Account Account)
        {
            account = Account;
        }
        #endregion

        #region Public Methods
        public void RecoverConfig()
        {
            
            string spath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlueSheep", "Accounts", account.AccountName, account.CharacterBaseInformations.Name + ".xml");
            if (File.Exists(spath))
            {
                Config conf = DeserializeConfig(spath);
                if (conf == null)
                    return;
                if (!String.IsNullOrEmpty(conf.m_AIPath))
                {
                    account.FightParser = new FightParser(account, conf.m_AIPath, conf.m_IA);
                    account.Fight = new BFight(account, account.FightParser, account.FightData);
                    //account.ModLabel(conf.m_IA, account.NomIA);
                    // TODO Militão: Populate the new interface
                }
                if (conf.m_Path != null && conf.m_BotPath != null && conf.m_Path != "")
                {
                    account.Path = new Core.Path.PathManager(account, conf.m_Path, conf.m_BotPath);
                    if (conf.m_BotPath != null)
                        account.Log(new BotTextInformation("Path loaded : " + conf.m_BotPath), 0);
                    else
                        account.Log(new BotTextInformation("Path loaded : UNKNOWN"), 0);
                }
                
                if (account.Fight == null)
                {
                    account.Log(new ErrorTextInformation("WARNING : You have not loaded any AI"), 0);
                }

                if (!String.IsNullOrEmpty(conf.m_FloodContent))
                {
                    account.Flood.FloodContent  = conf.m_FloodContent;
                }
                //if (conf.m_L1R != null)
                //{
                //    Dictionary<string, int> ressources = conf.m_L1R.ToDictionary(x => x, x => conf.m_L2R[conf.m_L1R.IndexOf(x)]);
                //    account.Gather.Stats = ressources;
                //}

                //if (conf.m_AutoUp.Count > 0)
                //{
                //    if (conf.m_AutoUp[0])
                //        account.CaracUC.VitaRb.Checked = true;
                //    else if (conf.m_AutoUp[1])
                //        account.CaracUC.WisRb.Checked = true;
                //    else if (conf.m_AutoUp[2])
                //        account.CaracUC.StreRb.Checked = true;
                //    else if (conf.m_AutoUp[3])
                //        account.CaracUC.InteRb.Checked = true;
                //    else if (conf.m_AutoUp[4])
                //        account.CaracUC.LuckRb.Checked = true;
                //    else if (conf.m_AutoUp[5])
                //        account.CaracUC.AgiRb.Checked = true;
                //}
                // TODO Militão: Add Character module
                if (conf.m_Elevage != null)
                    account.MapData.Begin = (bool)conf.m_Elevage;
                if (conf.m_IsLockingFight)
                    account.LockingFights = conf.m_IsLockingFight;
                //if (conf.m_RegenValue != null)
                //    account.RegenChoice.Value = conf.m_RegenValue;
                // TODO Militão: Add regen module
                if (conf.m_Restrictions.Count > 0)
                {
                    account.MinMonstersLevel = (int)conf.m_Restrictions[0];
                    account.MaxMonstersLevel = (int)conf.m_Restrictions[1];
                    account.MinMonstersNumber = (int)conf.m_Restrictions[2];
                    account.MaxMonstersNumber = (int)conf.m_Restrictions[3];
                }
                //if (conf.m_AutoDeletion != null)
                //    account.GestItemsUC.AutoDeletionBox.Checked = conf.m_AutoDeletion;
                // TODO Militão: Add Items module
                if (conf.m_RelaunchPath && account.Path != null)
                    account.Path.Relaunch = true;

                account.Log(new BotTextInformation("Restored settings."), 0);
            }
            else
            {
                account.Log(new BotTextInformation("No config for this character."), 0);
            }
            Restored = true;
        }

        public void SaveConfig()
        {
            if (!account.Enabled)
                return;
            List<BSpell> lspells = new List<BSpell>();
            string ia = "NO IA loaded";
            string AIpath = "";
            //Dictionary<DateTime, int> exp = new Dictionary<DateTime,int>();
            //Dictionary<string,int> winLose = new Dictionary<string,int>();

            if (account.FightParser != null)
            {
                ia = account.FightParser.Name;
                AIpath = account.FightParser.path;
                //exp = account.Fight.xpWon;
                //winLose = account.Fight.winLoseDic;
            }

            string path = "";
            if (account.Path != null && !string.IsNullOrEmpty(account.Path.path))
            {
                path = account.Path.path;
            }

            string flood = String.Empty;
            if (!String.IsNullOrEmpty(account.Flood.FloodContent))
            {
                flood = account.Flood.FloodContent;
            }

            string pathBot = "";
            if (account.Path != null)
                pathBot = account.Path.pathBot;

            List<bool> AutoUp = new List<bool>() { false, false, false, false, false, false };
            //if (account.CaracUC.VitaRb.Checked)
            //    AutoUp[0] = true;
            //else if (account.CaracUC.WisRb.Checked)
            //    AutoUp[1] = true;
            //else if (account.CaracUC.StreRb.Checked)
            //    AutoUp[2] = true;
            //else if (account.CaracUC.InteRb.Checked)
            //    AutoUp[3] = true;
            //else if (account.CaracUC.LuckRb.Checked)
            //    AutoUp[4] = true;
            //else if (account.CaracUC.AgiRb.Checked)
            //    AutoUp[5] = true;
            //TODO Militão: Add Character module

            bool isLockingFight = account.LockingFights;
            //decimal RegenValue = account.RegenChoice.Value;
            //TODO Militão: Add Regen module

            List<decimal> Restrictions = new List<decimal>(){
                account.MinMonstersLevel,
                account.MaxMonstersLevel,
                account.MinMonstersNumber,
                account.MaxMonstersNumber};

            //bool AutoDeletion = account.GestItemsUC.AutoDeletionBox.Checked;
            //TODO Militão: Add Items module

            bool RelaunchPath = account.RelaunchPath;
            //Dictionary<string, int> ressources = account.Gather.Stats;
            //List<string> L1R = ressources.Keys.ToList();
            //List<int> L2R = ressources.Values.ToList();
            //foreach (Job jb in account.Jobs)
            //    jb.exportToXml();
            //TODO Militão: Create export to xml to job

            //Config conf = new Config(path, flood, pathBot, ia, AIpath, account.MapData.Begin, 
            //    AutoUp, isLockingFight, RegenValue, Restrictions, AutoDeletion, RelaunchPath/*,GestItems*/);//, ressources, exp, winLose); 
            //string spath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlueSheep", "Accounts", account.AccountName, account.CharacterBaseInformations.Name + ".xml");
            //Serialize(conf, spath);
            //TODO Militão: Add Some modules

        }

        public void DeleteConfig()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlueSheep", "Accounts", account.AccountName, account.CharacterBaseInformations.Name + ".xml");
            if (File.Exists(path))
                File.Delete(path);
        }
        #endregion

        #region Private Methods
        private void Serialize<T>(T obj, string sConfigFilePath)
        {
            try
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
            catch(Exception ex)
            {
                account.Log(new ErrorTextInformation(ex.Message + ex.StackTrace), 0);
            }
        }

        public Config DeserializeConfig(string file)
        {
                StreamReader sr = new StreamReader(file);
                XmlSerializer seria = new XmlSerializer(typeof(Config));
                try
                {
                    Config conf = (Config)seria.Deserialize(sr);
                    return conf;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur de configuration. Supprimez votre configuration et réessayez.");
                    sr.Close();
                }
                return null;
                
        }

        
        #endregion
    }
}
