









// Generated on 12/11/2014 19:01:14
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ServerStatusUpdateMessage : Message
    {
        public new const uint ID =50;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.GameServerInformations server;
        
        public ServerStatusUpdateMessage()
        {
        }
        
        public ServerStatusUpdateMessage(Types.GameServerInformations server)
        {
            this.server = server;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            server.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            server = new Types.GameServerInformations();
            server.Deserialize(reader);
        }
        
    }
    
}