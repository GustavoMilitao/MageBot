using MageBot.DataFiles.Data.D2o;
using MageBot.Util.IO;
using MageBot.Protocol.Messages.Game.Context.Roleplay.Npc;
using MageBot.Protocol.Messages.Game.Dialog;
using MageBot.Protocol.Types.Game.Context.Roleplay;
using Util.Util.Text.Log;
using System.Collections.Generic;

namespace MageBot.Core.Npc
{
    public class Npc
    {
        #region Fields
        public Account.Account account { get; set; }
        public GameRolePlayNpcInformations Entity { get; set; }
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText
        {
            get { return MageBot.DataFiles.Data.I18n.I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.NpcMessages, QuestionId).Fields["messageId"]); }
        }

        public List<NpcReply> Replies { get; set; }
        #endregion

        #region Constructors
        public Npc(Account.Account Account)
        {
            account = Account;
            Entity = null;
            Replies = new List<NpcReply>();
        }
        #endregion

        #region Public Methods
        public void CloseDialog()
        {
            Replies.Clear();
            QuestionId = 0;
            Id = 0;

            LeaveDialogRequestMessage msg = new LeaveDialogRequestMessage();
            account.SocketManager.Send(msg);

        }

        public void SendReply(int replyId)
        {
            NpcDialogReplyMessage msg = new NpcDialogReplyMessage((uint)replyId);
            account.SocketManager.Send(msg);
        }

        public string GetNpcName(int npcId)
        {
            return MageBot.DataFiles.Data.I18n.I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Npcs, npcId).Fields["nameId"]);
        }

        public void TalkToNpc(int npcId)
        {
            npcId = FindContextIdFromNpcId(npcId);
            if (npcId == 0)
                return;
            account.Busy = true;
            using (BigEndianWriter writer = new BigEndianWriter())
            {
                NpcGenericActionRequestMessage msg = new NpcGenericActionRequestMessage(npcId, 3, account.MapData.Id);
                msg.Serialize(writer);
                writer.Content = account.HumanCheck.Hash_function(writer.Content);
                msg.Pack(writer);
                account.SocketManager.Send(writer.Content);
                account.Log(new DebugTextInformation("[SND] 5898 (NpcGenericActionRequestMessage)"), 0);
            }
        }

        public int FindContextIdFromNpcId(int npcId)
        {
            if (npcId == 0)
                return (int)account.MapData.Npcs[0].ContextualId;
            foreach (GameRolePlayNpcInformations p in account.MapData.Npcs)
            {
                if (p.NpcId == npcId)
                    return (int)p.ContextualId;
            }
            return 0;
        }

        #endregion

    }


}
