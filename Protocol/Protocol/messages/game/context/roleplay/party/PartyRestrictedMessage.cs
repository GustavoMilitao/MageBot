









// Generated on 12/11/2014 19:01:40
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PartyRestrictedMessage : AbstractPartyMessage
    {
        public new const uint ID =6175;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool restricted;
        
        public PartyRestrictedMessage()
        {
        }
        
        public PartyRestrictedMessage(int partyId, bool restricted)
         : base(partyId)
        {
            this.restricted = restricted;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(restricted);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            restricted = reader.ReadBoolean();
        }
        
    }
    
}