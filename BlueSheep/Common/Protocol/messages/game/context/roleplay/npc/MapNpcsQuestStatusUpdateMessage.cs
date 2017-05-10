//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Context.Roleplay.Npc
{
    using BlueSheep.Common.Protocol.Types.Game.Context.Roleplay.Quest;
    using System.Collections.Generic;
    using BlueSheep.Common;


    public class MapNpcsQuestStatusUpdateMessage : Message
    {
        
        public const int ProtocolId = 5642;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<System.Int32> m_npcsIdsWithQuest;
        
        public virtual List<System.Int32> NpcsIdsWithQuest
        {
            get
            {
                return m_npcsIdsWithQuest;
            }
            set
            {
                m_npcsIdsWithQuest = value;
            }
        }
        
        private List<GameRolePlayNpcQuestFlag> m_questFlags;
        
        public virtual List<GameRolePlayNpcQuestFlag> QuestFlags
        {
            get
            {
                return m_questFlags;
            }
            set
            {
                m_questFlags = value;
            }
        }
        
        private List<System.Int32> m_npcsIdsWithoutQuest;
        
        public virtual List<System.Int32> NpcsIdsWithoutQuest
        {
            get
            {
                return m_npcsIdsWithoutQuest;
            }
            set
            {
                m_npcsIdsWithoutQuest = value;
            }
        }
        
        private int m_mapId;
        
        public virtual int MapId
        {
            get
            {
                return m_mapId;
            }
            set
            {
                m_mapId = value;
            }
        }
        
        public MapNpcsQuestStatusUpdateMessage(List<System.Int32> npcsIdsWithQuest, List<GameRolePlayNpcQuestFlag> questFlags, List<System.Int32> npcsIdsWithoutQuest, int mapId)
        {
            m_npcsIdsWithQuest = npcsIdsWithQuest;
            m_questFlags = questFlags;
            m_npcsIdsWithoutQuest = npcsIdsWithoutQuest;
            m_mapId = mapId;
        }
        
        public MapNpcsQuestStatusUpdateMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(m_npcsIdsWithQuest.Count)));
            int npcsIdsWithQuestIndex;
            for (npcsIdsWithQuestIndex = 0; (npcsIdsWithQuestIndex < m_npcsIdsWithQuest.Count); npcsIdsWithQuestIndex = (npcsIdsWithQuestIndex + 1))
            {
                writer.WriteInt(m_npcsIdsWithQuest[npcsIdsWithQuestIndex]);
            }
            writer.WriteShort(((short)(m_questFlags.Count)));
            int questFlagsIndex;
            for (questFlagsIndex = 0; (questFlagsIndex < m_questFlags.Count); questFlagsIndex = (questFlagsIndex + 1))
            {
                GameRolePlayNpcQuestFlag objectToSend = m_questFlags[questFlagsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteShort(((short)(m_npcsIdsWithoutQuest.Count)));
            int npcsIdsWithoutQuestIndex;
            for (npcsIdsWithoutQuestIndex = 0; (npcsIdsWithoutQuestIndex < m_npcsIdsWithoutQuest.Count); npcsIdsWithoutQuestIndex = (npcsIdsWithoutQuestIndex + 1))
            {
                writer.WriteInt(m_npcsIdsWithoutQuest[npcsIdsWithoutQuestIndex]);
            }
            writer.WriteInt(m_mapId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            int npcsIdsWithQuestCount = reader.ReadUShort();
            int npcsIdsWithQuestIndex;
            m_npcsIdsWithQuest = new System.Collections.Generic.List<int>();
            for (npcsIdsWithQuestIndex = 0; (npcsIdsWithQuestIndex < npcsIdsWithQuestCount); npcsIdsWithQuestIndex = (npcsIdsWithQuestIndex + 1))
            {
                m_npcsIdsWithQuest.Add(reader.ReadInt());
            }
            int questFlagsCount = reader.ReadUShort();
            int questFlagsIndex;
            m_questFlags = new System.Collections.Generic.List<GameRolePlayNpcQuestFlag>();
            for (questFlagsIndex = 0; (questFlagsIndex < questFlagsCount); questFlagsIndex = (questFlagsIndex + 1))
            {
                GameRolePlayNpcQuestFlag objectToAdd = new GameRolePlayNpcQuestFlag();
                objectToAdd.Deserialize(reader);
                m_questFlags.Add(objectToAdd);
            }
            int npcsIdsWithoutQuestCount = reader.ReadUShort();
            int npcsIdsWithoutQuestIndex;
            m_npcsIdsWithoutQuest = new System.Collections.Generic.List<int>();
            for (npcsIdsWithoutQuestIndex = 0; (npcsIdsWithoutQuestIndex < npcsIdsWithoutQuestCount); npcsIdsWithoutQuestIndex = (npcsIdsWithoutQuestIndex + 1))
            {
                m_npcsIdsWithoutQuest.Add(reader.ReadInt());
            }
            m_mapId = reader.ReadInt();
        }
    }
}
