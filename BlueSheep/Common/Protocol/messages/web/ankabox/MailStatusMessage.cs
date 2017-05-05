









// Generated on 12/11/2014 19:02:01
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class MailStatusMessage : Message
    {
        public new const uint ID =6275;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int unread;
        public int total;
        
        public MailStatusMessage()
        {
        }
        
        public MailStatusMessage(int unread, int total)
        {
            this.unread = unread;
            this.total = total;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)unread);
            writer.WriteVarShort((short)total);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            unread = reader.ReadVarUhShort();
            if (unread < 0)
                throw new Exception("Forbidden value on unread = " + unread + ", it doesn't respect the following condition : unread < 0");
            total = reader.ReadVarUhShort();
            if (total < 0)
                throw new Exception("Forbidden value on total = " + total + ", it doesn't respect the following condition : total < 0");
        }
        
    }
    
}