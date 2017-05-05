









// Generated on 12/11/2014 19:01:24
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ChatAbstractClientMessage : Message
    {
        public new const uint ID =850;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public string content;
        
        public ChatAbstractClientMessage()
        {
        }
        
        public ChatAbstractClientMessage(string content)
        {
            this.content = content;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUTF(content);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            content = reader.ReadUTF();
        }
        
    }
    
}