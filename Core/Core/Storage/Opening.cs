using BlueSheep.Util.IO;
using BlueSheep.Protocol.Messages.Game.Interactive;
using BlueSheep.Protocol.Types.Game.Interactive;
using BlueSheep.Engine.Types;
using BlueSheep.Util.Text.Log;

namespace BlueSheep.Core.Storage
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
                writer.Content = account.HumanCheck.hash_function(writer.Content);

                interactiveUseRequestMessage.Pack(writer);

                account.SocketManager.Send(writer.Content);
                account.LastPacketID.Clear();
                account.Log(new DebugTextInformation("[SND] 5001 (InteractiveUseRequestMessage)"), 0);
            }
        }
        #endregion
    }
}
