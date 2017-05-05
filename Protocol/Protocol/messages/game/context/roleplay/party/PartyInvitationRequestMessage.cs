









// Generated on 12/11/2014 19:01:38
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PartyInvitationRequestMessage : Message
    {
        public new const uint ID =5585;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public string name;
        
        public PartyInvitationRequestMessage()
        {
        }
        
        public PartyInvitationRequestMessage(string name)
        {
            this.name = name;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUTF(name);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            name = reader.ReadUTF();
        }
        
    }
    
}