









// Generated on 12/11/2014 19:01:30
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class MountSetXpRatioRequestMessage : Message
    {
        public new const uint ID =5989;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte xpRatio;
        
        public MountSetXpRatioRequestMessage()
        {
        }
        
        public MountSetXpRatioRequestMessage(sbyte xpRatio)
        {
            this.xpRatio = xpRatio;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(xpRatio);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            xpRatio = reader.ReadSByte();
            if (xpRatio < 0)
                throw new Exception("Forbidden value on xpRatio = " + xpRatio + ", it doesn't respect the following condition : xpRatio < 0");
        }
        
    }
    
}