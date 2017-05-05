









// Generated on 12/11/2014 19:01:48
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeBidHouseListMessage : Message
    {
        public new const uint ID =5807;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public short id;
        
        public ExchangeBidHouseListMessage()
        {
        }
        
        public ExchangeBidHouseListMessage(short id)
        {
            this.id = id;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort(id);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            id = reader.ReadVarShort();
            if (id < 0)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0");
        }
        
    }
    
}