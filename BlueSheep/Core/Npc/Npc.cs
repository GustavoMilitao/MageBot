using BlueSheep.Common.Data.D2o;
using BlueSheep.Common.IO;
using BlueSheep.Common.Protocol.Messages;
using BlueSheep.Common.Protocol.Messages.Game.Context.Roleplay.Npc;
using BlueSheep.Common.Protocol.Messages.Game.Dialog;
using BlueSheep.Common.Protocol.Types.Game.Context.Roleplay;
using BlueSheep.Common.Types;
using BlueSheep.Engine.Types;
using BlueSheep.Interface;
using BlueSheep.Interface.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueSheep.Core.Npc
{
    public class Npc
    {
        #region Fields
        public AccountUC account { get; set; }
        public GameRolePlayNpcInformations Entity { get; set; }
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText
        {
            get { return BlueSheep.Common.Data.I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.NpcMessages, QuestionId).Fields["messageId"]); }
        }

        public List<NpcReply> Replies { get; set; }
        #endregion

        #region Constructors
        public Npc(AccountUC Account)
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
            return BlueSheep.Common.Data.I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Npcs, npcId).Fields["nameId"]);
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
                writer.Content = account.HumanCheck.hash_function(writer.Content);
                MessagePackaging pack = new MessagePackaging(writer);
                pack.Pack((int)msg.MessageID);
                account.SocketManager.Send(pack.Writer.Content);
                if (account.DebugMode.Checked)
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
