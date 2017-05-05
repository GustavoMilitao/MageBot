









// Generated on 12/11/2014 19:01:28
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameFightOptionToggleMessage : Message
    {
        public new const uint ID =707;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte option;
        
        public GameFightOptionToggleMessage()
        {
        }
        
        public GameFightOptionToggleMessage(sbyte option)
        {
            this.option = option;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(option);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            option = reader.ReadSByte();
            if (option < 0)
                throw new Exception("Forbidden value on option = " + option + ", it doesn't respect the following condition : option < 0");
        }
        
    }
    
}