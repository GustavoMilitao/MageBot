//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Inventory.Exchanges
{
    using BlueSheep.Protocol;


    public class ExchangeIsReadyMessage : Message
    {
        
        public const int ProtocolId = 5509;
        
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
        
        private bool m_ready;
        
        public virtual bool Ready
        {
            get
            {
                return m_ready;
            }
            set
            {
                m_ready = value;
            }
        }
        
        public ExchangeIsReadyMessage(double objectId, bool ready)
        {
            m_ObjectId = objectId;
            m_ready = ready;
        }
        
        public ExchangeIsReadyMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(m_ObjectId);
            writer.WriteBoolean(m_ready);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_ObjectId = reader.ReadDouble();
            m_ready = reader.ReadBoolean();
        }
    }
}
