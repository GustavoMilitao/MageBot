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
    using BlueSheep.Common;


    public class TeleportHavenBagAnswerMessage : Message
    {
        
        public const int ProtocolId = 6646;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
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
        
        public TeleportHavenBagAnswerMessage(bool accept)
        {
            m_accept = accept;
        }
        
        public TeleportHavenBagAnswerMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(m_accept);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_accept = reader.ReadBoolean();
        }
    }
}
