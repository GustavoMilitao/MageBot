









// Generated on 12/11/2014 19:01:39
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PartyModifiableStatusMessage : AbstractPartyMessage
    {
        public new const uint ID =6277;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool enabled;
        
        public PartyModifiableStatusMessage()
        {
        }
        
        public PartyModifiableStatusMessage(int partyId, bool enabled)
         : base(partyId)
        {
            this.enabled = enabled;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(enabled);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            enabled = reader.ReadBoolean();
        }
        
    }
    
}