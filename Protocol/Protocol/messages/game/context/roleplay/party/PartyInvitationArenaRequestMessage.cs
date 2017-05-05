









// Generated on 12/11/2014 19:01:37
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PartyInvitationArenaRequestMessage : PartyInvitationRequestMessage
    {
        public new const uint ID =6283;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public PartyInvitationArenaRequestMessage()
        {
        }
        
        public PartyInvitationArenaRequestMessage(string name)
         : base(name)
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