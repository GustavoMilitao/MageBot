//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Inventory.Exchanges
{
    using BlueSheep.Common;


    public class ExchangeGoldPaymentForCraftMessage : Message
    {
        
        public const int ProtocolId = 5833;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private bool m_onlySuccess;
        
        public virtual bool OnlySuccess
        {
            get
            {
                return m_onlySuccess;
            }
            set
            {
                m_onlySuccess = value;
            }
        }
        
        private uint m_goldSum;
        
        public virtual uint GoldSum
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
        
        public ExchangeGoldPaymentForCraftMessage(bool onlySuccess, uint goldSum)
        {
            m_onlySuccess = onlySuccess;
            m_goldSum = goldSum;
        }
        
        public ExchangeGoldPaymentForCraftMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(m_onlySuccess);
            writer.WriteVarInt(m_goldSum);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_onlySuccess = reader.ReadBoolean();
            m_goldSum = reader.ReadVarUhInt();
        }
    }
}
