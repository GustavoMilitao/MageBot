//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Prism
{
    using BlueSheep.Common;


    public class PrismSetSabotagedRefusedMessage : Message
    {
        
        public const int ProtocolId = 6466;
        
        public override int MessageID
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
        
        private byte m_reason;
        
        public virtual byte Reason
        {
            get
            {
                return m_reason;
            }
            set
            {
                m_reason = value;
            }
        }
        
        public PrismSetSabotagedRefusedMessage(ushort subAreaId, byte reason)
        {
            m_subAreaId = subAreaId;
            m_reason = reason;
        }
        
        public PrismSetSabotagedRefusedMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(m_subAreaId);
            writer.WriteByte(m_reason);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_subAreaId = reader.ReadVarUhShort();
            m_reason = reader.ReadByte();
        }
    }
}
