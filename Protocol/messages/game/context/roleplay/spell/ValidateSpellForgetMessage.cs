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


    public class ValidateSpellForgetMessage : Message
    {
        
        public const int ProtocolId = 1700;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ushort m_spellId;
        
        public virtual ushort SpellId
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
        
        public ValidateSpellForgetMessage(ushort spellId)
        {
            m_spellId = spellId;
        }
        
        public ValidateSpellForgetMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(m_spellId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_spellId = reader.ReadVarUhShort();
        }
    }
}
