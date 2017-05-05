









// Generated on 12/11/2014 19:01:31
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class NotificationUpdateFlagMessage : Message
    {
        public new const uint ID =6090;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int index;
        
        public NotificationUpdateFlagMessage()
        {
        }
        
        public NotificationUpdateFlagMessage(int index)
        {
            this.index = index;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)index);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            index = reader.ReadVarUhShort();
            if (index < 0)
                throw new Exception("Forbidden value on index = " + index + ", it doesn't respect the following condition : index < 0");
        }
        
    }
    
}