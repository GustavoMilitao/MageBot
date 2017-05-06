using BlueSheep.Common.IO;
using BlueSheep.Common.Protocol.Messages.Game.Chat;
using BlueSheep.Common.Protocol.Messages.Game.Context.Roleplay.Houses;
using BlueSheep.Common.Protocol.Messages.Game.Interactive;
using BlueSheep.Engine.Types;
using BlueSheep.Interface;
using BlueSheep.Interface.Text;

namespace BlueSheep.Core.Misc
{
    public class HouseBuy
    {
        private AccountUC account;
        public ulong priceHouse;
        public int ElementIdd;
        public int SkillInstanceID;

        public HouseBuy(AccountUC Account)
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
                writer.Content = account.HumanCheck.hash_function(writer.Content);
                MessagePackaging pack = new MessagePackaging(writer);
                pack.Pack((int)msg.MessageID);
                account.SocketManager.Send(pack.Writer.Content);
                if (account.DebugMode.Checked)
                    account.Log(new DebugTextInformation("[SND] 861 (ChatClientMultiMessage)"), 0);
            }
        }

        public void UseHouse()
        {
            using (BigEndianWriter writer = new BigEndianWriter())
            {
                InteractiveUseRequestMessage msg = new InteractiveUseRequestMessage((uint)ElementIdd,(uint)SkillInstanceID);
                msg.Serialize(writer);
                writer.Content = account.HumanCheck.hash_function(writer.Content);
                MessagePackaging pack = new MessagePackaging(writer);
                pack.Pack((int)msg.MessageID);
                account.SocketManager.Send(pack.Writer.Content);
                if (account.DebugMode.Checked)
                    account.Log(new DebugTextInformation("[SND] 5001 (InteractiveUseRequestMessage)"), 0);
            }
        }

        public void Buy()
        {
            HouseBuyRequestMessage msg = new HouseBuyRequestMessage(priceHouse);
            account.SocketManager.Send(msg);
            account.Log(new BotTextInformation("Maison achetée pour " + priceHouse + " kamas !"), 0);
            if (account.PhraseADire.Text.Length != 0)
            {
                Say(account.PhraseADire.Text);
            }
        }

        #endregion
    }
}
