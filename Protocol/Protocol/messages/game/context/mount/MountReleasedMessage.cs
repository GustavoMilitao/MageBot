









// Generated on 12/11/2014 19:01:30
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class MountReleasedMessage : Message
    {
        public new const uint ID =6308;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public double mountId;
        
        public MountReleasedMessage()
        {
        }
        
        public MountReleasedMessage(double mountId)
        {
            this.mountId = mountId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteDouble(mountId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            mountId = reader.ReadDouble();
            if (mountId < -9.007199254740992E15 || mountId > 9.007199254740992E15)
                throw new Exception("Forbidden value on mountId = " + mountId + ", it doesn't respect the following condition : mountId < -9.007199254740992E15 || mountId > 9.007199254740992E15");
        }
        
    }
    
}