//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Context.Display
{
    using BlueSheep.Protocol;


    public class DisplayNumericalValuePaddockMessage : Message
    {
        
        public const int ProtocolId = 6563;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private int m_rideId;
        
        public virtual int RideId
        {
            get
            {
                return m_rideId;
            }
            set
            {
                m_rideId = value;
            }
        }
        
        private int m_value;
        
        public virtual int Value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
            }
        }
        
        private byte m_type;
        
        public virtual byte Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }
        
        public DisplayNumericalValuePaddockMessage(int rideId, int value, byte type)
        {
            m_rideId = rideId;
            m_value = value;
            m_type = type;
        }
        
        public DisplayNumericalValuePaddockMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(m_rideId);
            writer.WriteInt(m_value);
            writer.WriteByte(m_type);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_rideId = reader.ReadInt();
            m_value = reader.ReadInt();
            m_type = reader.ReadByte();
        }
    }
}
