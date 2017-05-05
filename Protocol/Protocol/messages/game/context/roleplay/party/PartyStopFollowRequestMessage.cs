









// Generated on 12/11/2014 19:01:40
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PartyStopFollowRequestMessage : AbstractPartyMessage
    {
        public new const uint ID =5574;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public PartyStopFollowRequestMessage()
        {
        }
        
        public PartyStopFollowRequestMessage(int partyId)
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