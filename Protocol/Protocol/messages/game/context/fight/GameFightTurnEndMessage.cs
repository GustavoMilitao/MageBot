









// Generated on 12/11/2014 19:01:28
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameFightTurnEndMessage : Message
    {
        public new const uint ID =719;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int id;
        
        public GameFightTurnEndMessage()
        {
        }
        
        public GameFightTurnEndMessage(int id)
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