//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Guild.Tax
{


    public class GuildFightTakePlaceRequestMessage : GuildFightJoinRequestMessage
    {
        
        public const int ProtocolId = 6235;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private int m_replacedCharacterId;
        
        public virtual int ReplacedCharacterId
        {
            get
            {
                return m_replacedCharacterId;
            }
            set
            {
                m_replacedCharacterId = value;
            }
        }
        
        public GuildFightTakePlaceRequestMessage(int replacedCharacterId)
        {
            m_replacedCharacterId = replacedCharacterId;
        }
        
        public GuildFightTakePlaceRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(m_replacedCharacterId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_replacedCharacterId = reader.ReadInt();
        }
    }
}
