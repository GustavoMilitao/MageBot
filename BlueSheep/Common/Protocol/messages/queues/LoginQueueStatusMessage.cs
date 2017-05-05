









// Generated on 12/11/2014 19:02:00
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class LoginQueueStatusMessage : Message
    {
        public new const uint ID =10;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int position;
        public int total;
        
        public LoginQueueStatusMessage()
        {
        }
        
        public LoginQueueStatusMessage(int position, int total)
        {
            this.position = position;
            this.total = total;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUShort((ushort)position);
            writer.WriteUShort((ushort)total);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            position = reader.ReadUShort();
            if (position < 0 || position > 65535)
                throw new Exception("Forbidden value on position = " + position + ", it doesn't respect the following condition : position < 0 || position > 65535");
            total = reader.ReadUShort();
            if (total < 0 || total > 65535)
                throw new Exception("Forbidden value on total = " + total + ", it doesn't respect the following condition : total < 0 || total > 65535");
        }
        
    }
    
}