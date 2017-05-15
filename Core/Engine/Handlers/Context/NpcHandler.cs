using BlueSheep.Common.Data.D2o;
using BlueSheep.Util.IO;
using BlueSheep.Util.Text.Log;
using System.Linq;
using BotForgeAPI.Protocol.Messages;
using BotForgeAPI.Network.Messages;
using Core.Engine.Types;
using BotForgeAPI.Game.Map;
using BotForge.Core.Game.Map.Actors;
using BotForge.Core.Game.Path;
using BotForgeAPI.Protocol.Types;
using BotForge.Core.Game.Map;

namespace BlueSheep.Engine.Handlers.Context
{
    class NpcHandler
    {
        [MessageHandler(typeof(ExchangeStartOkNpcShopMessage))]
        public static void GameContextRemoveElementMessageTreatment(Message message, Account account)
        {
            ExchangeStartOkNpcShopMessage msg = (ExchangeStartOkNpcShopMessage)message;

            account.Game.Character.SetStatus(Status.Exchanging);
        }

        [MessageHandler(typeof(LeaveDialogMessage))]
        public static void NpcDialogCreationMessageTreatment(Message message, Account account)
        {
            LeaveDialogMessage msg = (LeaveDialogMessage)message;


            account.Game.Character.SetStatus(Status.None);
        }

        [MessageHandler(typeof(NpcDialogCreationMessage))]
        public static void LeaveDialogMessageTreatment(Message message, Account account)
        {
            NpcDialogCreationMessage msg = (NpcDialogCreationMessage)message;

            if (account.Game.Map.Data.Id == msg.MapId)
            {
                if (account.Game.Map.Data.Npcs.Any(npc => npc.Id == msg.NpcId))
                {
                    account.TalkingNPC = account.Game.Map.Data.Npcs.FirstOrDefault(npc => npc.Id == msg.NpcId);
                    account.Game.Character.SetStatus(Status.Speaking);
                }
            }
        }

        //[MessageHandler(typeof(NpcDialogQuestionMessage))]
        //public static void NpcDialogQuestionMessageTreatment(Message message, Account account)
        //{
        //    NpcDialogQuestionMessage msg = (NpcDialogQuestionMessage)message;
        //    //(account.TalkingNPC as Npc).QuestionId = (int)msg.MessageId;
        //    int mess = (int)GameData.GetDataObject(D2oFileEnum.NpcMessages, (int)msg.MessageId).Fields["messageId"];
        //    //TODO Militão 2.0: Check if on create npc all questions are created
        //    account.Logger.Log("Dialogue : " + BlueSheep.Common.Data.I18N.GetText(mess), BotForgeAPI.Logger.LogEnum.Bot);
        //    if (msg.MessageId == 318 && (int)msg.VisibleReplies[0] == 259)
        //    {
        //        //Bank
        //        account.Game.Map.SendReply(259);
        //        return;
        //    }
        //    else if (msg.MessageId == 318)
        //    {
        //        account.Logger.Log("You need lvl 10 to make a dialog with NPCS", BotForgeAPI.Logger.LogEnum.TextInformationError);
        //        account.Game.Map.CloseDialog();
        //    }
        //    if (msg.VisibleReplies.Count() == 0)
        //        account.Game.Map.CloseDialog();
        //    account.TalkingNPC.Replies.Clear();
        //    msg.VisibleReplies.ToList().ForEach((id) => account.TalkingNPC.Replies.Add((int)id, Common.Data.I18N.GetText((int)id)));
        //    if (account.Path != null)
        //    {
        //        (account.Path as Path).SearchReply(Common.Data.I18N.GetText(mess));
        //    }
        //}
        //TODO Militão 2.0: Check if it is necessary


        [MessageHandler(typeof(GameRolePlayNpcInformations))]
        public static void GameRolePlayNpcWithQuestMessageTreatment(GameRolePlayNpcInformations type, Account account)
        {
            account.Game.Map.Data.Npcs.Add(new Npc(type,(MapData)account.Game.Map.Data));
        }

        [MessageHandler(typeof(GameRolePlayNpcWithQuestInformations))]
        public static void GameRolePlayNpcWithQuestInformationsTreatment(GameRolePlayNpcWithQuestInformations type, Account account)
        {
            account.Game.Map.Data.Npcs.Add(new Npc(type, (MapData)account.Game.Map.Data));
        }
    }
}
