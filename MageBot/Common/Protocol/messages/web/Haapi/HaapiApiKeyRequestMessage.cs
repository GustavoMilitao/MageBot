//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Web.Haapi
{
    using BlueSheep.Common;


    public class HaapiApiKeyRequestMessage : Message
    {
        
        public const int ProtocolId = 6648;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_keyType;
        
        public virtual byte KeyType
        {
            get
            {
                return m_keyType;
            }
            set
            {
                m_keyType = value;
            }
        }
        
        public HaapiApiKeyRequestMessage(byte keyType)
        {
            m_keyType = keyType;
        }
        
        public HaapiApiKeyRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(m_keyType);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_keyType = reader.ReadByte();
        }
    }
}