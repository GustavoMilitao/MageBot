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


    public class LivingObjectDissociateMessage : Message
    {
        
        public const int ProtocolId = 5723;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private uint m_livingUID;
        
        public virtual uint LivingUID
        {
            get
            {
                return m_livingUID;
            }
            set
            {
                m_livingUID = value;
            }
        }
        
        private sbyte m_livingPosition;
        
        public virtual sbyte LivingPosition
        {
            get
            {
                return m_livingPosition;
            }
            set
            {
                m_livingPosition = value;
            }
        }
        
        public LivingObjectDissociateMessage(uint livingUID, sbyte livingPosition)
        {
            m_livingUID = livingUID;
            m_livingPosition = livingPosition;
        }
        
        public LivingObjectDissociateMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(m_livingUID);
            writer.WriteSByte(m_livingPosition);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_livingUID = reader.ReadVarUhInt();
            m_livingPosition = reader.ReadSByte();
        }
    }
}
