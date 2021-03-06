//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Context.Roleplay.Havenbag
{
    using BlueSheep.Common;


    public class ChangeThemeRequestMessage : Message
    {
        
        public const int ProtocolId = 6639;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_theme;
        
        public virtual byte Theme
        {
            get
            {
                return m_theme;
            }
            set
            {
                m_theme = value;
            }
        }
        
        public ChangeThemeRequestMessage(byte theme)
        {
            m_theme = theme;
        }
        
        public ChangeThemeRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(m_theme);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_theme = reader.ReadByte();
        }
    }
}
