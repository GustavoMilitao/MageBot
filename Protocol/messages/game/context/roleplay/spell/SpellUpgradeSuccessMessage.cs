//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Context.Roleplay.Spell
{
    using BlueSheep.Protocol;


    public class SpellUpgradeSuccessMessage : Message
    {
        
        public const int ProtocolId = 1201;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private int m_spellId;
        
        public virtual int SpellId
        {
            get
            {
                return m_spellId;
            }
            set
            {
                m_spellId = value;
            }
        }
        
        private byte m_spellLevel;
        
        public virtual byte SpellLevel
        {
            get
            {
                return m_spellLevel;
            }
            set
            {
                m_spellLevel = value;
            }
        }
        
        public SpellUpgradeSuccessMessage(int spellId, byte spellLevel)
        {
            m_spellId = spellId;
            m_spellLevel = spellLevel;
        }
        
        public SpellUpgradeSuccessMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(m_spellId);
            writer.WriteByte(m_spellLevel);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_spellId = reader.ReadInt();
            m_spellLevel = reader.ReadByte();
        }
    }
}
