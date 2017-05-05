









// Generated on 12/11/2014 19:01:28
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameFightSpectatePlayerRequestMessage : Message
    {
        public new const uint ID =6474;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int playerId;
        
        public GameFightSpectatePlayerRequestMessage()
        {
        }
        
        public GameFightSpectatePlayerRequestMessage(int playerId)
        {
            this.playerId = playerId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(playerId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            playerId = reader.ReadInt();
        }
        
    }
    
}