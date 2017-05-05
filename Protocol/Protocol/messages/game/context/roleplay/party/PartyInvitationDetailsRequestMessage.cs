









// Generated on 12/11/2014 19:01:37
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PartyInvitationDetailsRequestMessage : AbstractPartyMessage
    {
        public new const uint ID =6264;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public PartyInvitationDetailsRequestMessage()
        {
        }
        
        public PartyInvitationDetailsRequestMessage(int partyId)
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