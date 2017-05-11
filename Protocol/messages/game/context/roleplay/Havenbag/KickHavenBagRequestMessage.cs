//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Context.Roleplay.Havenbag
{
    using BlueSheep.Protocol;


    public class KickHavenBagRequestMessage : Message
    {
        
        public const int ProtocolId = 6652;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ulong m_guestId;
        
        public virtual ulong GuestId
        {
            get
            {
                return m_guestId;
            }
            set
            {
                m_guestId = value;
            }
        }
        
        public KickHavenBagRequestMessage(ulong guestId)
        {
            m_guestId = guestId;
        }
        
        public KickHavenBagRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarLong(m_guestId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_guestId = reader.ReadVarUhLong();
        }
    }
}
