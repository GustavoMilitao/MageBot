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
    using BlueSheep.Protocol.Messages.Game.Actions;


    public class GameActionFightPointsVariationMessage : AbstractGameActionMessage
    {
        
        public const int ProtocolId = 1030;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
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
        
        private short m_delta;
        
        public virtual short Delta
        {
            get
            {
                return m_delta;
            }
            set
            {
                m_delta = value;
            }
        }
        
        public GameActionFightPointsVariationMessage(double targetId, short delta)
        {
            m_targetId = targetId;
            m_delta = delta;
        }
        
        public GameActionFightPointsVariationMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(m_targetId);
            writer.WriteShort(m_delta);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_targetId = reader.ReadDouble();
            m_delta = reader.ReadShort();
        }
    }
}
