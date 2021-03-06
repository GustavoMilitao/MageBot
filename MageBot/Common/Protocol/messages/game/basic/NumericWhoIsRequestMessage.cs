//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Basic
{
    using BlueSheep.Common;


    public class NumericWhoIsRequestMessage : Message
    {
        
        public const int ProtocolId = 6298;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ulong m_playerId;
        
        public virtual ulong PlayerId
        {
            get
            {
                return m_playerId;
            }
            set
            {
                m_playerId = value;
            }
        }
        
        public NumericWhoIsRequestMessage(ulong playerId)
        {
            m_playerId = playerId;
        }
        
        public NumericWhoIsRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarLong(m_playerId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_playerId = reader.ReadVarUhLong();
        }
    }
}
