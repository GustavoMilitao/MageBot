using MageBot.Util.IO;
using MageBot.Protocol.Messages.Game.Context.Roleplay.Job;
using MageBot.Protocol.Types.Game.Context.Roleplay.Job;
using MageBot.Protocol.Types.Game.Interactive.Skill;
using MageBot.Protocol.Messages;
using System.Collections.Generic;

namespace MageBot.Core.Engine.Handlers.Job
{
    class JobHandler
    {
         #region Public methods
        [MessageHandler(typeof(JobDescriptionMessage))]
        public static void JobDescriptionMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            JobDescriptionMessage msg = (JobDescriptionMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }

            foreach (JobDescription job in msg.JobsDescription)
            {
                List<int> skills = new List<int>();
                foreach (SkillActionDescription s in job.Skills)
                {
                    skills.Add(s.SkillId);
                }
                MageBot.Core.Job.Job j = new MageBot.Core.Job.Job(job.JobId, skills, account);
                account.Jobs.Add(j);
            }
        }

        [MessageHandler(typeof(JobExperienceMultiUpdateMessage))]
        public static void JobExperienceMultiUpdateMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            JobExperienceMultiUpdateMessage msg = (JobExperienceMultiUpdateMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            foreach (JobExperience i in msg.ExperiencesUpdate)
            {
                foreach (MageBot.Core.Job.Job j in account.Jobs)
                {
                    if (i.JobId == j.Id)
                    {
                        j.Level = i.JobLevel;
                        j.XP = (int)i.JobXP;
                        j.XpLevelFloor =(int)i.JobXpLevelFloor;
                        j.XpNextLevelFloor = (int)i.JobXpNextLevelFloor;
                        break;
                    }
                }
            }
            //account.ActualizeJobs();
            // TODO Militão: Populate the new interface
        }

        [MessageHandler(typeof(JobExperienceUpdateMessage))]
        public static void JobExperienceUpdateMessageTreatment(Message message, byte[] packetDatas, MageBot.Core.Account.Account account)
        {
            JobExperienceUpdateMessage msg = (JobExperienceUpdateMessage)message;

            using (BigEndianReader reader = new BigEndianReader(packetDatas))
            {
                msg.Deserialize(reader);
            }
            JobExperience i = msg.ExperiencesUpdate;
            foreach (MageBot.Core.Job.Job j in account.Jobs)
            {
                if (i.JobId == j.Id)
                {
                    j.Level = i.JobLevel;
                    j.XP = (int)i.JobXP;
                    j.XpLevelFloor = (int)i.JobXpLevelFloor;
                    j.XpNextLevelFloor = (int)i.JobXpNextLevelFloor;
                    break;
                }
            }
            //foreach (JobUC j in account.JobsUC)
            //    j.UpdateJob();
            // TODO Militão: Populate the new interface

        }

        #endregion
    }
}
