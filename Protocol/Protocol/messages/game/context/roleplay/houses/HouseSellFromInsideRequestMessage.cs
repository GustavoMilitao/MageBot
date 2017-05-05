









// Generated on 12/11/2014 19:01:33
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class HouseSellFromInsideRequestMessage : HouseSellRequestMessage
    {
        public new const uint ID =5884;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public HouseSellFromInsideRequestMessage()
        {
        }
        
        public HouseSellFromInsideRequestMessage(int amount)
         : base(amount)
        {
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
        }
        
    }
    
}