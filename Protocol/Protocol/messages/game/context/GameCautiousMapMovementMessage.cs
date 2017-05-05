









// Generated on 12/11/2014 19:01:26
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameCautiousMapMovementMessage : GameMapMovementMessage
    {
        public new const uint ID =6497;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public GameCautiousMapMovementMessage()
        {
        }
        
        public GameCautiousMapMovementMessage(short[] keyMovements, int actorId)
         : base(keyMovements, actorId)
        {
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
        }
        
    }
    
}