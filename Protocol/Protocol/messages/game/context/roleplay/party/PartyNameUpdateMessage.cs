









// Generated on 12/11/2014 19:01:39
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PartyNameUpdateMessage : AbstractPartyMessage
    {
        public new const uint ID =6502;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public string partyName;
        
        public PartyNameUpdateMessage()
        {
        }
        
        public PartyNameUpdateMessage(int partyId, string partyName)
         : base(partyId)
        {
            this.partyName = partyName;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(partyName);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            partyName = reader.ReadUTF();
        }
        
    }
    
}