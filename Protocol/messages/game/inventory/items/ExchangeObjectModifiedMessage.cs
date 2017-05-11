//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Inventory.Items
{
    using BlueSheep.Protocol.Messages.Game.Inventory.Exchanges;
    using BlueSheep.Protocol.Types.Game.Data.Items;


    public class ExchangeObjectModifiedMessage : ExchangeObjectMessage
    {
        
        public const int ProtocolId = 5519;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ObjectItem m_object;
        
        public virtual ObjectItem Object
        {
            get
            {
                return m_object;
            }
            set
            {
                m_object = value;
            }
        }
        
        public ExchangeObjectModifiedMessage(ObjectItem @object)
        {
            m_object = @object;
        }
        
        public ExchangeObjectModifiedMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            m_object.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_object = new ObjectItem();
            m_object.Deserialize(reader);
        }
    }
}
