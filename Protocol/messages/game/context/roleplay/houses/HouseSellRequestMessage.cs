//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Context.Roleplay.Houses
{
    using BlueSheep.Protocol;


    public class HouseSellRequestMessage : Message
    {
        
        public const int ProtocolId = 5697;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private int m_instanceId;
        
        public virtual int InstanceId
        {
            get
            {
                return m_instanceId;
            }
            set
            {
                m_instanceId = value;
            }
        }
        
        private ulong m_amount;
        
        public virtual ulong Amount
        {
            get
            {
                return m_amount;
            }
            set
            {
                m_amount = value;
            }
        }
        
        private bool m_forSale;
        
        public virtual bool ForSale
        {
            get
            {
                return m_forSale;
            }
            set
            {
                m_forSale = value;
            }
        }
        
        public HouseSellRequestMessage(int instanceId, ulong amount, bool forSale)
        {
            m_instanceId = instanceId;
            m_amount = amount;
            m_forSale = forSale;
        }
        
        public HouseSellRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(m_instanceId);
            writer.WriteVarLong(m_amount);
            writer.WriteBoolean(m_forSale);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_instanceId = reader.ReadInt();
            m_amount = reader.ReadVarUhLong();
            m_forSale = reader.ReadBoolean();
        }
    }
}
