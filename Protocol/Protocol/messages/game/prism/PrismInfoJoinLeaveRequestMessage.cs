









// Generated on 12/11/2014 19:01:58
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PrismInfoJoinLeaveRequestMessage : Message
    {
        public new const uint ID =5844;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool join;
        
        public PrismInfoJoinLeaveRequestMessage()
        {
        }
        
        public PrismInfoJoinLeaveRequestMessage(bool join)
        {
            this.join = join;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteBoolean(join);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            join = reader.ReadBoolean();
        }
        
    }
    
}