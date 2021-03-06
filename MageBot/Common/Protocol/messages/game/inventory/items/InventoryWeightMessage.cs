//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Inventory.Items
{
    using BlueSheep.Common;


    public class InventoryWeightMessage : Message
    {
        
        public const int ProtocolId = 3009;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private uint m_weight;
        
        public virtual uint Weight
        {
            get
            {
                return m_weight;
            }
            set
            {
                m_weight = value;
            }
        }
        
        private uint m_weightMax;
        
        public virtual uint WeightMax
        {
            get
            {
                return m_weightMax;
            }
            set
            {
                m_weightMax = value;
            }
        }
        
        public InventoryWeightMessage(uint weight, uint weightMax)
        {
            m_weight = weight;
            m_weightMax = weightMax;
        }
        
        public InventoryWeightMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(m_weight);
            writer.WriteVarInt(m_weightMax);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_weight = reader.ReadVarUhInt();
            m_weightMax = reader.ReadVarUhInt();
        }
    }
}
