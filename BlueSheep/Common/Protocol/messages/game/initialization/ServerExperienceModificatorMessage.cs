//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Initialization
{
    using BlueSheep.Common;


    public class ServerExperienceModificatorMessage : Message
    {
        
        public const int ProtocolId = 6237;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ushort m_experiencePercent;
        
        public virtual ushort ExperiencePercent
        {
            get
            {
                return m_experiencePercent;
            }
            set
            {
                m_experiencePercent = value;
            }
        }
        
        public ServerExperienceModificatorMessage(ushort experiencePercent)
        {
            m_experiencePercent = experiencePercent;
        }
        
        public ServerExperienceModificatorMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(m_experiencePercent);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_experiencePercent = reader.ReadVarUhShort();
        }
    }
}
