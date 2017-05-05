









// Generated on 12/11/2014 19:01:47
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class TeleportBuddiesAnswerMessage : Message
    {
        public new const uint ID =6294;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool accept;
        
        public TeleportBuddiesAnswerMessage()
        {
        }
        
        public TeleportBuddiesAnswerMessage(bool accept)
        {
            this.accept = accept;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteBoolean(accept);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            accept = reader.ReadBoolean();
        }
        
    }
    
}