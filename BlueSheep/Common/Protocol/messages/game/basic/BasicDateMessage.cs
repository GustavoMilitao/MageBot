//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Basic
{
    using BlueSheep.Common;


    public class BasicDateMessage : Message
    {
        
        public const int ProtocolId = 177;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_day;
        
        public virtual byte Day
        {
            get
            {
                return m_day;
            }
            set
            {
                m_day = value;
            }
        }
        
        private byte m_month;
        
        public virtual byte Month
        {
            get
            {
                return m_month;
            }
            set
            {
                m_month = value;
            }
        }
        
        private short m_year;
        
        public virtual short Year
        {
            get
            {
                return m_year;
            }
            set
            {
                m_year = value;
            }
        }
        
        public BasicDateMessage(byte day, byte month, short year)
        {
            m_day = day;
            m_month = month;
            m_year = year;
        }
        
        public BasicDateMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(m_day);
            writer.WriteByte(m_month);
            writer.WriteShort(m_year);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_day = reader.ReadByte();
            m_month = reader.ReadByte();
            m_year = reader.ReadShort();
        }
    }
}
