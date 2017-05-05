









// Generated on 12/11/2014 19:01:34
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class JobCrafterDirectoryListRequestMessage : Message
    {
        public new const uint ID =6047;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte jobId;
        
        public JobCrafterDirectoryListRequestMessage()
        {
        }
        
        public JobCrafterDirectoryListRequestMessage(sbyte jobId)
        {
            this.jobId = jobId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(jobId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            jobId = reader.ReadSByte();
            if (jobId < 0)
                throw new Exception("Forbidden value on jobId = " + jobId + ", it doesn't respect the following condition : jobId < 0");
        }
        
    }
    
}