//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Context.Roleplay.Havenbag.Meeting
{
    using BlueSheep.Protocol.Types.Game.Character;
    using BlueSheep.Protocol;


    public class InviteInHavenBagClosedMessage : Message
    {
        
        public const int ProtocolId = 6645;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private CharacterMinimalInformations m_hostInformations;
        
        public virtual CharacterMinimalInformations HostInformations
        {
            get
            {
                return m_hostInformations;
            }
            set
            {
                m_hostInformations = value;
            }
        }
        
        public InviteInHavenBagClosedMessage(CharacterMinimalInformations hostInformations)
        {
            m_hostInformations = hostInformations;
        }
        
        public InviteInHavenBagClosedMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            m_hostInformations.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_hostInformations = new CharacterMinimalInformations();
            m_hostInformations.Deserialize(reader);
        }
    }
}
