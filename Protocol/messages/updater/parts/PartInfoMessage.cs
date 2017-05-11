//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Updater.Parts
{
    using BlueSheep.Protocol.Types.Updater;
    using BlueSheep.Protocol;


    public class PartInfoMessage : Message
    {
        
        public const int ProtocolId = 1508;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ContentPart m_part;
        
        public virtual ContentPart Part
        {
            get
            {
                return m_part;
            }
            set
            {
                m_part = value;
            }
        }
        
        private float m_installationPercent;
        
        public virtual float InstallationPercent
        {
            get
            {
                return m_installationPercent;
            }
            set
            {
                m_installationPercent = value;
            }
        }
        
        public PartInfoMessage(ContentPart part, float installationPercent)
        {
            m_part = part;
            m_installationPercent = installationPercent;
        }
        
        public PartInfoMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            m_part.Serialize(writer);
            writer.WriteFloat(m_installationPercent);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_part = new ContentPart();
            m_part.Deserialize(reader);
            m_installationPercent = reader.ReadFloat();
        }
    }
}
