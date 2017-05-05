using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class JobBookSubscription
    {
        public const int ID = 500;

        public uint jobId = 0;

        public bool subscribed = false;

        public JobBookSubscription()
        {
        }


        public void Serialize(BigEndianWriter writer)
        {
            if (jobId < 0)
            {
                throw new Exception("Forbidden value (" + jobId + ") on element jobId.");
            }
            writer.WriteByte((byte)jobId);
            writer.WriteBoolean(subscribed);
        }

        public void Deserialize(BigEndianReader reader)
        {
            _jobIdFunc(reader);
            _subscribedFunc(reader);
        }

        private void _jobIdFunc(BigEndianReader reader)
        {
            jobId = reader.ReadByte();
            if (jobId < 0)
            {
                throw new Exception("Forbidden value (" + jobId + ") on element of JobBookSubscription.jobId.");
            }
        }

        private void _subscribedFunc(BigEndianReader reader)
        {
            subscribed = reader.ReadBoolean();
        }

    }
}