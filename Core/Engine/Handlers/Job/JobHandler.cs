using BlueSheep.Util.IO;
using System.Collections.Generic;
using BotForgeAPI.Protocol.Messages;
using BotForgeAPI.Network.Messages;
using Core.Engine.Types;
using BotForgeAPI.Protocol.Types;
using System.Linq;

namespace BlueSheep.Engine.Handlers.Job
{
    class JobHandler
    {
         #region Public methods
        [MessageHandler(typeof(JobDescriptionMessage))]
        public static void JobDescriptionMessageTreatment(Message message,  Account account)
        {
            JobDescriptionMessage msg = (JobDescriptionMessage)message;

            foreach (JobDescription job in msg.JobsDescription)
            {
                BotForge.Core.Game.Job.Job j = new BotForge.Core.Game.Job.Job(job.JobId, job.Skills.ToList());
                account.Game.Character.Jobs.Add(j);
            }
        }

        [MessageHandler(typeof(JobExperienceMultiUpdateMessage))]
        public static void JobExperienceMultiUpdateMessageTreatment(Message message,  Account account)
        {
            JobExperienceMultiUpdateMessage msg = (JobExperienceMultiUpdateMessage)message;

            foreach (JobExperience i in msg.ExperiencesUpdate)
            {
                foreach (BotForge.Core.Game.Job.Job j in account.Game.Character.Jobs)
                {
                    if (i.JobId == j.Id)
                    {
                        j.Level = i.JobLevel;
                        j.Xp = i.JobXP;
                        j.LevelFloor = i.JobXpLevelFloor;
                        j.NextXp = i.JobXpNextLevelFloor;
                        break;
                    }
                }
            }
            //account.ActualizeJobs();
            // TODO Militão: Populate the new interface
        }

        [MessageHandler(typeof(JobExperienceUpdateMessage))]
        public static void JobExperienceUpdateMessageTreatment(Message message,  Account account)
        {
            JobExperienceUpdateMessage msg = (JobExperienceUpdateMessage)message;
            JobExperience i = msg.ExperiencesUpdate;
            foreach (BotForge.Core.Game.Job.Job j in account.Game.Character.Jobs)
            {
                if (i.JobId == j.Id)
                {
                    j.Level = i.JobLevel;
                    j.Xp = i.JobXP;
                    j.LevelFloor = i.JobXpLevelFloor;
                    j.NextXp = i.JobXpNextLevelFloor;
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
