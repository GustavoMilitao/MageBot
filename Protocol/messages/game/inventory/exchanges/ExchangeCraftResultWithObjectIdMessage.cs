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


    public class ExchangeCraftResultWithObjectIdMessage : ExchangeCraftResultMessage
    {
        
        public const int ProtocolId = 6000;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ushort m_objectGenericId;
        
        public virtual ushort ObjectGenericId
        {
            get
            {
                return m_objectGenericId;
            }
            set
            {
                m_objectGenericId = value;
            }
        }
        
        public ExchangeCraftResultWithObjectIdMessage(ushort objectGenericId)
        {
            m_objectGenericId = objectGenericId;
        }
        
        public ExchangeCraftResultWithObjectIdMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarShort(m_objectGenericId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_objectGenericId = reader.ReadVarUhShort();
        }
    }
}
