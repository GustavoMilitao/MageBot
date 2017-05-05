









// Generated on 12/11/2014 19:01:54
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GoldAddedMessage : Message
    {
        public new const uint ID =6030;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.GoldItem gold;
        
        public GoldAddedMessage()
        {
        }
        
        public GoldAddedMessage(Types.GoldItem gold)
        {
            this.gold = gold;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            gold.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            gold = new Types.GoldItem();
            gold.Deserialize(reader);
        }
        
    }
    
}