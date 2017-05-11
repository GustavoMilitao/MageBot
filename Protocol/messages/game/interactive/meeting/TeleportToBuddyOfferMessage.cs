//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Interactive.Meeting
{
    using BlueSheep.Protocol;


    public class TeleportToBuddyOfferMessage : Message
    {
        
        public const int ProtocolId = 6287;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ushort m_dungeonId;
        
        public virtual ushort DungeonId
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
        
        private ulong m_buddyId;
        
        public virtual ulong BuddyId
        {
            get
            {
                return m_buddyId;
            }
            set
            {
                m_buddyId = value;
            }
        }
        
        private uint m_timeLeft;
        
        public virtual uint TimeLeft
        {
            get
            {
                return m_timeLeft;
            }
            set
            {
                m_timeLeft = value;
            }
        }
        
        public TeleportToBuddyOfferMessage(ushort dungeonId, ulong buddyId, uint timeLeft)
        {
            m_dungeonId = dungeonId;
            m_buddyId = buddyId;
            m_timeLeft = timeLeft;
        }
        
        public TeleportToBuddyOfferMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(m_dungeonId);
            writer.WriteVarLong(m_buddyId);
            writer.WriteVarInt(m_timeLeft);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_dungeonId = reader.ReadVarUhShort();
            m_buddyId = reader.ReadVarUhLong();
            m_timeLeft = reader.ReadVarUhInt();
        }
    }
}
