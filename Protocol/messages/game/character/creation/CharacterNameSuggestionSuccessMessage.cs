//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MageBot.Protocol.Messages.Game.Character.Creation
{
    public class CharacterNameSuggestionSuccessMessage : Message
    {
        
        public override int ProtocolId { get; } = 5544;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private string m_suggestion;
        
        public virtual string Suggestion
        {
            get
            {
                return m_suggestion;
            }
            set
            {
                m_suggestion = value;
            }
        }
        
        public CharacterNameSuggestionSuccessMessage(string suggestion)
        {
            m_suggestion = suggestion;
        }
        
        public CharacterNameSuggestionSuccessMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(m_suggestion);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_suggestion = reader.ReadUTF();
        }
    }
}
