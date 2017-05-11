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


    public class ExchangeCraftCountModifiedMessage : Message
    {
        
        public const int ProtocolId = 6595;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private int m_count;
        
        public virtual int Count
        {
            get
            {
                return m_count;
            }
            set
            {
                m_count = value;
            }
        }
        
        public ExchangeCraftCountModifiedMessage(int count)
        {
            m_count = count;
        }
        
        public ExchangeCraftCountModifiedMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(m_count);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_count = reader.ReadVarInt();
        }
    }
}
