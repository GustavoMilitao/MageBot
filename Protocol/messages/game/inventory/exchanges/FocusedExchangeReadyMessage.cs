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


    public class FocusedExchangeReadyMessage : ExchangeReadyMessage
    {
        
        public const int ProtocolId = 6701;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private uint m_focusActionId;
        
        public virtual uint FocusActionId
        {
            get
            {
                return m_focusActionId;
            }
            set
            {
                m_focusActionId = value;
            }
        }
        
        public FocusedExchangeReadyMessage(uint focusActionId)
        {
            m_focusActionId = focusActionId;
        }
        
        public FocusedExchangeReadyMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt(m_focusActionId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_focusActionId = reader.ReadVarUhInt();
        }
    }
}
