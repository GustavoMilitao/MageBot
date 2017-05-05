









// Generated on 12/11/2014 19:01:36
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class AbstractPartyEventMessage : AbstractPartyMessage
    {
        public new const uint ID =6273;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public AbstractPartyEventMessage()
        {
        }
        
        public AbstractPartyEventMessage(int partyId)
         : base(partyId)
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