//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Guild.Tax
{
    using BlueSheep.Common.Protocol.Types.Game.Guild.Tax;
    using BlueSheep.Common;


    public class TaxCollectorMovementMessage : Message
    {
        
        public const int ProtocolId = 5633;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private TaxCollectorBasicInformations m_basicInfos;
        
        public virtual TaxCollectorBasicInformations BasicInfos
        {
            get
            {
                return m_basicInfos;
            }
            set
            {
                m_basicInfos = value;
            }
        }
        
        private byte m_movementType;
        
        public virtual byte MovementType
        {
            get
            {
                return m_movementType;
            }
            set
            {
                m_movementType = value;
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
        
        private string m_playerName;
        
        public virtual string PlayerName
        {
            get
            {
                return m_playerName;
            }
            set
            {
                m_playerName = value;
            }
        }
        
        public TaxCollectorMovementMessage(TaxCollectorBasicInformations basicInfos, byte movementType, ulong playerId, string playerName)
        {
            m_basicInfos = basicInfos;
            m_movementType = movementType;
            m_playerId = playerId;
            m_playerName = playerName;
        }
        
        public TaxCollectorMovementMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            m_basicInfos.Serialize(writer);
            writer.WriteByte(m_movementType);
            writer.WriteVarLong(m_playerId);
            writer.WriteUTF(m_playerName);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_basicInfos = new TaxCollectorBasicInformations();
            m_basicInfos.Deserialize(reader);
            m_movementType = reader.ReadByte();
            m_playerId = reader.ReadVarUhLong();
            m_playerName = reader.ReadUTF();
        }
    }
}
