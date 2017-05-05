









// Generated on 12/11/2014 19:01:15
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class AbstractGameActionWithAckMessage : AbstractGameActionMessage
    {
        public new const uint ID =1001;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int waitAckId;
        
        public AbstractGameActionWithAckMessage()
        {
        }
        
        public AbstractGameActionWithAckMessage(int actionId, ulong sourceId, int waitAckId)
         : base(actionId, sourceId)
        {
            this.waitAckId = waitAckId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)waitAckId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            waitAckId = reader.ReadShort();
        }
        
    }
    
}