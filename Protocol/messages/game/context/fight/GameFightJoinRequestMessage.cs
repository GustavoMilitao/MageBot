//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Context.Fight
{
    using BlueSheep.Protocol;


    public class GameFightJoinRequestMessage : Message
    {
        
        public const int ProtocolId = 701;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private double m_fighterId;
        
        public virtual double FighterId
        {
            get
            {
                return m_fighterId;
            }
            set
            {
                m_fighterId = value;
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
        
        public GameFightJoinRequestMessage(double fighterId, int fightId)
        {
            m_fighterId = fighterId;
            m_fightId = fightId;
        }
        
        public GameFightJoinRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(m_fighterId);
            writer.WriteInt(m_fightId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_fighterId = reader.ReadDouble();
            m_fightId = reader.ReadInt();
        }
    }
}
