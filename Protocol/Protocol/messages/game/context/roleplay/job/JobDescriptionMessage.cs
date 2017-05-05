









// Generated on 12/11/2014 19:01:34
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class JobDescriptionMessage : Message
    {
        public const uint ID =5655;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.JobDescription[] jobsDescription;
        
        public JobDescriptionMessage()
        {
        }
        
        public JobDescriptionMessage(Types.JobDescription[] jobsDescription)
        {
            this.jobsDescription = jobsDescription;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUShort((ushort)jobsDescription.Length);
            foreach (var entry in jobsDescription)
            {
                 entry.Serialize(writer);
            }
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            uint limit = reader.ReadUShort();
            jobsDescription = new Types.JobDescription[limit];
            for (int i = 0; i < limit; i++)
            {
                 jobsDescription[i] = new Types.JobDescription();
                 jobsDescription[i].Deserialize(reader);
            }
        }
        
    }
    
}