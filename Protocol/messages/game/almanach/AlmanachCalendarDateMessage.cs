//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Almanach
{
    using BlueSheep.Protocol;


    public class AlmanachCalendarDateMessage : Message
    {
        
        public const int ProtocolId = 6341;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private int m_date;
        
        public virtual int Date
        {
            get
            {
                return m_date;
            }
            set
            {
                m_date = value;
            }
        }
        
        public AlmanachCalendarDateMessage(int date)
        {
            m_date = date;
        }
        
        public AlmanachCalendarDateMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(m_date);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_date = reader.ReadInt();
        }
    }
}
