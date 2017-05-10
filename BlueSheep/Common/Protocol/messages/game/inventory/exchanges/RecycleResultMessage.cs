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


    public class RecycleResultMessage : Message
    {
        
        public const int ProtocolId = 6601;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private uint m_nuggetsForPrism;
        
        public virtual uint NuggetsForPrism
        {
            get
            {
                return m_nuggetsForPrism;
            }
            set
            {
                m_nuggetsForPrism = value;
            }
        }
        
        private uint m_nuggetsForPlayer;
        
        public virtual uint NuggetsForPlayer
        {
            get
            {
                return m_nuggetsForPlayer;
            }
            set
            {
                m_nuggetsForPlayer = value;
            }
        }
        
        public RecycleResultMessage(uint nuggetsForPrism, uint nuggetsForPlayer)
        {
            m_nuggetsForPrism = nuggetsForPrism;
            m_nuggetsForPlayer = nuggetsForPlayer;
        }
        
        public RecycleResultMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(m_nuggetsForPrism);
            writer.WriteVarInt(m_nuggetsForPlayer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_nuggetsForPrism = reader.ReadVarUhInt();
            m_nuggetsForPlayer = reader.ReadVarUhInt();
        }
    }
}
