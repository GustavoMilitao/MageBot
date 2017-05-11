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
    using BlueSheep.Protocol.Types.Game.Data.Items;
    using BlueSheep.Protocol;


    public class ObjectModifiedMessage : Message
    {
        
        public const int ProtocolId = 3029;
        
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
        
        public ObjectModifiedMessage(ObjectItem @object)
        {
            m_object = @object;
        }
        
        public ObjectModifiedMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            m_object.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_object = new ObjectItem();
            m_object.Deserialize(reader);
        }
    }
}
