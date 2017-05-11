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


    public class ExchangeCraftPaymentModifiedMessage : Message
    {
        
        public const int ProtocolId = 6578;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ulong m_goldSum;
        
        public virtual ulong GoldSum
        {
            get
            {
                return m_goldSum;
            }
            set
            {
                m_goldSum = value;
            }
        }
        
        public ExchangeCraftPaymentModifiedMessage(ulong goldSum)
        {
            m_goldSum = goldSum;
        }
        
        public ExchangeCraftPaymentModifiedMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarLong(m_goldSum);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_goldSum = reader.ReadVarUhLong();
        }
    }
}