//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Types.Game.Prism
{
    using BlueSheep.Protocol.Types;


    public class PrismSubareaEmptyInfo : NetworkType
    {
        
        public const int ProtocolId = 438;
        
        public override int TypeID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ushort m_subAreaId;
        
        public virtual ushort SubAreaId
        {
            get
            {
                return m_subAreaId;
            }
            set
            {
                m_subAreaId = value;
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
        
        public PrismSubareaEmptyInfo(ushort subAreaId, uint allianceId)
        {
            m_subAreaId = subAreaId;
            m_allianceId = allianceId;
        }
        
        public PrismSubareaEmptyInfo()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(m_subAreaId);
            writer.WriteVarInt(m_allianceId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_subAreaId = reader.ReadVarUhShort();
            m_allianceId = reader.ReadVarUhInt();
        }
    }
}
