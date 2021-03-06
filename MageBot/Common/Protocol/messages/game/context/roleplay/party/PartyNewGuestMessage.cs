//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Context.Roleplay.Party
{
    using BlueSheep.Common.Protocol.Types.Game.Context.Roleplay.Party;


    public class PartyNewGuestMessage : AbstractPartyEventMessage
    {
        
        public const int ProtocolId = 6260;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private PartyGuestInformations m_guest;
        
        public virtual PartyGuestInformations Guest
        {
            get
            {
                return m_guest;
            }
            set
            {
                m_guest = value;
            }
        }
        
        public PartyNewGuestMessage(PartyGuestInformations guest)
        {
            m_guest = guest;
        }
        
        public PartyNewGuestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            m_guest.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_guest = new PartyGuestInformations();
            m_guest.Deserialize(reader);
        }
    }
}
