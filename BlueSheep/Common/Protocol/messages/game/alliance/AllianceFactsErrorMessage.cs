//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Alliance
{
    using BlueSheep.Common;


    public class AllianceFactsErrorMessage : Message
    {
        
        public const int ProtocolId = 6423;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private uint m_allianceId;
        
        public virtual uint AllianceId
        {
            get
            {
                return m_allianceId;
            }
            set
            {
                m_allianceId = value;
            }
        }
        
        public AllianceFactsErrorMessage(uint allianceId)
        {
            m_allianceId = allianceId;
        }
        
        public AllianceFactsErrorMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(m_allianceId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_allianceId = reader.ReadVarUhInt();
        }
    }
}
