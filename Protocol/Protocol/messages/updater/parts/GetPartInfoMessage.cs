









// Generated on 12/11/2014 19:02:01
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GetPartInfoMessage : Message
    {
        public new const uint ID =1506;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public string id;
        
        public GetPartInfoMessage()
        {
        }
        
        public GetPartInfoMessage(string id)
        {
            this.id = id;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUTF(id);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            id = reader.ReadUTF();
        }
        
    }
    
}