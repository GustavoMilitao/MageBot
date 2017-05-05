









// Generated on 12/11/2014 19:01:29
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameFightTurnReadyRequestMessage : Message
    {
        public new const uint ID =715;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int id;
        
        public GameFightTurnReadyRequestMessage()
        {
        }
        
        public GameFightTurnReadyRequestMessage(int id)
        {
            this.id = id;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(id);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            id = reader.ReadInt();
        }
        
    }
    
}