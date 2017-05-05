









// Generated on 12/11/2014 19:01:49
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeIsReadyMessage : Message
    {
        public new const uint ID =5509;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public ulong id;
        public bool ready;
        
        public ExchangeIsReadyMessage()
        {
        }
        
        public ExchangeIsReadyMessage(ulong id, bool ready)
        {
            this.id = id;
            this.ready = ready;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarLong(id);
            writer.WriteBoolean(ready);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            id = reader.ReadVarUhLong();
            if (id < 0)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0");
            ready = reader.ReadBoolean();
        }
        
    }
    
}