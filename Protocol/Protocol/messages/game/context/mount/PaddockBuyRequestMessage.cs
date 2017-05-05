









// Generated on 12/11/2014 19:01:30
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PaddockBuyRequestMessage : Message
    {
        public new const uint ID =5951;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int proposedPrice;
        
        public PaddockBuyRequestMessage()
        {
        }
        
        public PaddockBuyRequestMessage(int proposedPrice)
        {
            this.proposedPrice = proposedPrice;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarInt(proposedPrice);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            proposedPrice = reader.ReadVarInt();
            if (proposedPrice < 0)
                throw new Exception("Forbidden value on proposedPrice = " + proposedPrice + ", it doesn't respect the following condition : proposedPrice < 0");
        }
        
    }
    
}