









// Generated on 12/11/2014 19:01:32
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameRolePlayPlayerLifeStatusMessage : Message
    {
        public new const uint ID =5996;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte state;
        
        public GameRolePlayPlayerLifeStatusMessage()
        {
        }
        
        public GameRolePlayPlayerLifeStatusMessage(sbyte state)
        {
            this.state = state;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(state);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            state = reader.ReadSByte();
            if (state < 0)
                throw new Exception("Forbidden value on state = " + state + ", it doesn't respect the following condition : state < 0");
        }
        
    }
    
}