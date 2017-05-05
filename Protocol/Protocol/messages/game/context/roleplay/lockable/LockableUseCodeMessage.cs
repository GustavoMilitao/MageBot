









// Generated on 12/11/2014 19:01:35
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class LockableUseCodeMessage : Message
    {
        public new const uint ID =5667;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public string code;
        
        public LockableUseCodeMessage()
        {
        }
        
        public LockableUseCodeMessage(string code)
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