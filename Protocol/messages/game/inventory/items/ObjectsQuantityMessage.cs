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
    using System.Collections.Generic;
    using BlueSheep.Protocol;


    public class ObjectsQuantityMessage : Message
    {
        
        public const int ProtocolId = 6206;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<ObjectItemQuantity> m_objectsUIDAndQty;
        
        public virtual List<ObjectItemQuantity> ObjectsUIDAndQty
        {
            get
            {
                return m_objectsUIDAndQty;
            }
            set
            {
                m_objectsUIDAndQty = value;
            }
        }
        
        public ObjectsQuantityMessage(List<ObjectItemQuantity> objectsUIDAndQty)
        {
            m_objectsUIDAndQty = objectsUIDAndQty;
        }
        
        public ObjectsQuantityMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(m_objectsUIDAndQty.Count)));
            int objectsUIDAndQtyIndex;
            for (objectsUIDAndQtyIndex = 0; (objectsUIDAndQtyIndex < m_objectsUIDAndQty.Count); objectsUIDAndQtyIndex = (objectsUIDAndQtyIndex + 1))
            {
                ObjectItemQuantity objectToSend = m_objectsUIDAndQty[objectsUIDAndQtyIndex];
                objectToSend.Serialize(writer);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            int objectsUIDAndQtyCount = reader.ReadUShort();
            int objectsUIDAndQtyIndex;
            m_objectsUIDAndQty = new System.Collections.Generic.List<ObjectItemQuantity>();
            for (objectsUIDAndQtyIndex = 0; (objectsUIDAndQtyIndex < objectsUIDAndQtyCount); objectsUIDAndQtyIndex = (objectsUIDAndQtyIndex + 1))
            {
                ObjectItemQuantity objectToAdd = new ObjectItemQuantity();
                objectToAdd.Deserialize(reader);
                m_objectsUIDAndQty.Add(objectToAdd);
            }
        }
    }
}