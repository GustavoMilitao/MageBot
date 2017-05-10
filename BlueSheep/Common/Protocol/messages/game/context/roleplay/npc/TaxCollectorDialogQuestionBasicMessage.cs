//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Context.Roleplay.Npc
{
    using BlueSheep.Common.Protocol.Types.Game.Context.Roleplay;
    using BlueSheep.Common;


    public class TaxCollectorDialogQuestionBasicMessage : Message
    {
        
        public const int ProtocolId = 5619;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private BasicGuildInformations m_guildInfo;
        
        public virtual BasicGuildInformations GuildInfo
        {
            get
            {
                return m_guildInfo;
            }
            set
            {
                m_guildInfo = value;
            }
        }
        
        public TaxCollectorDialogQuestionBasicMessage(BasicGuildInformations guildInfo)
        {
            m_guildInfo = guildInfo;
        }
        
        public TaxCollectorDialogQuestionBasicMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            m_guildInfo.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_guildInfo = new BasicGuildInformations();
            m_guildInfo.Deserialize(reader);
        }
    }
}
