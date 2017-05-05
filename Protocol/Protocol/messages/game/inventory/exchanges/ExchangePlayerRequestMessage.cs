









// Generated on 12/11/2014 19:01:51
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangePlayerRequestMessage : ExchangeRequestMessage
    {
        public new const uint ID =5773;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int target;
        
        public ExchangePlayerRequestMessage()
        {
        }
        
        public ExchangePlayerRequestMessage(sbyte exchangeType, int target)
         : base(exchangeType)
        {
            this.target = target;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt(target);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            target = reader.ReadVarInt();
            if (target < 0)
                throw new Exception("Forbidden value on target = " + target + ", it doesn't respect the following condition : target < 0");
        }
        
    }
    
}