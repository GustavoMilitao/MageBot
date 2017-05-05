









// Generated on 12/11/2014 19:01:50
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeMountStableAddMessage : Message
    {
        public new const uint ID =5971;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.MountClientData mountDescription;
        
        public ExchangeMountStableAddMessage()
        {
        }
        
        public ExchangeMountStableAddMessage(Types.MountClientData mountDescription)
        {
            this.mountDescription = mountDescription;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            mountDescription.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            mountDescription = new Types.MountClientData();
            mountDescription.Deserialize(reader);
        }
        
    }
    
}