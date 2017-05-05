









// Generated on 12/11/2014 19:01:38
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PartyLoyaltyStatusMessage : AbstractPartyMessage
    {
        public new const uint ID =6270;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool loyal;
        
        public PartyLoyaltyStatusMessage()
        {
        }
        
        public PartyLoyaltyStatusMessage(int partyId, bool loyal)
         : base(partyId)
        {
            this.loyal = loyal;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(loyal);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            loyal = reader.ReadBoolean();
        }
        
    }
    
}