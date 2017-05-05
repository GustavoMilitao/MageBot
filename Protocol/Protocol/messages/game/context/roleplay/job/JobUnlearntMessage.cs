









// Generated on 12/11/2014 19:01:35
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class JobUnlearntMessage : Message
    {
        public new const uint ID =5657;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte jobId;
        
        public JobUnlearntMessage()
        {
        }
        
        public JobUnlearntMessage(sbyte jobId)
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