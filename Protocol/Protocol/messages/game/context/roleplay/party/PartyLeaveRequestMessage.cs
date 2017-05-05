









// Generated on 12/11/2014 19:01:38
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PartyLeaveRequestMessage : AbstractPartyMessage
    {
        public new const uint ID =5593;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public PartyLeaveRequestMessage()
        {
        }
        
        public PartyLeaveRequestMessage(int partyId)
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