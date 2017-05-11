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


    public class ExchangeCraftResultMagicWithObjectDescMessage : ExchangeCraftResultWithObjectDescMessage
    {
        
        public const int ProtocolId = 6188;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_magicPoolStatus;
        
        public virtual byte MagicPoolStatus
        {
            get
            {
                return m_magicPoolStatus;
            }
            set
            {
                m_magicPoolStatus = value;
            }
        }
        
        public ExchangeCraftResultMagicWithObjectDescMessage(byte magicPoolStatus)
        {
            m_magicPoolStatus = magicPoolStatus;
        }
        
        public ExchangeCraftResultMagicWithObjectDescMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(m_magicPoolStatus);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_magicPoolStatus = reader.ReadByte();
        }
    }
}