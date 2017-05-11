//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Context.Roleplay.Quest
{
    using System.Collections.Generic;
    using BlueSheep.Protocol;


    public class RefreshFollowedQuestsOrderRequestMessage : Message
    {
        
        public const int ProtocolId = 6722;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<System.UInt16> m_quests;
        
        public virtual List<System.UInt16> Quests
        {
            get
            {
                return m_quests;
            }
            set
            {
                m_quests = value;
            }
        }
        
        public RefreshFollowedQuestsOrderRequestMessage(List<System.UInt16> quests)
        {
            m_quests = quests;
        }
        
        public RefreshFollowedQuestsOrderRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(m_quests.Count)));
            int questsIndex;
            for (questsIndex = 0; (questsIndex < m_quests.Count); questsIndex = (questsIndex + 1))
            {
                writer.WriteVarShort(m_quests[questsIndex]);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            int questsCount = reader.ReadUShort();
            int questsIndex;
            m_quests = new System.Collections.Generic.List<ushort>();
            for (questsIndex = 0; (questsIndex < questsCount); questsIndex = (questsIndex + 1))
            {
                m_quests.Add(reader.ReadVarUhShort());
            }
        }
    }
}
