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
    using BlueSheep.Protocol.Types.Game.Shortcut;
    using BlueSheep.Protocol;
	using BlueSheep.Protocol.Types;


    public class ShortcutBarRefreshMessage : Message
    {
        
        public const int ProtocolId = 6229;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private Shortcut m_shortcut;
        
        public virtual Shortcut Shortcut
        {
            get
            {
                return m_shortcut;
            }
            set
            {
                m_shortcut = value;
            }
        }
        
        private byte m_barType;
        
        public virtual byte BarType
        {
            get
            {
                return m_barType;
            }
            set
            {
                m_barType = value;
            }
        }
        
        public ShortcutBarRefreshMessage(Shortcut shortcut, byte barType)
        {
            m_shortcut = shortcut;
            m_barType = barType;
        }
        
        public ShortcutBarRefreshMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort(((ushort)(m_shortcut.TypeID)));
            m_shortcut.Serialize(writer);
            writer.WriteByte(m_barType);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_shortcut = ProtocolTypeManager.GetInstance<Shortcut>(reader.ReadUShort());
            m_shortcut.Deserialize(reader);
            m_barType = reader.ReadByte();
        }
    }
}
