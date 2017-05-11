//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Context.Roleplay.Party
{
    using BlueSheep.Protocol.Types.Game.Context;


    public class PartyMemberInFightMessage : AbstractPartyMessage
    {
        
        public const int ProtocolId = 6342;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private MapCoordinatesExtended m_fightMap;
        
        public virtual MapCoordinatesExtended FightMap
        {
            get
            {
                return m_fightMap;
            }
            set
            {
                m_fightMap = value;
            }
        }
        
        private byte m_reason;
        
        public virtual byte Reason
        {
            get
            {
                return m_reason;
            }
            set
            {
                m_reason = value;
            }
        }
        
        private ulong m_memberId;
        
        public virtual ulong MemberId
        {
            get
            {
                return m_memberId;
            }
            set
            {
                m_memberId = value;
            }
        }
        
        private int m_memberAccountId;
        
        public virtual int MemberAccountId
        {
            get
            {
                return m_memberAccountId;
            }
            set
            {
                m_memberAccountId = value;
            }
        }
        
        private string m_memberName;
        
        public virtual string MemberName
        {
            get
            {
                return m_memberName;
            }
            set
            {
                m_memberName = value;
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
        
        private short m_timeBeforeFightStart;
        
        public virtual short TimeBeforeFightStart
        {
            get
            {
                return m_timeBeforeFightStart;
            }
            set
            {
                m_timeBeforeFightStart = value;
            }
        }
        
        public PartyMemberInFightMessage(MapCoordinatesExtended fightMap, byte reason, ulong memberId, int memberAccountId, string memberName, int fightId, short timeBeforeFightStart)
        {
            m_fightMap = fightMap;
            m_reason = reason;
            m_memberId = memberId;
            m_memberAccountId = memberAccountId;
            m_memberName = memberName;
            m_fightId = fightId;
            m_timeBeforeFightStart = timeBeforeFightStart;
        }
        
        public PartyMemberInFightMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            m_fightMap.Serialize(writer);
            writer.WriteByte(m_reason);
            writer.WriteVarLong(m_memberId);
            writer.WriteInt(m_memberAccountId);
            writer.WriteUTF(m_memberName);
            writer.WriteInt(m_fightId);
            writer.WriteVarShort(m_timeBeforeFightStart);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_fightMap = new MapCoordinatesExtended();
            m_fightMap.Deserialize(reader);
            m_reason = reader.ReadByte();
            m_memberId = reader.ReadVarUhLong();
            m_memberAccountId = reader.ReadInt();
            m_memberName = reader.ReadUTF();
            m_fightId = reader.ReadInt();
            m_timeBeforeFightStart = reader.ReadVarShort();
        }
    }
}
