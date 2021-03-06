//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MageBot.Protocol.Messages.Game.Context.Fight
{
    public class GameFightPlacementPositionRequestMessage : Message
    {
        
        public override int ProtocolId { get; } = 704;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ushort m_cellId;
        
        public virtual ushort CellId
        {
            get
            {
                return m_cellId;
            }
            set
            {
                m_cellId = value;
            }
        }
        
        public GameFightPlacementPositionRequestMessage(ushort cellId)
        {
            m_cellId = cellId;
        }
        
        public GameFightPlacementPositionRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(m_cellId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_cellId = reader.ReadVarUhShort();
        }
    }
}
