using BlueSheep.Common.IO;
using BlueSheep.Common.Protocol.Messages.Game.Interactive;
using BlueSheep.Common.Protocol.Types.Game.Interactive;
using BlueSheep.Engine.Types;
using BlueSheep.Interface;

namespace BlueSheep.Core.Storage
{
    class Opening
    {
        #region Public methods
        public void Init(AccountUC account)
        {
            int skillUID = 0;

            foreach (InteractiveElementSkill skill in account.Safe.EnabledSkills)
            {
                if (skill.SkillId == 104)
                {
                    skillUID = skill.SkillInstanceUid;

                    break;
                }
            }

            InteractiveUseRequestMessage interactiveUseRequestMessage = new InteractiveUseRequestMessage(
                (uint)account.Safe.ElementId,
                (uint)skillUID);

            using (BigEndianWriter writer = new BigEndianWriter())
            {
                interactiveUseRequestMessage.Serialize(writer);
                writer.Content = account.HumanCheck.hash_function(writer.Content);
                MessagePackaging messagePackaging = new MessagePackaging(writer);

                messagePackaging.Pack((int)interactiveUseRequestMessage.MessageID);

                account.SocketManager.Send(messagePackaging.Writer.Content);
                account.LastPacketID.Clear();
                if (account.DebugMode.Checked)
                    account.Log(new BlueSheep.Interface.Text.DebugTextInformation("[SND] 5001 (InteractiveUseRequestMessage)"), 0);
            }
        }
        #endregion
    }
}
