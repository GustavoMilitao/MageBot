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


    public class ExchangePlayerMultiCraftRequestMessage : ExchangeRequestMessage
    {
        
        public const int ProtocolId = 5784;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ulong m_target;
        
        public virtual ulong Target
        {
            get
            {
                return m_target;
            }
            set
            {
                m_target = value;
            }
        }
        
        private uint m_skillId;
        
        public virtual uint SkillId
        {
            get
            {
                return m_skillId;
            }
            set
            {
                m_skillId = value;
            }
        }
        
        public ExchangePlayerMultiCraftRequestMessage(ulong target, uint skillId)
        {
            m_target = target;
            m_skillId = skillId;
        }
        
        public ExchangePlayerMultiCraftRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarLong(m_target);
            writer.WriteVarInt(m_skillId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_target = reader.ReadVarUhLong();
            m_skillId = reader.ReadVarUhInt();
        }
    }
}
