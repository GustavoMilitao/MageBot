//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Connection
{
    using BlueSheep.Protocol.Types.Connection;
    using BlueSheep.Protocol;


    public class ServerStatusUpdateMessage : Message
    {
        
        public const int ProtocolId = 50;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private GameServerInformations m_server;
        
        public virtual GameServerInformations Server
        {
            get
            {
                return m_server;
            }
            set
            {
                m_server = value;
            }
        }
        
        public ServerStatusUpdateMessage(GameServerInformations server)
        {
            m_server = server;
        }
        
        public ServerStatusUpdateMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            m_server.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_server = new GameServerInformations();
            m_server.Deserialize(reader);
        }
    }
}
