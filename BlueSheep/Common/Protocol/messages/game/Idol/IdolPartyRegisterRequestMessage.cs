//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Idol
{
    using BlueSheep.Common;


    public class IdolPartyRegisterRequestMessage : Message
    {
        
        public const int ProtocolId = 6582;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private bool m_register;
        
        public virtual bool Register
        {
            get
            {
                return m_register;
            }
            set
            {
                m_register = value;
            }
        }
        
        public IdolPartyRegisterRequestMessage(bool register)
        {
            m_register = register;
        }
        
        public IdolPartyRegisterRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(m_register);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_register = reader.ReadBoolean();
        }
    }
}
