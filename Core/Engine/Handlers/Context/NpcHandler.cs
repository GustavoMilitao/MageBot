using MageBot.DataFiles.Data.D2o;
using MageBot.Util.IO;
using MageBot.Protocol.Messages.Game.Context.Roleplay.Npc;
using MageBot.Protocol.Messages.Game.Dialog;
using MageBot.Protocol.Messages.Game.Inventory.Exchanges;
using MageBot.Util.Enums.Internal;
using MageBot.Protocol.Messages;
using Util.Util.Text.Log;
using System.Linq;

namespace MageBot.Core.Engine.Handlers.Context
{
    class NpcHandler
    {
        [MessageHandler(typeof(ExchangeStartOkNpcShopMessage))]
        public static void GameContextRemoveElementMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            ExchangeStartOkNpcShopMessage msg = (ExchangeStartOkNpcShopMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.SetStatus(Status.Exchanging);
        }

        [MessageHandler(typeof(LeaveDialogMessage))]
        public static void NpcDialogCreationMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            LeaveDialogMessage msg = (LeaveDialogMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.SetStatus(Status.None);
        }

        [MessageHandler(typeof(NpcDialogCreationMessage))]
        public static void LeaveDialogMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            NpcDialogCreationMessage msg = (NpcDialogCreationMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.Npc.Id = msg.NpcId;
            account.Npc.Entity = account.MapData.Npcs.FirstOrDefault((npc) => (int)npc.ContextualId == msg.NpcId);
            account.SetStatus(Status.Speaking);
        }

        [MessageHandler(typeof(NpcDialogQuestionMessage))]
        public static void NpcDialogQuestionMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            NpcDialogQuestionMessage msg = (NpcDialogQuestionMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            account.Npc.QuestionId = (int)msg.MessageId;
            int mess = (int)GameData.GetDataObject(D2oFileEnum.NpcMessages, account.Npc.QuestionId).Fields["messageId"];
            account.Log(new BotTextInformation("Dialogue : " + MageBot.DataFiles.Data.I18n.I18N.GetText(mess)), 0);
            if (account.Npc.QuestionId == 318 && (int)msg.VisibleReplies[0] == 259)
            {
                //Bank
                account.Npc.SendReply(259);
                return;
            }
            else if (account.Npc.QuestionId == 318)
            {
                account.Log(new ErrorTextInformation("Vous n'êtes pas level 10, vous ne pouvez pas utiliser la banque. Fermeture du dialogue."), 0);
                account.Npc.CloseDialog();
            }
            if (msg.VisibleReplies.Count == 0)
                account.Npc.CloseDialog();
            account.Npc.Replies.Clear();
            account.Npc.Replies = msg.VisibleReplies.Select((id) => new MageBot.Core.Npc.NpcReply(account.MapData.Npcs.Find(n => (int)n.ContextualId == account.Npc.Id).NpcId, (int)id)).ToList();
            if (account.Path != null)
            {
                account.Path.SearchReplies(MageBot.DataFiles.Data.I18n.I18N.GetText(mess));
            }
        }

    }
}
