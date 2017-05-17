using MageBot.Util.Cryptography;
using MageBot.Util.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MageBot.Core.Groups;
using MageBot.Core.Account;

namespace MageBot.Core.Storage.AccountsManager
{
    public class AccountsFileInteractions
    {
        #region Fields
        public List<Account.Account> Accounts { get; set; } = new List<Account.Account>();
        private List<Account.Account> GroupAccounts { get; set; } = new List<Account.Account>();
        public List<Group> Groups = new List<Group>();
        private readonly string m_SavingFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MageBot\Accounts\accounts.mb";
        private readonly string m_SavingDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MageBot";
        private readonly string m_SavingGroupDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MageBot\Groups";
        #endregion

        #region Public methods
        public void SaveAccountsInfos(List<Account.Account> accounts)
        {
            foreach (Account.Account acc in accounts)
            {
                acc.AccountPassword = CryptageBS.EncryptBS(acc.AccountPassword);
                Accounts.Add(acc);
            }
            if (!Directory.Exists(m_SavingDirectoryPath))
                Directory.CreateDirectory(m_SavingDirectoryPath);
            using (BigEndianWriter writer = new BigEndianWriter())
            {
                writer.WriteInt(Accounts.Count);
                foreach (Account.Account accountObject in Accounts)
                {
                    writer.WriteUTF(accountObject.AccountName);
                    writer.WriteUTF(accountObject.AccountPassword);
                }
                IFormatter binaryFormatter = new BinaryFormatter();
                using (Stream stream = new FileStream(m_SavingFilePath, FileMode.Create, FileAccess.Write))
                {
                    binaryFormatter.Serialize(stream, writer);
                }
            }
            foreach (Account.Account acc in accounts)
                acc.AccountPassword = CryptageBS.DecryptBS(acc.AccountPassword);
        }
        public void RecoverAccountsInfos()
        {
            if (File.Exists(m_SavingFilePath))
            {
                IFormatter binaryFormatter = new BinaryFormatter();
                using (Stream stream = new FileStream(m_SavingFilePath, FileMode.Open, FileAccess.Read))
                {
                    BigEndianWriter writer = (BigEndianWriter)binaryFormatter.Deserialize(stream);
                    using (BigEndianReader reader = new BigEndianReader(writer.Content))
                    {
                        int limite = reader.ReadInt();
                        Accounts = new List<Account.Account>();
                        for (int index = 0; index < limite; index++)
                            Accounts.Add(new Account.Account(reader.ReadUTF(), reader.ReadUTF()));
                    }
                    writer.Dispose();
                    stream.Close();
                }
            }
            foreach (Account.Account accountObject in Accounts)
                accountObject.AccountPassword = CryptageBS.DecryptBS(accountObject.AccountPassword);
        }
        public void SaveGroup(List<Account.Account> accounts, string groupname)
        {
            if (!Directory.Exists(m_SavingGroupDirectoryPath))
                Directory.CreateDirectory(m_SavingGroupDirectoryPath);
            GroupAccounts = accounts;
            using (BigEndianWriter writer = new BigEndianWriter(File.Create(System.IO.Path.Combine(m_SavingGroupDirectoryPath, groupname))))
            {
                writer.WriteInt(GroupAccounts.Count);
                foreach (Account.Account accountObject in GroupAccounts)
                {
                    writer.WriteUTF(accountObject.AccountName);
                    writer.WriteUTF(CryptageBS.EncryptBS(accountObject.AccountPassword));
                }
            }
        }
        public void RecoverGroups()
        {
            foreach (FileInfo file in new DirectoryInfo(m_SavingGroupDirectoryPath).GetFiles())
            {
                byte[] content = File.ReadAllBytes(file.FullName);
                using (BigEndianReader reader = new BigEndianReader(content))
                {
                    int limite = reader.ReadInt();
                    GroupAccounts = new List<Account.Account>();
                    for (int index = 0; index < limite; index++)
                        GroupAccounts.Add(new Account.Account(reader.ReadUTF(), reader.ReadUTF()));
                    Groups.Add(new Group(GroupAccounts, file.Name.Remove(file.Name.Length - 3)));
                    foreach (Account.Account accountObject in GroupAccounts)
                        accountObject.AccountPassword = CryptageBS.DecryptBS(accountObject.AccountPassword);
                }
            }
        }
        #endregion
    }
}
