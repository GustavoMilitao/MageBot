









// Generated on 12/11/2014 19:01:50
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeObjectMessage : Message
    {
        public new const uint ID =5515;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool remote;
        
        public ExchangeObjectMessage()
        {
        }
        
        public ExchangeObjectMessage(bool remote)
        {
            this.remote = remote;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteBoolean(remote);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            remote = reader.ReadBoolean();
        }
        
    }
    
}