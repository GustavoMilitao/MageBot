









// Generated on 12/11/2014 19:01:37
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PartyCancelInvitationMessage : AbstractPartyMessage
    {
        public new const uint ID =6254;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int guestId;
        
        public PartyCancelInvitationMessage()
        {
        }
        
        public PartyCancelInvitationMessage(int partyId, int guestId)
         : base(partyId)
        {
            this.guestId = guestId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt(guestId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            guestId = reader.ReadVarInt();
            if (guestId < 0)
                throw new Exception("Forbidden value on guestId = " + guestId + ", it doesn't respect the following condition : guestId < 0");
        }
        
    }
    
}