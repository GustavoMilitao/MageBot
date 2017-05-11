//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Social
{
    using BlueSheep.Protocol;


    public class SocialNoticeMessage : Message
    {
        
        public const int ProtocolId = 6688;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private string m_content;
        
        public virtual string Content
        {
            get
            {
                return m_content;
            }
            set
            {
                m_content = value;
            }
        }
        
        private int m_timestamp;
        
        public virtual int Timestamp
        {
            get
            {
                return m_timestamp;
            }
            set
            {
                m_timestamp = value;
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
        
        public SocialNoticeMessage(string content, int timestamp, ulong memberId, string memberName)
        {
            m_content = content;
            m_timestamp = timestamp;
            m_memberId = memberId;
            m_memberName = memberName;
        }
        
        public SocialNoticeMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(m_content);
            writer.WriteInt(m_timestamp);
            writer.WriteVarLong(m_memberId);
            writer.WriteUTF(m_memberName);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_content = reader.ReadUTF();
            m_timestamp = reader.ReadInt();
            m_memberId = reader.ReadVarUhLong();
            m_memberName = reader.ReadUTF();
        }
    }
}
