//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Context.Mount
{
    using BlueSheep.Protocol;


    public class MountInformationRequestMessage : Message
    {
        
        public const int ProtocolId = 5972;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private double m_ObjectId;
        
        public virtual double ObjectId
        {
            get
            {
                return m_ObjectId;
            }
            set
            {
                m_ObjectId = value;
            }
        }
        
        private double m_time;
        
        public virtual double Time
        {
            get
            {
                return m_time;
            }
            set
            {
                m_time = value;
            }
        }
        
        public MountInformationRequestMessage(double objectId, double time)
        {
            m_ObjectId = objectId;
            m_time = time;
        }
        
        public MountInformationRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(m_ObjectId);
            writer.WriteDouble(m_time);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_ObjectId = reader.ReadDouble();
            m_time = reader.ReadDouble();
        }
    }
}
