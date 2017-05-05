









// Generated on 12/11/2014 19:01:34
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class JobExperienceUpdateMessage : Message
    {
        public new const uint ID =5654;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.JobExperience experiencesUpdate;
        
        public JobExperienceUpdateMessage()
        {
        }
        
        public JobExperienceUpdateMessage(Types.JobExperience experiencesUpdate)
        {
            this.experiencesUpdate = experiencesUpdate;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            experiencesUpdate.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            experiencesUpdate = new Types.JobExperience();
            experiencesUpdate.Deserialize(reader);
        }
        
    }
    
}