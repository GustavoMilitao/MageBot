//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Guild
{
    using BlueSheep.Protocol.Types.Game.Context.Roleplay;
    using System.Collections.Generic;
    using BlueSheep.Protocol;


    public class GuildListMessage : Message
    {
        
        public const int ProtocolId = 6413;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<GuildInformations> m_guilds;
        
        public virtual List<GuildInformations> Guilds
        {
            get
            {
                return m_guilds;
            }
            set
            {
                m_guilds = value;
            }
        }
        
        public GuildListMessage(List<GuildInformations> guilds)
        {
            m_guilds = guilds;
        }
        
        public GuildListMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(m_guilds.Count)));
            int guildsIndex;
            for (guildsIndex = 0; (guildsIndex < m_guilds.Count); guildsIndex = (guildsIndex + 1))
            {
                GuildInformations objectToSend = m_guilds[guildsIndex];
                objectToSend.Serialize(writer);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            int guildsCount = reader.ReadUShort();
            int guildsIndex;
            m_guilds = new System.Collections.Generic.List<GuildInformations>();
            for (guildsIndex = 0; (guildsIndex < guildsCount); guildsIndex = (guildsIndex + 1))
            {
                GuildInformations objectToAdd = new GuildInformations();
                objectToAdd.Deserialize(reader);
                m_guilds.Add(objectToAdd);
            }
        }
    }
}
