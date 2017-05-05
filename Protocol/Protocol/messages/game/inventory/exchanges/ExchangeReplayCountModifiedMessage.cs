









// Generated on 12/11/2014 19:01:51
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeReplayCountModifiedMessage : Message
    {
        public new const uint ID =6023;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int count;
        
        public ExchangeReplayCountModifiedMessage()
        {
        }
        
        public ExchangeReplayCountModifiedMessage(int count)
        {
            this.count = count;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarInt(count);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            count = reader.ReadVarInt();
        }
        
    }
    
}