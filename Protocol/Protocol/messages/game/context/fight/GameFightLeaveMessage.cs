









// Generated on 12/11/2014 19:01:27
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameFightLeaveMessage : Message
    {
        public new const uint ID =721;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int charId;
        
        public GameFightLeaveMessage()
        {
        }
        
        public GameFightLeaveMessage(int charId)
        {
            this.charId = charId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(charId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            charId = reader.ReadInt();
        }
        
    }
    
}