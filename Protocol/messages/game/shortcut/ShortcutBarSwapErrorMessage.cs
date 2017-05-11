//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Shortcut
{
    using BlueSheep.Protocol;


    public class ShortcutBarSwapErrorMessage : Message
    {
        
        public const int ProtocolId = 6226;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_error;
        
        public virtual byte Error
        {
            get
            {
                return m_error;
            }
            set
            {
                m_error = value;
            }
        }
        
        public ShortcutBarSwapErrorMessage(byte error)
        {
            m_error = error;
        }
        
        public ShortcutBarSwapErrorMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(m_error);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_error = reader.ReadByte();
        }
    }
}
