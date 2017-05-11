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


    public class ExchangeStartedMessage : Message
    {
        
        public const int ProtocolId = 5512;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_exchangeType;
        
        public virtual byte ExchangeType
        {
            get
            {
                return m_exchangeType;
            }
            set
            {
                m_exchangeType = value;
            }
        }
        
        public ExchangeStartedMessage(byte exchangeType)
        {
            m_exchangeType = exchangeType;
        }
        
        public ExchangeStartedMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(m_exchangeType);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_exchangeType = reader.ReadByte();
        }
    }
}
