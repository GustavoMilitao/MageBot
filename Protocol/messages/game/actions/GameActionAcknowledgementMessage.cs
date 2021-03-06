//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MageBot.Protocol.Messages.Game.Actions
{
    public class GameActionAcknowledgementMessage : Message
    {
        
        public override int ProtocolId { get; } = 957;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private bool m_valid;
        
        public virtual bool Valid
        {
            get
            {
                return m_valid;
            }
            set
            {
                m_valid = value;
            }
        }
        
        private byte m_actionId;
        
        public virtual byte ActionId
        {
            get
            {
                return m_actionId;
            }
            set
            {
                m_actionId = value;
            }
        }
        
        public GameActionAcknowledgementMessage(bool valid, byte actionId)
        {
            m_valid = valid;
            m_actionId = actionId;
        }
        
        public GameActionAcknowledgementMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(m_valid);
            writer.WriteByte(m_actionId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_valid = reader.ReadBoolean();
            m_actionId = reader.ReadByte();
        }
    }
}
