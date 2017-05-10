//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Context.Roleplay.Fight
{
    using BlueSheep.Common;


    public class GameRolePlayFightRequestCanceledMessage : Message
    {
        
        public const int ProtocolId = 5822;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private int m_fightId;
        
        public virtual int FightId
        {
            get
            {
                return m_fightId;
            }
            set
            {
                m_fightId = value;
            }
        }
        
        private double m_sourceId;
        
        public virtual double SourceId
        {
            get
            {
                return m_sourceId;
            }
            set
            {
                m_sourceId = value;
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
        
        public GameRolePlayFightRequestCanceledMessage(int fightId, double sourceId, double targetId)
        {
            m_fightId = fightId;
            m_sourceId = sourceId;
            m_targetId = targetId;
        }
        
        public GameRolePlayFightRequestCanceledMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(m_fightId);
            writer.WriteDouble(m_sourceId);
            writer.WriteDouble(m_targetId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_fightId = reader.ReadInt();
            m_sourceId = reader.ReadDouble();
            m_targetId = reader.ReadDouble();
        }
    }
}
