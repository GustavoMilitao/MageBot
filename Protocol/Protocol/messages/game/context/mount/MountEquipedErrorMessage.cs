









// Generated on 12/11/2014 19:01:30
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class MountEquipedErrorMessage : Message
    {
        public new const uint ID =5963;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte errorType;
        
        public MountEquipedErrorMessage()
        {
        }
        
        public MountEquipedErrorMessage(sbyte errorType)
        {
            this.errorType = errorType;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(errorType);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            errorType = reader.ReadSByte();
            if (errorType < 0)
                throw new Exception("Forbidden value on errorType = " + errorType + ", it doesn't respect the following condition : errorType < 0");
        }
        
    }
    
}