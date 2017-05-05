


















// Generated on 12/11/2014 19:02:07
using System;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class JobDescription
    {

        public new const short ID = 101;
        public virtual short TypeId
        {
            get { return ID; }
        }

        public uint jobId;
        public SkillActionDescription[] skills;


        public JobDescription()
        {
        }

        public JobDescription(uint jobId, SkillActionDescription[] skills)
        {
            this.jobId = jobId;
            this.skills = skills;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteUInt(jobId);
            writer.WriteUShort((ushort)skills.Length);
            foreach (var entry in skills)
            {
                writer.WriteShort(entry.TypeId);
                entry.Serialize(writer);
            }


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            jobId = reader.ReadByte();
            if (jobId < 0)
                throw new Exception("Forbidden value on jobId = " + jobId + ", it doesn't respect the following condition : jobId < 0");
            uint limit = reader.ReadUShort();
            skills = new SkillActionDescription[limit];
            for (int i = 0; i < limit; i++)
            {
                skills[i] = Types.ProtocolTypeManager.GetInstance<SkillActionDescription>(reader.ReadUShort());
                skills[i].Deserialize(reader);
            }


        }


    }


}