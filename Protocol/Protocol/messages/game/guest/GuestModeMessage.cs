









// Generated on 12/11/2014 19:01:43
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GuestModeMessage : Message
    {
        public new const uint ID =6505;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool active;
        
        public GuestModeMessage()
        {
        }
        
        public GuestModeMessage(bool active)
        {
            this.active = active;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteBoolean(active);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            active = reader.ReadBoolean();
        }
        
    }
    
}