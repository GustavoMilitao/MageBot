using Util.Util.Text.Log;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MageBot.Core.Account
{
    [Serializable()]
    public class ConfigManager
    {
        #region Fields
        private Account account;
        #endregion

        #region Constructors
        public ConfigManager(MageBot.Core.Account.Account Account)
        {
            account = Account;
        }
        #endregion

        #region Public Methods
        public bool RecoverConfig()
        {
            string spath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MageBot", "Accounts", account.AccountName, account.CharacterBaseInformations.Name + "Config.json");
            if (File.Exists(spath))
            {
                AccountConfig configuration = DeserializeConfig<AccountConfig>(spath);
                if (configuration != null)
                {
                    account.Config = configuration;
                    account.Log(new BotTextInformation("Restored settings."), 0);
                    account.Config.Restored = true;
                    if (account.Config.WaitingForTheSale)
                        account.House = new Misc.HouseBuy(account);
                    return true;
                }
                account.Log(new ErrorTextInformation("Error to load config file."), 0);
                account.Config.Restored = false;
                return false;
            }
            account.Log(new BotTextInformation("No config for this character."), 0);
            account.Config.Restored = false;
            return false;
        }

        public void SaveConfig()
        {
            if (account.Config != null && account.Config.Enabled)
            {
                string spath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MageBot", "Accounts", account.AccountName, account.CharacterBaseInformations.Name + "Config.json");
                CreateDirectoryIfNotExists();
                Serialize(account.Config, spath);
            }
        }

        public void DeleteConfig()
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MageBot", "Accounts", account.AccountName, account.CharacterBaseInformations.Name + "Config.json");
            if (File.Exists(path))
                File.Delete(path);
        }
        #endregion

        #region Private Methods
        private void CreateDirectoryIfNotExists()
        {
            if (!Directory.Exists(System.IO.Path.Combine(
                                    Environment.GetFolderPath(
                                        Environment.SpecialFolder.ApplicationData),
                                    "MageBot", "Accounts", account.AccountName)
                                    ))
                Directory.CreateDirectory(System.IO.Path.Combine(
                                    Environment.GetFolderPath(
                                        Environment.SpecialFolder.ApplicationData),
                                    "MageBot", "Accounts", account.AccountName));
        }

        private void Serialize<T>(T obj, string sConfigFilePath)
        {
            try
            {
                File.WriteAllText(sConfigFilePath,JsonConvert.SerializeObject(obj));
                //JsonSerializer serializer = new JsonSerializer();
                //serializer.Serialize()
                //using (MemoryStream memoryStream = new MemoryStream())
                //using (StreamReader reader = new StreamReader(memoryStream))
                //{
                //    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                //    serializer.WriteObject(memoryStream, obj);
                //}
                //DataContractSerializer XmlBuddy = new DataContractSerializer(typeof(T));
                //System.Xml.XmlWriterSettings MySettings = new System.Xml.XmlWriterSettings();
                //MySettings.Indent = true;
                //MySettings.CloseOutput = true;
                //MySettings.OmitXmlDeclaration = true;
                //System.Xml.XmlWriter MyWriter = System.Xml.XmlWriter.Create(sConfigFilePath, MySettings);
                //XmlBuddy..Serialize(MyWriter, obj);
                //MyWriter.Flush();
                //MyWriter.Close();
            }
            catch (Exception ex)
            {
                account.Log(new ErrorTextInformation(ex.Message + ex.StackTrace), 0);
            }
        }

        private T DeserializeConfig<T>(string file)
        {
            //using (Stream stream = new MemoryStream())
            //{
            //    stream.Write(data, 0, data.Length);
            //    stream.Position = 0;
            //    DataContractSerializer deserializer = new DataContractSerializer(toType);
            //    return deserializer.ReadObject(stream);
            //}
            //StreamReader sr = new StreamReader(file);
            //XmlSerializer seria = new XmlSerializer(typeof(AccountConfig));
            try
            {
                //AccountConfig conf = (AccountConfig)seria.Deserialize(sr);
                //return conf;
                var Content = File.ReadAllText(file);
                var result = JsonConvert.DeserializeObject<T>(Content);
                return result;
            }
            catch
            {
                account.Log(new ErrorTextInformation("Failed to get Saved config"), 0);
            }
            return default(T);
        }


        #endregion
    }
}
