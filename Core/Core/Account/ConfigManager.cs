using BlueSheep.Util.Text.Log;
using System;
using System.IO;
using System.Xml.Serialization;

namespace BlueSheep.Core.Account
{
    public class ConfigManager
    {
        #region Fields
        private Core.Account.Account account;
        public bool Restored { get; set; }
        #endregion

        #region Constructors
        public ConfigManager(Core.Account.Account Account)
        {
            account = Account;
        }
        #endregion

        #region Public Methods
        public bool RecoverConfig()
        {
            string spath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlueSheep", "Accounts", account.AccountName, account.CharacterBaseInformations.Name + ".xml");
            if (File.Exists(spath))
            {
                AccountConfig configuration = DeserializeConfig(spath);
                if (configuration != null)
                {
                    account.Config = configuration;
                    account.Log(new BotTextInformation("Restored settings."), 0);
                    Restored = true;
                    return true;
                }
                account.Log(new ErrorTextInformation("Error to load config file."), 0);
                Restored = false;
                return false;
            }
            account.Log(new BotTextInformation("No config for this character."), 0);
            Restored = false;
            return false;
        }

        public void SaveConfig()
        {
            string spath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlueSheep", "Accounts", account.AccountName, account.CharacterBaseInformations.Name + ".xml");

            if (account.Config != null && account.Config.Enabled)
            {
                Serialize(account.Config, spath);
            }
        }

        public void DeleteConfig()
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlueSheep", "Accounts", account.AccountName, account.CharacterBaseInformations.Name + ".xml");
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
            catch (Exception ex)
            {
                account.Log(new ErrorTextInformation(ex.Message + ex.StackTrace), 0);
            }
        }

        private AccountConfig DeserializeConfig(string file)
        {
            StreamReader sr = new StreamReader(file);
            XmlSerializer seria = new XmlSerializer(typeof(AccountConfig));
            try
            {
                AccountConfig conf = (AccountConfig)seria.Deserialize(sr);
                return conf;
            }
            catch
            {
                account.Log(new ErrorTextInformation("Failed to get Saved config"), 0);
                sr.Close();
            }
            return null;

        }


        #endregion
    }
}
