//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Context.Mount
{
    using BlueSheep.Protocol;


    public class PaddockBuyRequestMessage : Message
    {
        
        public const int ProtocolId = 5951;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ulong m_proposedPrice;
        
        public virtual ulong ProposedPrice
        {
            get
            {
                return m_proposedPrice;
            }
            set
            {
                m_proposedPrice = value;
            }
        }
        
        public PaddockBuyRequestMessage(ulong proposedPrice)
        {
            m_proposedPrice = proposedPrice;
        }
        
        public PaddockBuyRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarLong(m_proposedPrice);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_proposedPrice = reader.ReadVarUhLong();
        }
    }
}
