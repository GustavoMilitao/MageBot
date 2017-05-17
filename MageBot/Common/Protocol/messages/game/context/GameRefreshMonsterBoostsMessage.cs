//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Context
{
    using BlueSheep.Common.Protocol.Types.Game.Context.Roleplay;
    using System.Collections.Generic;
    using BlueSheep.Common;


    public class GameRefreshMonsterBoostsMessage : Message
    {
        
        public const int ProtocolId = 6618;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<MonsterBoosts> m_monsterBoosts;
        
        public virtual List<MonsterBoosts> MonsterBoosts
        {
            get
            {
                return m_monsterBoosts;
            }
            set
            {
                m_monsterBoosts = value;
            }
        }
        
        private List<MonsterBoosts> m_familyBoosts;
        
        public virtual List<MonsterBoosts> FamilyBoosts
        {
            get
            {
                return m_familyBoosts;
            }
            set
            {
                m_familyBoosts = value;
            }
        }
        
        public GameRefreshMonsterBoostsMessage(List<MonsterBoosts> monsterBoosts, List<MonsterBoosts> familyBoosts)
        {
            m_monsterBoosts = monsterBoosts;
            m_familyBoosts = familyBoosts;
        }
        
        public GameRefreshMonsterBoostsMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(m_monsterBoosts.Count)));
            int monsterBoostsIndex;
            for (monsterBoostsIndex = 0; (monsterBoostsIndex < m_monsterBoosts.Count); monsterBoostsIndex = (monsterBoostsIndex + 1))
            {
                MonsterBoosts objectToSend = m_monsterBoosts[monsterBoostsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteShort(((short)(m_familyBoosts.Count)));
            int familyBoostsIndex;
            for (familyBoostsIndex = 0; (familyBoostsIndex < m_familyBoosts.Count); familyBoostsIndex = (familyBoostsIndex + 1))
            {
                MonsterBoosts objectToSend = m_familyBoosts[familyBoostsIndex];
                objectToSend.Serialize(writer);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            int monsterBoostsCount = reader.ReadUShort();
            int monsterBoostsIndex;
            m_monsterBoosts = new System.Collections.Generic.List<MonsterBoosts>();
            for (monsterBoostsIndex = 0; (monsterBoostsIndex < monsterBoostsCount); monsterBoostsIndex = (monsterBoostsIndex + 1))
            {
                MonsterBoosts objectToAdd = new MonsterBoosts();
                objectToAdd.Deserialize(reader);
                m_monsterBoosts.Add(objectToAdd);
            }
            int familyBoostsCount = reader.ReadUShort();
            int familyBoostsIndex;
            m_familyBoosts = new System.Collections.Generic.List<MonsterBoosts>();
            for (familyBoostsIndex = 0; (familyBoostsIndex < familyBoostsCount); familyBoostsIndex = (familyBoostsIndex + 1))
            {
                MonsterBoosts objectToAdd = new MonsterBoosts();
                objectToAdd.Deserialize(reader);
                m_familyBoosts.Add(objectToAdd);
            }
        }
    }
}