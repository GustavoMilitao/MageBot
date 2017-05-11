//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Connection.Search
{
    using System.Collections.Generic;
    using BlueSheep.Protocol;


    public class AcquaintanceServerListMessage : Message
    {
        
        public const int ProtocolId = 6142;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<System.UInt16> m_servers;
        
        public virtual List<System.UInt16> Servers
        {
            get
            {
                return m_servers;
            }
            set
            {
                m_servers = value;
            }
        }
        
        public AcquaintanceServerListMessage(List<System.UInt16> servers)
        {
            m_servers = servers;
        }
        
        public AcquaintanceServerListMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(m_servers.Count)));
            int serversIndex;
            for (serversIndex = 0; (serversIndex < m_servers.Count); serversIndex = (serversIndex + 1))
            {
                writer.WriteVarShort(m_servers[serversIndex]);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            int serversCount = reader.ReadUShort();
            int serversIndex;
            m_servers = new System.Collections.Generic.List<ushort>();
            for (serversIndex = 0; (serversIndex < serversCount); serversIndex = (serversIndex + 1))
            {
                m_servers.Add(reader.ReadVarUhShort());
            }
        }
    }
}
