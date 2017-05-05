









// Generated on 12/11/2014 19:01:28
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameFightPlacementSwapPositionsErrorMessage : Message
    {
        public new const uint ID =6548;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public GameFightPlacementSwapPositionsErrorMessage()
        {
        }
        
        
        public override void Serialize(BigEndianWriter writer)
        {
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
        }
        
    }
    
}