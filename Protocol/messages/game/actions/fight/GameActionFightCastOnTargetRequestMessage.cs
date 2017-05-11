//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Actions.Fight
{
    using BlueSheep.Protocol;


    public class GameActionFightCastOnTargetRequestMessage : Message
    {
        
        public const int ProtocolId = 6330;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ushort m_spellId;
        
        public virtual ushort SpellId
        {
            get
            {
                return m_spellId;
            }
            set
            {
                m_spellId = value;
            }
        }
        
        private double m_targetId;
        
        public virtual double TargetId
        {
            get
            {
                return m_targetId;
            }
            set
            {
                m_targetId = value;
            }
        }
        
        public GameActionFightCastOnTargetRequestMessage(ushort spellId, double targetId)
        {
            m_spellId = spellId;
            m_targetId = targetId;
        }
        
        public GameActionFightCastOnTargetRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(m_spellId);
            writer.WriteDouble(m_targetId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_spellId = reader.ReadVarUhShort();
            m_targetId = reader.ReadDouble();
        }
    }
}
