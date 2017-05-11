//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Context.Fight.Challenge
{
    using BlueSheep.Protocol;


    public class ChallengeDungeonStackedBonusMessage : Message
    {
        
        public const int ProtocolId = 6151;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private int m_dungeonId;
        
        public virtual int DungeonId
        {
            get
            {
                return m_dungeonId;
            }
            set
            {
                m_dungeonId = value;
            }
        }
        
        private int m_xpBonus;
        
        public virtual int XpBonus
        {
            get
            {
                return m_xpBonus;
            }
            set
            {
                m_xpBonus = value;
            }
        }
        
        private int m_dropBonus;
        
        public virtual int DropBonus
        {
            get
            {
                return m_dropBonus;
            }
            set
            {
                m_dropBonus = value;
            }
        }
        
        public ChallengeDungeonStackedBonusMessage(int dungeonId, int xpBonus, int dropBonus)
        {
            m_dungeonId = dungeonId;
            m_xpBonus = xpBonus;
            m_dropBonus = dropBonus;
        }
        
        public ChallengeDungeonStackedBonusMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(m_dungeonId);
            writer.WriteInt(m_xpBonus);
            writer.WriteInt(m_dropBonus);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_dungeonId = reader.ReadInt();
            m_xpBonus = reader.ReadInt();
            m_dropBonus = reader.ReadInt();
        }
    }
}
