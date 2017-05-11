//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Guild
{
    using BlueSheep.Protocol;


    public class GuildInvitationStateRecrutedMessage : Message
    {
        
        public const int ProtocolId = 5548;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_invitationState;
        
        public virtual byte InvitationState
        {
            get
            {
                return m_invitationState;
            }
            set
            {
                m_invitationState = value;
            }
        }
        
        public GuildInvitationStateRecrutedMessage(byte invitationState)
        {
            m_invitationState = invitationState;
        }
        
        public GuildInvitationStateRecrutedMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(m_invitationState);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_invitationState = reader.ReadByte();
        }
    }
}