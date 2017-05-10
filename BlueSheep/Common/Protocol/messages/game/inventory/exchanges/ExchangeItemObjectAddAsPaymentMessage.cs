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


    public class ExchangeItemObjectAddAsPaymentMessage : Message
    {
        
        public const int ProtocolId = 5766;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_paymentType;
        
        public virtual byte PaymentType
        {
            get
            {
                return m_paymentType;
            }
            set
            {
                m_paymentType = value;
            }
        }
        
        private bool m_bAdd;
        
        public virtual bool BAdd
        {
            get
            {
                return m_bAdd;
            }
            set
            {
                m_bAdd = value;
            }
        }
        
        private uint m_objectToMoveId;
        
        public virtual uint ObjectToMoveId
        {
            get
            {
                return m_objectToMoveId;
            }
            set
            {
                m_objectToMoveId = value;
            }
        }
        
        private uint m_quantity;
        
        public virtual uint Quantity
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
        
        public ExchangeItemObjectAddAsPaymentMessage(byte paymentType, bool bAdd, uint objectToMoveId, uint quantity)
        {
            m_paymentType = paymentType;
            m_bAdd = bAdd;
            m_objectToMoveId = objectToMoveId;
            m_quantity = quantity;
        }
        
        public ExchangeItemObjectAddAsPaymentMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(m_paymentType);
            writer.WriteBoolean(m_bAdd);
            writer.WriteVarInt(m_objectToMoveId);
            writer.WriteVarInt(m_quantity);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_paymentType = reader.ReadByte();
            m_bAdd = reader.ReadBoolean();
            m_objectToMoveId = reader.ReadVarUhInt();
            m_quantity = reader.ReadVarUhInt();
        }
    }
}
