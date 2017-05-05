









// Generated on 12/11/2014 19:01:35
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class LockableChangeCodeMessage : Message
    {
        public new const uint ID =5666;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public string code;
        
        public LockableChangeCodeMessage()
        {
        }
        
        public LockableChangeCodeMessage(string code)
        {
            this.code = code;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUTF(code);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            code = reader.ReadUTF();
        }
        
    }
    
}