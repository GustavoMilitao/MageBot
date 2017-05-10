//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Context.Roleplay.Havenbag.Meeting
{
    using BlueSheep.Common.Protocol.Types.Game.Character;
    using BlueSheep.Common;


    public class InviteInHavenBagMessage : Message
    {
        
        public const int ProtocolId = 6642;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private CharacterMinimalInformations m_guestInformations;
        
        public virtual CharacterMinimalInformations GuestInformations
        {
            get
            {
                return m_guestInformations;
            }
            set
            {
                m_guestInformations = value;
            }
        }
        
        private bool m_accept;
        
        public virtual bool Accept
        {
            get
            {
                return m_accept;
            }
            set
            {
                m_accept = value;
            }
        }
        
        public InviteInHavenBagMessage(CharacterMinimalInformations guestInformations, bool accept)
        {
            m_guestInformations = guestInformations;
            m_accept = accept;
        }
        
        public InviteInHavenBagMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            m_guestInformations.Serialize(writer);
            writer.WriteBoolean(m_accept);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_guestInformations = new CharacterMinimalInformations();
            m_guestInformations.Deserialize(reader);
            m_accept = reader.ReadBoolean();
        }
    }
}
