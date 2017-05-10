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


    public class ExchangeBidHouseListMessage : Message
    {
        
        public const int ProtocolId = 5807;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ushort m_ObjectId;
        
        public virtual ushort ObjectId
        {
            get
            {
                return m_ObjectId;
            }
            set
            {
                m_ObjectId = value;
            }
        }
        
        public ExchangeBidHouseListMessage(ushort objectId)
        {
            m_ObjectId = objectId;
        }
        
        public ExchangeBidHouseListMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(m_ObjectId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_ObjectId = reader.ReadVarUhShort();
        }
    }
}
