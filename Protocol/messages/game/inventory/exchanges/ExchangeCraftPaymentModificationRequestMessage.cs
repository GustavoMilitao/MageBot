//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MageBot.Protocol.Messages.Game.Inventory.Exchanges
{
    public class ExchangeCraftPaymentModificationRequestMessage : Message
    {
        
        public override int ProtocolId { get; } = 6579;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ulong m_quantity;
        
        public virtual ulong Quantity
        {
            get
            {
                return m_quantity;
            }
            set
            {
                m_quantity = value;
            }
        }
        
        public ExchangeCraftPaymentModificationRequestMessage(ulong quantity)
        {
            m_quantity = quantity;
        }
        
        public ExchangeCraftPaymentModificationRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarLong(m_quantity);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_quantity = reader.ReadVarUhLong();
        }
    }
}
