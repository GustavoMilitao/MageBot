//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Atlas
{
    using BlueSheep.Protocol.Types.Game.Context.Roleplay;
    using BlueSheep.Protocol;


    public class AtlasPointInformationsMessage : Message
    {
        
        public const int ProtocolId = 5956;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private AtlasPointsInformations m_type;
        
        public virtual AtlasPointsInformations Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }
        
        public AtlasPointInformationsMessage(AtlasPointsInformations type)
        {
            m_type = type;
        }
        
        public AtlasPointInformationsMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            m_type.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_type = new AtlasPointsInformations();
            m_type.Deserialize(reader);
        }
    }
}
