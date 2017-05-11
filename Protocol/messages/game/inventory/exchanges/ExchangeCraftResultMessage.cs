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


    public class ExchangeCraftResultMessage : Message
    {
        
        public const int ProtocolId = 5790;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_craftResult;
        
        public virtual byte CraftResult
        {
            get
            {
                return m_craftResult;
            }
            set
            {
                m_craftResult = value;
            }
        }
        
        public ExchangeCraftResultMessage(byte craftResult)
        {
            m_craftResult = craftResult;
        }
        
        public ExchangeCraftResultMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(m_craftResult);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_craftResult = reader.ReadByte();
        }
    }
}
