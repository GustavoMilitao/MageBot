









// Generated on 12/11/2014 19:02:00
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class RawDataMessage : Message
    {
        public new const uint ID =6253;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public RawDataMessage()
        {
        }
        
        
        public override void Serialize(BigEndianWriter writer)
        {
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            var _loc_2 = reader.ReadVarInt();
            //param1.readBytes(this.content, 0, _loc_2);
        }
        
    }
    
}