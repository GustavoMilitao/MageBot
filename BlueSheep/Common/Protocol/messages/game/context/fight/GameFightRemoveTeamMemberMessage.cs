//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Context.Fight
{
    using BlueSheep.Common;


    public class GameFightRemoveTeamMemberMessage : Message
    {
        
        public const int ProtocolId = 711;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private short m_fightId;
        
        public virtual short FightId
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
        
        private byte m_teamId;
        
        public virtual byte TeamId
        {
            get
            {
                return m_teamId;
            }
            set
            {
                m_teamId = value;
            }
        }
        
        private double m_charId;
        
        public virtual double CharId
        {
            get
            {
                return m_charId;
            }
            set
            {
                m_charId = value;
            }
        }
        
        public GameFightRemoveTeamMemberMessage(short fightId, byte teamId, double charId)
        {
            m_fightId = fightId;
            m_teamId = teamId;
            m_charId = charId;
        }
        
        public GameFightRemoveTeamMemberMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(m_fightId);
            writer.WriteByte(m_teamId);
            writer.WriteDouble(m_charId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_fightId = reader.ReadShort();
            m_teamId = reader.ReadByte();
            m_charId = reader.ReadDouble();
        }
    }
}
