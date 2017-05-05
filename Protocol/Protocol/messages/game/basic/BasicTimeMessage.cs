









// Generated on 12/11/2014 19:01:22
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class BasicTimeMessage : Message
    {
        public new const uint ID =175;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public double timestamp;
        public short timezoneOffset;
        
        public BasicTimeMessage()
        {
        }
        
        public BasicTimeMessage(double timestamp, short timezoneOffset)
        {
            this.timestamp = timestamp;
            this.timezoneOffset = timezoneOffset;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteDouble(timestamp);
            writer.WriteShort(timezoneOffset);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            timestamp = reader.ReadDouble();
            if (timestamp < 0 || timestamp > 9.007199254740992E15)
                throw new Exception("Forbidden value on timestamp = " + timestamp + ", it doesn't respect the following condition : timestamp < 0 || timestamp > 9.007199254740992E15");
            timezoneOffset = reader.ReadShort();
        }
        
    }
    
}