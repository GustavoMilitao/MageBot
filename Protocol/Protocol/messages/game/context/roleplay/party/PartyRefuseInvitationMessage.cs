









// Generated on 12/11/2014 19:01:39
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PartyRefuseInvitationMessage : AbstractPartyMessage
    {
        public new const uint ID =5582;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public PartyRefuseInvitationMessage()
        {
        }
        
        public PartyRefuseInvitationMessage(int partyId)
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