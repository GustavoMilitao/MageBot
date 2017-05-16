using BlueSheep.Util.IO;
using BlueSheep.Protocol.Messages.Game.Chat;
using BlueSheep.Protocol.Messages.Game.Context.Roleplay.Houses;
using BlueSheep.Protocol.Messages.Game.Interactive;
using BlueSheep.Util.Text.Log;
using System;

namespace BlueSheep.Core.Misc
{
    public class HouseBuy
    {
        private Account.Account account;
        public ulong priceHouse;
        public int ElementIdd;
        public int SkillInstanceID;
        public string SentenceToSay { get; set; }

        public HouseBuy(Account.Account Account)
        {
            account = Account;
        }

        #region Methods
        private void Say(string sentence)
        {
            using (BigEndianWriter writer = new BigEndianWriter())
            {
                ChatClientMultiMessage msg = new ChatClientMultiMessage(sentence, 0);
                msg.Serialize(writer);
                writer.Content = account.HumanCheck.Hash_function(writer.Content);
                msg.Pack(writer);
                account.SocketManager.Send(writer.Content);
                account.Log(new DebugTextInformation("[SND] 861 (ChatClientMultiMessage)"), 0);
            }
        }

        public void UseHouse()
        {
            using (BigEndianWriter writer = new BigEndianWriter())
            {
                InteractiveUseRequestMessage msg = new InteractiveUseRequestMessage((uint)ElementIdd, (uint)SkillInstanceID);
                msg.Serialize(writer);
                writer.Content = account.HumanCheck.Hash_function(writer.Content);
                msg.Pack(writer);
                account.SocketManager.Send(writer.Content);
                account.Log(new DebugTextInformation("[SND] 5001 (InteractiveUseRequestMessage)"), 0);
            }
        }

        public void Buy()
        {
            HouseBuyRequestMessage msg = new HouseBuyRequestMessage(priceHouse);
            account.SocketManager.Send(msg);
            account.Log(new BotTextInformation("Maison achetée pour " + priceHouse + " kamas !"), 0);
            if (!String.IsNullOrEmpty(SentenceToSay))
            {
                Say(SentenceToSay);
            }
        }

        #endregion
    }
}
