using MageBot.Util.IO;
using MageBot.Protocol.Messages.Game.Interactive;
using MageBot.Protocol.Types.Game.Interactive;
using Util.Util.Text.Log;
using MageBot.Core.Engine.Network;

namespace MageBot.Core.Storage
{
    class Opening
    {
        #region Public methods
        public void Init(Account.Account account)
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
                (uint)account.Safe.Id,
                (uint)skillUID);

            using (BigEndianWriter writer = new BigEndianWriter())
            {
                interactiveUseRequestMessage.Serialize(writer);
                writer.Content = account.HumanCheck.Hash_function(writer.Content);

                MessagePackaging mp = new MessagePackaging(writer);
                mp.Pack(interactiveUseRequestMessage.ProtocolId);

                account.SocketManager.Send(mp.Writer.Content);
                account.LastPacketID.Clear();
                account.Log(new DebugTextInformation("[SND] 5001 (InteractiveUseRequestMessage)"), 0);
            }
        }
        #endregion
    }
}
