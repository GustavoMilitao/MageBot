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


    public class ExchangeBidHouseGenericItemRemovedMessage : Message
    {
        
        public const int ProtocolId = 5948;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ushort m_objGenericId;
        
        public virtual ushort ObjGenericId
        {
            get
            {
                return m_objGenericId;
            }
            set
            {
                m_objGenericId = value;
            }
        }
        
        public ExchangeBidHouseGenericItemRemovedMessage(ushort objGenericId)
        {
            m_objGenericId = objGenericId;
        }
        
        public ExchangeBidHouseGenericItemRemovedMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(m_objGenericId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_objGenericId = reader.ReadVarUhShort();
        }
    }
}
