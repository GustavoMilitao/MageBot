









// Generated on 12/11/2014 19:01:27
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameFightJoinRequestMessage : Message
    {
        public new const uint ID =701;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int fighterId;
        public int fightId;
        
        public GameFightJoinRequestMessage()
        {
        }
        
        public GameFightJoinRequestMessage(int fighterId, int fightId)
        {
            this.fighterId = fighterId;
            this.fightId = fightId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(fighterId);
            writer.WriteInt(fightId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            fighterId = reader.ReadInt();
            fightId = reader.ReadInt();
        }
        
    }
    
}