









// Generated on 12/11/2014 19:01:51
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangePlayerRequestMessage : ExchangeRequestMessage
    {
        public new const uint ID =5773;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public ulong target;
        
        public ExchangePlayerRequestMessage()
        {
        }
        
        public ExchangePlayerRequestMessage(sbyte exchangeType, ulong target)
         : base(exchangeType)
        {
            this.target = target;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarLong(target);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            target = reader.ReadVarUhLong();
            if (target < 0)
                throw new Exception("Forbidden value on target = " + target + ", it doesn't respect the following condition : target < 0");
        }
        
    }
    
}